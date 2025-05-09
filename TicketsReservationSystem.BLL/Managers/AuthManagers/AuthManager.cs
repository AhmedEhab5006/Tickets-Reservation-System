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
using TicketsReservationSystem.DAL.Repository;

namespace TicketsReservationSystem.BLL.Managers.AuthManagers
{
    public class AuthManager : IAuthManager
    {
        private IUserRepository _userRepository;
        private IApplicationUserRoleRepository _applicationUserRoleRepository;
        private IConfiguration _configuration;
        private IClientRepository _clientRepository;

        public AuthManager(IUserRepository userRepository, IApplicationUserRoleRepository applicationUserRoleRepository, IConfiguration configuration , IClientRepository clientRepository)
        {
            _userRepository = userRepository;
            _applicationUserRoleRepository = applicationUserRoleRepository;
            _configuration = configuration;
            _clientRepository = clientRepository;
        }

        public string GenerateToken(IList<Claim> claims)
        {
            var secretkey = _configuration.GetSection("SecretKey").Value;
            var byteSecretKey = Encoding.UTF8.GetBytes(secretkey);
            SecurityKey securityKey = new SymmetricSecurityKey(byteSecretKey);
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var expiryDate = DateTime.Now.AddDays(5);
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(claims: claims, expires: expiryDate, signingCredentials: signingCredentials);
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            var token = handler.WriteToken(jwtSecurityToken);

            return token;
        }

        public async Task<string> Login(LoginDto loginDto)
        {
            var logged = await _userRepository.GetByEmail(loginDto.email);

            if (logged == null)
            {
                return null;
            }

            var check = await _userRepository.CheckPassword(loginDto.password, logged);

            if (check == null)
            {
                return null;
            }

            var role = await _applicationUserRoleRepository.GetRole(logged);
            var claims = await _userRepository.GetClaims(logged);
            claims.Add(new Claim(ClaimTypes.NameIdentifier, logged.Id));
            claims.Add(new Claim(ClaimTypes.Name, logged.UserName));
            claims.Add(new Claim(ClaimTypes.Email, logged.Email));
            claims.Add(new Claim(ClaimTypes.Role, role));
            

            var token = GenerateToken(claims);

            return token;

        }

        public async Task<string> Register(RegisterDto registerDto)
        {

            var addedUser = new ApplicationUser
            {
                Email = registerDto.email,
                UserName = registerDto.username
            };

            var emailExists = await _userRepository.GetByEmail(addedUser.Email);
            var userNameExists = await _userRepository.GetByUserName(addedUser.UserName);

            if (emailExists != null || userNameExists != null)
            {
                return "exsist";
            }



            string addingResult = string.Empty;
            string addingRoleResult = string.Empty;

            if (registerDto.role == "Vendor")
            {
                var addedVendor = new Vendor
                {
                    UserName = registerDto.username,
                    Email = registerDto.email,
                    acceptanceStatus = "Pending"
                    
                    // Add any other needed properties
                };

                addingResult = await _userRepository.AddAsync(registerDto.password, addedVendor);
                addingRoleResult = await _applicationUserRoleRepository.Add(registerDto.role, addedVendor);

                addedUser = addedVendor;
            }
            else if (registerDto.role == "Client")
            {
                var address = new Address();
               var id =  _clientRepository.AddAddress(address);
                var addedClient = new Client
                {
                    UserName = registerDto.username,
                    Email = registerDto.email,
                    addressId = id
                    
                };

                addingResult = await _userRepository.AddAsync(registerDto.password, addedClient);
                addingRoleResult = await _applicationUserRoleRepository.Add(registerDto.role, addedClient);

                addedUser = addedClient;
            }
            else
            {
                return "Invalid role"; // Or handle unsupported roles as needed
            }

            if (addingResult == "done" && addingRoleResult == "done")
            {
                var claims = new List<Claim>
                {
                new Claim("UserName", registerDto.username),
                new Claim("Email", registerDto.email),
                new Claim("Password", registerDto.password),
                new Claim("Role", registerDto.role)
                };

                var token = GenerateToken(claims);
                return token;
            }

            return null; // Or appropriate error

        }
    }
}