using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketsReservationSystem.BLL.Managers;
using TicketsReservationSystem.DAL.Repository;

namespace TicketsReservationSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Client")]
    public class ClientController : ControllerBase
    {
        private IClientManager _clientManager;

        public ClientController(IClientManager clientManager) {
            _clientManager = clientManager;
        }
        
        [HttpGet("SportEvents")]
        [AllowAnonymous]
        public IActionResult GetSportEvents()
        {
            var found = _clientManager.GetSportEvents();

            if (found != null)
            {
                return Ok(found);
            }

            return Ok("No Events to show");
        }

        [HttpGet("EntertainmentEvents")]
        [AllowAnonymous]
        public IActionResult GetEntertainmentEvents()
        {
            var found = _clientManager.GetEntertainmentEvents();
            
            if(found != null)
            {
                return Ok(found);
            }

            return Ok("No Events to show");
        }
    }
}
