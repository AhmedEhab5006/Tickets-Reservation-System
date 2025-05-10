using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketsReservationSystem.BLL.Managers;
using TicketsReservationSystem.DAL.Models;
using TicketsReservationSystem.DAL.Repository;

namespace TicketsReservationSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {

        private IAdminManager _adminManager;

        public AdminController(IAdminManager adminManager)
        {
            _adminManager = adminManager;
        }

        //[HttpPost("register")]
        //public IActionResult RegisterAdmin(User admin)
        //{
        //    _adminRepo.AddAdmin(admin);
        //    return Ok("Admin Registered Successfully");
        //}

        [HttpGet("vendor-requests")]
        public IActionResult GetVendorRequests()
        {
            var pendingVendors = _adminManager.GetAllPendingVendors();
            if (pendingVendors.Count() > 0)
            {
                return Ok(pendingVendors);
            }

            return NotFound("No pending vendors");
        }

        [HttpPut("accept/{vendorId}")]
        public IActionResult AcceptVendor(string vendorId)
        {
            var done = _adminManager.ConfirmVendor(vendorId);
            if (done)
            {
                return Ok("Vendor Accepted");
            }
            return BadRequest("An Error occured may be vendor wasn't found");
        }

        [HttpPut("reject/{vendorId}")]
        public IActionResult RejectVendor(string vendorId)
        {
            var done = _adminManager.ConfirmVendor(vendorId);
            if (done)
            {
                return Ok("Vendor Rejected");
            }
            return BadRequest("An Error occured may be vendor wasn't found");
        }

        [HttpGet("GetPendingSport")]
        public IActionResult GetPendingSportEvent()
        {
            var found = _adminManager.GetPendingSportEvents();
            if (found.Count() > 0)
            {
                return Ok(found);
            }
            return NotFound("There is no pending events");
        }

        [HttpGet("GetPendingEntertainment")]
        public IActionResult GetPendingEntertainmentEvent()
        {
            var found = _adminManager.GetPendingEntertainmentEvents();
            if (found.Count() > 0)
            {
                return Ok(found);
            }
            return NotFound("There is no pending events");
        }

        [HttpPut("AcceptEvent/{id}")]
        public IActionResult AcceptEvent (int id)
        {
            var done = _adminManager.AcceptEvent(id);
            if (done)
            {
                return Ok("Accepted");
            }
            return BadRequest("An error occured may be event doesn't exist");
        }

        [HttpPut("RejectEvent/{id}")]
        public IActionResult RejectEvent(int id)
        {
            var done = _adminManager.RejectEvent(id);
            if (done)
            {
                return Ok("Rejected");
            }
            return BadRequest("An error occured may be event doesn't exist");
        }
    }
}
