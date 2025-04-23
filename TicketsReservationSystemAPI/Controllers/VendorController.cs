using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;
using System.Security.Claims;
using TicketsReservationSystem.API.Filters;
using TicketsReservationSystem.API.Helpers;
using TicketsReservationSystem.BLL.Dto_s;
using TicketsReservationSystem.BLL.Dto_s.ControllerDto;
using TicketsReservationSystem.BLL.Managers;
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
        private int vendorId;

        public VendorController(IVendorManager vendorManager , IGetLoggedData getLoggedData) {
            _vendorManager = vendorManager;
            _getLoggedData = getLoggedData;
        }

        [HttpPost("Event")]
        [EventAddFilter]
        public IActionResult AddEvent(FullEventAddDto Event)
        {
            
            vendorId = _getLoggedData.GetId();
            string acceptance = _getLoggedData.GetVendorStatus(vendorId); 
            
            if (acceptance != "Pending")
            {
                Event.Event.vendorId = vendorId;
                _vendorManager.AddEvent(Event.Event, Event.EntertainmentEvent, Event.SportsEvent);
                return Created(nameof(Event.Event.vendorId), new
                {
                    Event.Event.location,
                    Event.Event.numberOfSeats,
                    Event.Event.category,
                    Event.Event.date
                });

            }

            return Unauthorized("You don't have the permission to add an event");


        }
        [HttpPut("Event")]
        public IActionResult EditEvent(int id , EventUpdateDto Event)
        {
            
            Event.id = id;
            var found = _vendorManager.GetEventById(Event.id);
            vendorId = _getLoggedData.GetId();
            string acceptance = _getLoggedData.GetVendorStatus(vendorId);
            
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

        [HttpPut("Entertainment")]
        public IActionResult EditEntertainmentEvent(int id , EntertainmentEventUpdateDto entertainmentEventUpdateDto){

            entertainmentEventUpdateDto.id = id;
            var found = _vendorManager.GetEntertainmentEventById(entertainmentEventUpdateDto.id);

            vendorId = _getLoggedData.GetId();
            string acceptance = _getLoggedData.GetVendorStatus(vendorId);

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
        [HttpPut("Sport")]
        public IActionResult EditSportsEvent(int id , SportEventUpdateDto SportsEvent){
            
            
            SportsEvent.id = id;
            var found = _vendorManager.GetSportEventById(SportsEvent.id);


            vendorId = _getLoggedData.GetId();
            string acceptance = _getLoggedData.GetVendorStatus(vendorId);

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

        [HttpDelete("Event")]
        public IActionResult Delete(int id)
        {
            var found = _vendorManager.GetEventById(id);


            vendorId = _getLoggedData.GetId();
            string acceptance = _getLoggedData.GetVendorStatus(vendorId);

            if (acceptance == "Pending")
            {
                return Unauthorized("You Don't have the permission to delete an event");

            }

            if (found == null)
            {
                return NotFound("Desired Event isn't found");
            }

            if (_vendorManager.GetEventById(found.vendorId).vendorId != vendorId)
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
            var found = _vendorManager.GetMyEntertainmentEvent(vendorId);
           
            if (found != null)
            {
                return Ok(found);
            }

            return NotFound("You Haven't Posted any events yet");
        }

        [HttpGet("MySportEvents")]
        public IActionResult GetMySportEvent()
        {
            vendorId = _getLoggedData.GetId();
            var found = _vendorManager.GetMySportEvent(vendorId);
            
            if(found != null)
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
            string acceptance = _getLoggedData.GetVendorStatus(vendorId);

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

        [HttpPut("EditTicket")]
        public IActionResult EditTicket(int id , TicketUpdateDto ticketUpdateDto)
        {
            ticketUpdateDto.Id = id;
            var sucssed = _vendorManager.UpdateTicket(ticketUpdateDto);
            if (sucssed)
            {
                return Created();
            }

            return BadRequest("desired avillable seats number is greater than event avillable seats");
        }
        [HttpDelete("DeleteTicket")]
        public IActionResult DeleteTicket (int id)
        {
            _vendorManager.DeleteTicket(id);
            return Ok();
        }
    }
}
