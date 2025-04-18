using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TicketsReservationSystem.BLL.Dto_s;
using TicketsReservationSystem.BLL.Dto_s.AuthDto_s;
using TicketsReservationSystem.DAL.Models;

namespace TicketsReservationSystem.BLL.Managers.AuthManagers
{
    public class AuthManager : IAuthManager
    {
        private UserManager<ApplicationUser> _userManager;
        private IConfiguration _configuration;
        private IUserManager _Manager;
        private IClientManager _clientManager;
        private IVendorManager _vendorManager;

        public AuthManager(UserManager<ApplicationUser> userManager, IConfiguration configuration 
                                , IUserManager manager , IClientManager clientManager , IVendorManager vendorManager)
        {
            _userManager = userManager;
            _configuration = configuration;
            _Manager = manager;
            _clientManager = clientManager;
            _vendorManager = vendorManager;
        }

        public async Task<string> Login(LoginDto user)
        {
            var logged = await _userManager.FindByEmailAsync(user.email);

            if (logged == null)
            {
                return null;
            }

            var check = await _userManager.CheckPasswordAsync(logged, user.password);
            if (check == null)
            {
                return null;
            }

            var claims = await _userManager.GetClaimsAsync(logged);
            var token = GenerateToken(claims);

            return token;

        }


        public async Task<string> Register(RegisterDto user)
        {
            ApplicationUser applicationUser = new ApplicationUser();
            applicationUser.Email = user.email;
            applicationUser.UserName = user.firstname + user.lastname;
            applicationUser.Role = user.role;

            var result = await _userManager.CreateAsync(applicationUser, user.password);
            Console.WriteLine(result);

            if (result.Succeeded)
            {

             var id = _Manager.Add(new UserAddDto
                {
                    email = user.email,
                    password = user.password,
                    firstname = user.firstname,
                    lastname = user.lastname,
                    role = user.role,
                });
                
                switch (user.role)
                {
                    case "Client":
                        var clientDto = new Client
                        {
                            userId = id
                        };
                        _clientManager.Add(id);
                        break;

                    case "Vendor":
                        var vendorDto = new VendorAddDto
                        {
                            id = id,
                        };
                        _vendorManager.Add(vendorDto , id);
                        break;
                }




                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim("UserName", user.firstname + user.lastname));
                claims.Add(new Claim("Email", user.email));
                claims.Add(new Claim("Password", user.password));
                claims.Add(new Claim("Role", user.role));

                var token = GenerateToken(claims);
                return token;

            }
            return null;

        }

        public String GenerateToken(IList<Claim> claims)
        {
            var secretkey = _configuration.GetSection("SecretKey").Value;
            var byteSecretKey = Encoding.UTF8.GetBytes(secretkey);
            SecurityKey securityKey = new SymmetricSecurityKey(byteSecretKey);

            var _rsa = new RSACryptoServiceProvider(2048);
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var expiryDate = DateTime.Now.AddDays(5);
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(claims: claims, expires: expiryDate, signingCredentials: signingCredentials);
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            var token = handler.WriteToken(jwtSecurityToken);
            Console.WriteLine(token);
            return token;
        }
    }
}
