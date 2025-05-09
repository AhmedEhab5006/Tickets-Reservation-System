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

        public AuthController(IAuthManager authManager)
        {
            _authManager = authManager;
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

             return Ok(token);
        }
    }
}
    
