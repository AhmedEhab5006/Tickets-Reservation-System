using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TicketsReservationSystem.BLL.Dto_s;
using TicketsReservationSystem.BLL.Dto_s.ControllerDto;

namespace TicketsReservationSystem.API.Filters
{
    public class TicketsAddFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var tikcetAddDto = context.ActionArguments["ticketAddDto"] as TicketAddDto;


            if (string.IsNullOrWhiteSpace(tikcetAddDto.category) ||
                tikcetAddDto.avillableNumber <= 0 ||
                tikcetAddDto.price <= 0 )
            {
                context.ModelState.AddModelError("", "Missing info, make sure that all fields are complete");
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}
