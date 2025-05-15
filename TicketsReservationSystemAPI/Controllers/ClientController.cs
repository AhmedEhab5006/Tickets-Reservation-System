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

        [HttpPost("Booking/{ticketId}")]
        public IActionResult BookTicket(int ticketId)
        {
            var clientId = _getLoggedData.GetId(); // from JWT

            // Call the Manager method to book the ticket
            bool bookingSuccessful = _clientManager.Book(ticketId, clientId);

            if (!bookingSuccessful)
            {
                // Return a BadRequest if the ticket is not available or booking fails
                return BadRequest("Failed to book the ticket. Please ensure the ticket is available.");
            }

            return Ok("Ticket booked successfully.");
        }

        [HttpPost("CancelBooking/{ticketId}")]
        public IActionResult CancelBooking(int ticketId)
        {
            var clientId = _getLoggedData.GetId(); // from JWT
            if (string.IsNullOrEmpty(clientId))
                return Unauthorized("Client not authenticated.");

            bool result = _clientManager.CancelBooking(ticketId, clientId);

            if (!result)
                return BadRequest("Cancellation failed. Ticket may not exist or is not booked.");

            return Ok("Ticket booking cancelled successfully.");
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

        [HttpPut("Edit-address")]
        public async Task<IActionResult> EditAddress([FromBody] AddressUpdateDto dto)
        {
            var clientId = _getLoggedData.GetId();

            if (string.IsNullOrEmpty(clientId))
                return Unauthorized("Client not authenticated.");

            bool result = await _clientManager.EditAddressAsync(clientId, dto);

            if (!result)
                return NotFound("Client or address not found.");

            return Ok("Address updated successfully.");
        }

        [HttpGet("{clientId}/ViewBookings")]
        public async Task<ActionResult<List<ClientBookingDto>>> ViewBookings(string clientId)
        {
            var bookings = await _clientManager.ViewBookingsAsync(clientId); 

            if (bookings == null || bookings.Count == 0)
            {
                return NotFound("No bookings found for this client.");
            }

            return Ok(bookings);
        }
    }
}
