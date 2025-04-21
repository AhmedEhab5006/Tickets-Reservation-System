using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketsReservationSystem.BLL.Dto_s.AuthDto_s;
using TicketsReservationSystem.BLL.Managers.AuthManagers;

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

        [HttpPost("Login")]
        public async Task<ActionResult> Login(LoginDto loginDto)
        {
            var logged = await _authManager.Login(loginDto);
            if (logged == null)
            {
                return Unauthorized("Wrong Email Or Password");
            }
            return Ok(logged);
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Register(RegisterDto registerDto)
        {
            try
            {
                var token = await _authManager.Register(registerDto);
                return Ok(new { token });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }

        }

    }
}