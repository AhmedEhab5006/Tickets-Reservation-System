using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TicketsReservationSystem.BLL.Dto_s.AuthDto_s;

namespace TicketsReservationSystem.API.Filters
{
    public class LoginFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var loginDto = context.ActionArguments["loginDto"] as LoginDto;

            if(string.IsNullOrWhiteSpace(loginDto.email) || string.IsNullOrWhiteSpace(loginDto.password))
            {
                context.ModelState.AddModelError("", "Missing info");
                context.Result = new BadRequestObjectResult(context.ModelState);
            } 
        }
    }
}
