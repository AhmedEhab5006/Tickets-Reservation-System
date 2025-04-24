using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TicketsReservationSystem.BLL.Dto_s.AuthDto_s;
using TicketsReservationSystem.BLL.Dto_s.ControllerDto;
using TicketsReservationSystem.BLL.Managers;
using TicketsReservationSystem.DAL.Models;

namespace TicketsReservationSystem.API.Filters
{
    public class RegisterFilterAttribute : ActionFilterAttribute
    {   
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var registerDto = context.ActionArguments["registerDto"] as RegisterDto;
           
            var validRoles = new[] { "Admin", "Client", "Vendor" };

            if (!validRoles.Contains(registerDto.role))
            {
                context.ModelState.AddModelError("", "Invalid role");
                context.Result = new BadRequestObjectResult(context.ModelState);
            }

            if (registerDto.password.Length < 5)
            {
                context.ModelState.AddModelError("" , "Password must be greater than or equal 5 chars");
                context.Result = new BadRequestObjectResult(context.ModelState);
            }


        }
    }
}
