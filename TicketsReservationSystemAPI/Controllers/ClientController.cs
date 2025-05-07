//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using TicketsReservationSystem.BLL.Dto_s;
//using TicketsReservationSystem.BLL.Managers;
//using TicketsReservationSystem.DAL.Models;
//using TicketsReservationSystem.DAL.Repository;

//namespace TicketsReservationSystem.API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    [Authorize(Roles = "Client")]
//    public class ClientController : ControllerBase
//    {
//        private IClientManager _clientManager;

//        public ClientController(IClientManager clientManager)
//        {
//            _clientManager = clientManager;
//        }

//        [HttpGet("SportEvents")]
//        [AllowAnonymous]
//        public IActionResult GetSportEvents()
//        {
//            var found = _clientManager.GetSportEvents();

//            if (found != null)
//            {
//                return Ok(found);
//            }

//            return Ok("No Events to show");
//        }

//        [HttpGet("EntertainmentEvents")]
//        [AllowAnonymous]
//        public IActionResult GetEntertainmentEvents()
//        {
//            var found = _clientManager.GetEntertainmentEvents();

//            if (found != null)
//            {
//                return Ok(found);
//            }

//            return Ok("No Events to show");
//        }

//        [HttpPost]
//        public IActionResult AddClient([FromBody] ClientAddDto clientAddDto)
//        {
//            var userId = int.Parse(User.FindFirst("UserId")?.Value ?? "0"); // Extract UserId from JWT token
//            _clientManager.Add(clientAddDto, userId);
//            return Ok("Client added successfully.");
//        }

//        //[HttpPost("Address")]
//        //public async Task<IActionResult> AddAddress([FromBody] AddressReadDto addressDto)
//        //{
//        //    await _clientManager.AddAddressAsync(addressDto);
//        //    return Ok("Address added successfully.");
//        //}




//        //[HttpPut("Address")]
//        //public IActionResult EditAddress([FromBody] Address address)
//        //{
//        //    _clientManager.EditAddress(address);
//        //    return Ok("Address updated successfully.");
//        //}

//        // Book a ticket
//        [HttpPost("Book/{ticketId}")]
//        public IActionResult BookTicket(int ticketId)
//        {
//            try
//            {
//                _clientManager.Book(ticketId);
//                return Ok("Ticket booked successfully.");
//            }
//            catch (InvalidOperationException ex)
//            {
//                return BadRequest(ex.Message);
//            }
//        }

//        // Cancel a ticket booking
//        [HttpPost("Cancel/{ticketId}")]
//        public IActionResult CancelBooking(int ticketId)
//        {
//            try
//            {
//                _clientManager.CancelBooking(ticketId);
//                return Ok("Ticket booking canceled successfully.");
//            }
//            catch (InvalidOperationException ex)
//            {
//                return BadRequest(ex.Message);
//            }
//        }

//        [HttpGet]
//        public async Task<IActionResult> GetAllClients()
//        {
//            var clients = await _clientManager.GetAllClientsAsync();
//            return Ok(clients);
//        }



//    }
//}
