using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketsReservationSystem.BLL.Dto_s;
using TicketsReservationSystem.BLL.Managers;
using TicketsReservationSystem.API.Helpers;

namespace TicketsReservationSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Client")]
    public class ClientController : ControllerBase
    {
        private IClientManager _clientManager;
        private readonly IGetLoggedData _getLoggedData;

        public ClientController(IClientManager clientManager, IGetLoggedData getLoggedData)
        {
            _clientManager = clientManager;
            _getLoggedData = getLoggedData;
        }


        [HttpGet("SportEvents")]
        [AllowAnonymous]
        public IActionResult GetSportEvents()
        {
            var found = _clientManager.GetSportEvents();

            if (found != null && found.Count() > 0)  
            {
                return Ok(found);
            }

            return NotFound("No Events to show");
        }

        [HttpGet("EntertainmentEvents")]
        [AllowAnonymous]
        public IActionResult GetEntertainmentEvents()
        {
            var found = _clientManager.GetEntertainmentEvents();

            if (found != null && found.Count() > 0)
            {
                return Ok(found);
            }

            return NotFound("No Events to show");
        }

        [HttpGet("GetAddress")]
        public IActionResult GetAddress()
        {
            var found = _clientManager.GetMyAddress(_getLoggedData.GetId());
            if (found != null)
            {
                return Ok(found);
            }

            return NotFound("No Address Found");
        }

        [HttpPut("UpdateAddress")]
        public IActionResult EditAddress(AddressUpdateDto addressUpdateDto)
        {
            _clientManager.EditAddress(addressUpdateDto , _getLoggedData.GetId());
            return Ok("Updated");

        }
        [HttpPost("Book/{ticketId}")]
        public IActionResult Book(ReservationAddDto reservationAddDto , int ticketId)
        {
            reservationAddDto.clientId = _getLoggedData.GetId();
            reservationAddDto.shippingAddressId = _clientManager.GetMyAddress(_getLoggedData.GetId()).id;
            reservationAddDto.ticketId = ticketId;
            
            var done = _clientManager.Book(reservationAddDto);
            if (done)
            {
                return Ok("Reservation done");
            }

            return BadRequest("An error occured may be you booked more than 5");

        }

        [HttpDelete("Cancel/{ReservationId}")]
        public IActionResult Delete(int ReservationId)
        {
            _clientManager.CancelBooking(ReservationId);
            return NoContent();

        }

        [HttpGet("ViewReservations")]
        public IActionResult GetAll()
        {
            var found = _clientManager.GetClientBookings(_getLoggedData.GetId());

            if (found.Count() > 0)
            {
                return Ok(found);
            }

            return NotFound("No Reservations yet");
        }
        [HttpGet("GetEventTickets/{eventId}")]
        public IActionResult GetTickets (int eventId)
        {
            var found = _clientManager.GetEventTickets(eventId).ToList();
            if (found.Count() > 0)
            {
                return Ok(found);
            }

            return NotFound("No tickets");
        }

    }
}
