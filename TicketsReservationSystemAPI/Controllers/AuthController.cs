using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketsReservationSystem.BLL.Dto_s.AuthDto_s;
using TicketsReservationSystem.BLL.Managers.AuthManagers;
using TicketsReservationSystem.API.Helpers;
using Microsoft.Extensions.Caching.Memory;
using TicketsReservationSystem.API.Filters;



namespace TicketsReservationSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthManager _authManager;
        private IEmailSender _emailSender;

        public AuthController(IAuthManager authManager , IEmailSender emailSender)
        {
            _authManager = authManager;
            _emailSender = emailSender;
        }

        [HttpPost("Login")]
        [LoginFilter]
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
        [RegisterFilter]
        public async Task<ActionResult> Register(RegisterDto registerDto , int otpVerfiy)
        {
            int otp = 456789;

            if (await _authManager.GetEmailAndUsernameFromDB(registerDto.email , registerDto.firstname + registerDto.lastname))
            {
                return BadRequest("Email or username already taken");
            }
              
            if (otpVerfiy == 0)
            {
                await _emailSender.SendEmailAsync(registerDto.email, "Verfying your email", $"Your OTP is {otp}");
                return Ok("otp sent to your email");
            }

            if (otpVerfiy == otp)
            {
                var token = await _authManager.Register(registerDto);
                return Ok(new { token });

            }

            return BadRequest("Wrong OTP make sure that you entered a valid email");
        }
    }
}