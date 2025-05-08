//using Microsoft.AspNetCore.Mvc;
//using TicketsReservationSystem.DAL.Models;
//using TicketsReservationSystem.DAL.Repository;

//namespace TicketsReservationSystem.API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class AdminController : ControllerBase
//    {
//        private readonly IAdminRepository _adminRepo;
//        private readonly IVendorRepository _vendorRepo;

//        public AdminController(IAdminRepository adminRepo, IVendorRepository vendorRepo)
//        {
//            _adminRepo = adminRepo;
//            _vendorRepo = vendorRepo;
//        }

//        [HttpPost("register")]
//        public IActionResult RegisterAdmin(User admin)
//        {
//            _adminRepo.AddAdmin(admin);
//            return Ok("Admin Registered Successfully");
//        }

//        [HttpGet("vendor-requests")]
//        public IActionResult GetVendorRequests()
//        {
//            var pendingVendors = _vendorRepo.GetAllPendingVendors()
//                .Where(v => v.acceptanceStatus == "Pending")
//                .ToList();

//            return Ok(pendingVendors);
//        }

//        [HttpPost("vendor/accept/{vendorId}")]
//        public IActionResult AcceptVendor(int vendorId)
//        {
//            _adminRepo.ConfirmVendor(vendorId);
//            return Ok("Vendor Accepted");
//        }

//        [HttpPost("vendor/reject/{vendorId}")]
//        public IActionResult RejectVendor(int vendorId)
//        {
//            _adminRepo.RejectVendor(vendorId);
//            return Ok("Vendor Rejected");
//        }
//    }
//}
