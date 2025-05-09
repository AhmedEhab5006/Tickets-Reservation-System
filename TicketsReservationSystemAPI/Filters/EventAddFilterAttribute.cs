using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TicketsReservationSystem.BLL.Dto_s;
using TicketsReservationSystem.BLL.Dto_s.ControllerDto;

namespace TicketsReservationSystem.API.Filters
{
    public class EventAddFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var eventAddDto = context.ActionArguments["Event"] as FullEventAddDto;


            if (string.IsNullOrWhiteSpace(eventAddDto.Event.category) ||
                string.IsNullOrWhiteSpace(eventAddDto.Event.location) ||
                eventAddDto.Event.date == default ||
                eventAddDto.Event.numberOfSeats <= 0)
            {
                context.ModelState.AddModelError("", "Missing info, make sure that all fields are complete");
                context.Result = new BadRequestObjectResult(context.ModelState);
            }

            if (eventAddDto.EntertainmentEvent != null)
            {
                if (string.IsNullOrWhiteSpace(eventAddDto.EntertainmentEvent.performerName) ||
                    eventAddDto.EntertainmentEvent.duration <= 0 ||
                    string.IsNullOrWhiteSpace(eventAddDto.EntertainmentEvent.showCategory) ||
                    eventAddDto.EntertainmentEvent.ageRestriction <= 0 ||
                    string.IsNullOrWhiteSpace(eventAddDto.EntertainmentEvent.genre) ||
                    string.IsNullOrWhiteSpace(eventAddDto.EntertainmentEvent.eventImage))
                {
                    context.ModelState.AddModelError("", "Missing info, make sure that all fields are complete");
                    context.Result = new BadRequestObjectResult(context.ModelState);
                }

                if (eventAddDto.Event.category == "Sport")
                {
                    context.ModelState.AddModelError("", "This is an entertainment event please select entertainment category");
                    context.Result = new BadRequestObjectResult(context.ModelState);

                }
            }

            if (eventAddDto.SportsEvent != null)
            {
                if (string.IsNullOrWhiteSpace(eventAddDto.SportsEvent.sport) ||
                    string.IsNullOrWhiteSpace(eventAddDto.SportsEvent.tournamentStage) ||
                    string.IsNullOrWhiteSpace(eventAddDto.SportsEvent.tournament) ||
                    string.IsNullOrWhiteSpace(eventAddDto.SportsEvent.team1) ||
                    string.IsNullOrWhiteSpace(eventAddDto.SportsEvent.team2) || string.IsNullOrWhiteSpace(eventAddDto.SportsEvent.team1Image)
                    || string.IsNullOrWhiteSpace(eventAddDto.SportsEvent.team2Image))
                {
                    context.ModelState.AddModelError("", "Missing info, make sure that all fields are complete");
                    context.Result = new BadRequestObjectResult(context.ModelState);
                }


                if (eventAddDto.Event.category == "Entertainment")
                {
                    context.ModelState.AddModelError("", "This is an sport event please select sport category");
                    context.Result = new BadRequestObjectResult(context.ModelState);

                }
            }


            if (eventAddDto.SportsEvent != null && eventAddDto.EntertainmentEvent != null)
            {
                context.ModelState.AddModelError("", "You can't add sport and entertainment event per same request");
                context.Result = new BadRequestObjectResult(context.ModelState);
            }

        }

    }
}

