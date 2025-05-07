using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketsReservationSystem.BLL.Dto_s.AuthDto_s;
using TicketsReservationSystem.BLL.Managers.AuthManagers;
using TicketsReservationSystem.API.Helpers;
using Microsoft.Extensions.Caching.Memory;
using TicketsReservationSystem.API.Filters;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TicketsReservationSystem.DAL.Models;
using TicketsReservationSystem.BLL.Managers;
using TicketsReservationSystem.DAL.Repository;
using TicketsReservationSystem.BLL.Dto_s;



namespace TicketsReservationSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthManager _authManager;
        private IVendorManager _vendorManager;
        private IGetLoggedData _getLoggedData;

        public AuthController(IAuthManager authManager , IVendorManager vendorManager , IGetLoggedData getLoggedData)
        {
            _authManager = authManager;
            _vendorManager = vendorManager;
            _getLoggedData = getLoggedData;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var done = await _authManager.Register(registerDto);

            if (done == "exsist")
            {
                return BadRequest("Email or username already taken");
            }

            if (done == null)
            {
                return BadRequest();
            }

            return Ok(done);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var token = await _authManager.Login(loginDto);
            
            if (token == null)
            {
                return NotFound("Wrong email or password");
            }


            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var role = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            var id = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;


            if (role == "Vendor")
            {

                var exists = await _vendorManager.GetById(id);


                if (exists == null)
                { 
                _vendorManager.Add(new VendorAddDto
                {
                        id = id,
                });
                    
                }
            }
            //else if (role == "Customer")
            //{
            //    var exists = await _customerRepository.ExistsAsync(Guid.Parse(userId));
            //    if (!exists)
            //    {
            //        await _customerRepository.AddAsync(new Customer
            //        {
            //            Id = Guid.Parse(userId),
            //            // Add other defaults if needed
            //        });
            //    }
            //}

            return Ok(token);
        }
    }
}
    
