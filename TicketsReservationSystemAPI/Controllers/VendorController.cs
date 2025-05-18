using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;
using System.Security.Claims;
using TicketsReservationSystem.API.Helpers;
using TicketsReservationSystem.BLL.Dto_s;
using TicketsReservationSystem.BLL.Dto_s.ControllerDto;
using TicketsReservationSystem.BLL.Managers;
using TicketsReservationSystem.DAL.Database;
using TicketsReservationSystem.DAL.Models;

namespace TicketsReservationSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Vendor")]
    public class VendorController : ControllerBase
    {
        private IVendorManager _vendorManager;
        private IGetLoggedData _getLoggedData;
        private string vendorId;

        public VendorController(IVendorManager vendorManager, IGetLoggedData getLoggedData)
        {
            _vendorManager = vendorManager;
            _getLoggedData = getLoggedData;
        }

        [HttpPost("AddSportEvent")]
        public IActionResult AddSportEvent (SportAddDto sportAdd)
        {
            string acceptance = _vendorManager.GetAcceptanceStatus(_getLoggedData.GetId());

            if (acceptance != "Pending")
            {
                sportAdd.vendorId = _getLoggedData.GetId();
                _vendorManager.AddSportEvent(sportAdd);
                return Created(nameof(sportAdd.vendorId), new
                {
                    sportAdd.location,
                    sportAdd.numberOfSeats,
                    sportAdd.category,
                    sportAdd.date,
                    sportAdd.tournament,
                    sportAdd.tournamentStage,
                    sportAdd.sport,
                    sportAdd.team1,
                    sportAdd.team2,
                    sportAdd.team1Image,
                    sportAdd.team2Image,
                });

            }

            return Unauthorized("You don't have the permission to add an event");
        }

        [HttpPost("AddEntertainmentEvent")]
        public IActionResult AddEntertainmentEvent(EntertainmentAddDto entertainmentAdd)
        {
            string acceptance = _vendorManager.GetAcceptanceStatus(_getLoggedData.GetId());

            if (acceptance != "Pending")
            {
                entertainmentAdd.vendorId = _getLoggedData.GetId();
                _vendorManager.AddEntertainmentEvent(entertainmentAdd);
                return Created(nameof(entertainmentAdd.vendorId), new
                {
                    entertainmentAdd.location,
                    entertainmentAdd.numberOfSeats,
                    entertainmentAdd.category,
                    entertainmentAdd.date,
                    entertainmentAdd.showCategory,
                    entertainmentAdd.duration,
                    entertainmentAdd.ageRestriction,
                    entertainmentAdd.eventImage,
                    entertainmentAdd.genre,
                });

            }

            return Unauthorized("You don't have the permission to add an event");
        }

        [HttpPut("Event/{id}")]
        public IActionResult EditEvent(int id, EventUpdateDto Event)
        {

            Event.id = id;
            var found = _vendorManager.GetEventById(Event.id);
            vendorId = _getLoggedData.GetId();
            string acceptance = _vendorManager.GetAcceptanceStatus(_getLoggedData.GetId());

            if (acceptance == "Pending")
            {
                return Unauthorized("You Don't have the permission to edit an event");
            }


            if (found == null)
            {
                return NotFound("Desired Event isn't found");
            }


            if (found.vendorId != vendorId)
            {
                return Unauthorized("You cannot update this event");
            }

            _vendorManager.EditEvent(Event);
            return Ok("Updated");
        }

        [HttpPut("Entertainment/{id}")]
        public IActionResult EditEntertainmentEvent(int id, EntertainmentEventUpdateDto entertainmentEventUpdateDto)
        {

            entertainmentEventUpdateDto.id = id;
            var found = _vendorManager.GetEntertainmentEventById(entertainmentEventUpdateDto.id);

            vendorId = _getLoggedData.GetId();
            string acceptance = _vendorManager.GetAcceptanceStatus(_getLoggedData.GetId());

            if (acceptance == "Pending")
            {
                return Unauthorized("You Don't have the permission to edit an event");
            }

            if (found == null)
            {
                return NotFound("Desired Event isn't found");
            }

            if (_vendorManager.GetEventById(found.EventId).vendorId != vendorId)
            {
                return Unauthorized("You cannot update this event");
            }

            _vendorManager.EditEntertainmentEvent(entertainmentEventUpdateDto);
            return Ok("Updated");
        }
        [HttpPut("Sport/{id}")]
        public IActionResult EditSportsEvent(int id, SportEventUpdateDto SportsEvent)
        {


            SportsEvent.id = id;
            var found = _vendorManager.GetSportEventById(SportsEvent.id);


            vendorId = _getLoggedData.GetId();
            string acceptance = _vendorManager.GetAcceptanceStatus(_getLoggedData.GetId());

            if (acceptance == "Pending")
            {
                return Unauthorized("You Don't have the permission to edit an event");

            }

            if (found == null)
            {
                return NotFound("Desired Event isn't found");
            }

            if (_vendorManager.GetEventById(found.EventId).vendorId != vendorId)
            {
                return Unauthorized("You cannot update this event");
            }

            _vendorManager.EditSportsEvent(SportsEvent);
            return Ok("Updated");
        }

        [HttpDelete("Event/{id}")]
        public IActionResult Delete(int id)
        {
            var found = _vendorManager.GetEventById(id);


            vendorId = _getLoggedData.GetId();
            string acceptance = _vendorManager.GetAcceptanceStatus(_getLoggedData.GetId());

            if (acceptance == "Pending")
            {
                return Unauthorized("You Don't have the permission to delete an event");

            }

            if (found == null)
            {
                return NotFound("Desired Event isn't found");
            }

            if (found.vendorId != vendorId)
            {
                return Unauthorized("You cannot delete this event");
            }

            _vendorManager.DeleteEvent(id);
            return Ok("Updated");
        }

        [HttpGet("MyEntertainmentEvents")]
        public IActionResult GetMyEntertainmentEvent()
        {
            vendorId = _getLoggedData.GetId();
            var found = _vendorManager.GetMyEntertainmentEvent(vendorId).ToList();

            if (found.Count() > 0)
            {
                return Ok(found);
            }

            return NotFound("You Haven't Posted any events yet");
        }


        [HttpGet("MySportEvents")]
        public IActionResult GetMySportEvent()
        {
            vendorId = _getLoggedData.GetId();
            var found = _vendorManager.GetMySportEvent(vendorId).ToList();

            if (found.Count() > 0)
            {
                return Ok(found);
            }

            return NotFound("You Haven't posted any events yet");
        }
        [HttpPost("AddTicket")]
        public IActionResult AddTicket(TicketAddDto ticketAddDto)
        {
            var sucssed = _vendorManager.AddTicket(ticketAddDto);

            vendorId = _getLoggedData.GetId();
            string acceptance = _vendorManager.GetAcceptanceStatus(_getLoggedData.GetId());

            if (acceptance == "Pending")
            {
                return Unauthorized("You Don't have the permission to add a ticket");
            }


            if (_vendorManager.GetEventById(ticketAddDto.EventId).vendorId != vendorId)
            {
                return Unauthorized("You don't have a permission to add tickets to this event");
            }

            if (sucssed)
            {
                return Created(nameof(ticketAddDto), new
                {
                    ticketAddDto.EventId,
                    ticketAddDto.category,
                    ticketAddDto.avillableNumber,
                });
            }

            return BadRequest("desired avillable seats number is greater than event avillable seats");
        }

        [HttpPut("EditTicket/{id}")]
        public IActionResult EditTicket(int id, TicketUpdateDto ticketUpdateDto)
        {
            ticketUpdateDto.Id = id;

            vendorId = _getLoggedData.GetId();
            string acceptance = _vendorManager.GetAcceptanceStatus(_getLoggedData.GetId());

            if (acceptance == "Pending")
            {
                return Unauthorized("You Don't have the permission to update a ticket");
            }

            var sucssed = _vendorManager.UpdateTicket(ticketUpdateDto);
            if (sucssed)
            {
                return Ok("Updated");
            }

            return BadRequest("desired avillable seats number is greater than event avillable seats");
        }
        [HttpDelete("DeleteTicket/{id}")]
        public IActionResult DeleteTicket(int id)
        {

            vendorId = _getLoggedData.GetId();
            string acceptance = _vendorManager.GetAcceptanceStatus(_getLoggedData.GetId());

            if (acceptance == "Pending")
            {
                return Unauthorized("You Don't have the permission to delete a ticket");
            }

            var found = _vendorManager.GetTicketById(id);
            if (found != null)
            {
                _vendorManager.DeleteTicket(id);
                return Ok();
            }

            return NotFound("Desired Ticket not found");

        }

        [HttpGet("GetMyTickets")]
        public IActionResult GetMyTickets()
        {
            var found = _vendorManager.GetMyTicket(_getLoggedData.GetId());
            if (found.Count() > 0)
            {
                return Ok(found);
            }

            return NotFound("No tickets to show");
        }
    }
}
