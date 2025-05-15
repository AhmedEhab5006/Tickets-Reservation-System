using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using WebApplication2.ViewModels;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApplication2.Controllers

{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;

        public AdminController(ILogger<AdminController> logger, HttpClient httpClient, IConfiguration configuration)
        {
            _logger = logger;
            _client = httpClient;
            _client.BaseAddress = new Uri("https://localhost:7110/");
            _configuration = configuration;
        }
        public async Task<IActionResult> VendorRequests()
        {
            try
            {
                // Check if user is logged in and is an admin
                var userRole = HttpContext.Session.GetString("UserRole");
                if (userRole != "Admin")
                {
                    return RedirectToAction("sign_in", "User");
                }

                // Get the token from session
                var token = HttpContext.Session.GetString("JWTToken");
                if (string.IsNullOrEmpty(token))
                {
                    return RedirectToAction("sign_in", "User");
                }

                // Set the authorization header with the token from session
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _client.GetAsync("api/Admin/vendor-requests");
                if (response.IsSuccessStatusCode)
                {
                    var vendors = await response.Content.ReadFromJsonAsync<List<ApplicationUserVM>>();
                    return View(vendors);
                }

                // Log the error for debugging
                _logger.LogError($"API Error: Status code: {response.StatusCode}");
                ViewBag.Error = "No pending vendors found.";
                return View(new List<ApplicationUserVM>());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in VendorRequests: {ex.Message}");
                ViewBag.Error = "An error occurred while fetching vendor requests.";
                return View(new List<ApplicationUserVM>());
            }
        }


        public IActionResult AdminPage()
        {
            return View();
        }

        public async Task<IActionResult> Users()
        {
            var response = await _client.GetAsync("api/Admin/Users");
            if (response.IsSuccessStatusCode)
            {
                var users = await response.Content.ReadFromJsonAsync<List<UserVM>>();
                return View(users);
            }
            return View(new List<UserVM>());
        }

        public async Task<IActionResult> Events()
        {
            var response = await _client.GetAsync("api/Admin/Events");
            if (response.IsSuccessStatusCode)
            {
                var events = await response.Content.ReadFromJsonAsync<List<EventVM>>();
                return View(events);
            }
            return View(new List<EventVM>());
        }

        public async Task<IActionResult> PendingSport()
        {
            var response = await _client.GetAsync("api/Admin/GetPendingSport");
            if (response.IsSuccessStatusCode)
            {
                var events = await response.Content.ReadFromJsonAsync<List<FullDetailSportEventReadDto>>();
                return View(events);
            }
            ViewBag.Error = "No pending sport events found.";
            return View(new List<FullDetailSportEventReadDto>());
        }

        public async Task<IActionResult> PendingEntertainment()
        {
            var response = await _client.GetAsync("api/Admin/GetPendingEntertainment");
            if (response.IsSuccessStatusCode)
            {
                var events = await response.Content.ReadFromJsonAsync<List<FullDetailEntertainmentEventReadDto>>();
                return View(events);
            }
            ViewBag.Error = "No pending entertainment events found.";
            return View(new List<FullDetailEntertainmentEventReadDto>());
        }

        [HttpGet]
        public IActionResult AcceptVendor() => View();

        [HttpPost]
        public async Task<IActionResult> AcceptVendor(string vendorId)
        {
            try
            {
                // Check if user is logged in and is an admin
                var userRole = HttpContext.Session.GetString("UserRole");
                if (userRole != "Admin")
                {
                    return RedirectToAction("sign_in", "User");
                }

                // Get the token from session
                var token = HttpContext.Session.GetString("JWTToken");
                if (string.IsNullOrEmpty(token))
                {
                    return RedirectToAction("sign_in", "User");
                }

                // Set the authorization header with the token from session
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _client.PutAsync($"api/Admin/accept/{vendorId}", null);
                if (response.IsSuccessStatusCode)
                {
                    TempData["Result"] = "Vendor accepted successfully.";
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    TempData["Error"] = errorContent ?? "Failed to accept vendor.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error accepting vendor: {ex.Message}");
                TempData["Error"] = "An error occurred while accepting the vendor.";
            }

            return RedirectToAction("VendorRequests");
        }

        [HttpGet]
        public IActionResult RejectVendor() => View();

        [HttpPost]
        public async Task<IActionResult> RejectVendor(string vendorId)
        {
            try
            {
                // Check if user is logged in and is an admin
                var userRole = HttpContext.Session.GetString("UserRole");
                if (userRole != "Admin")
                {
                    return RedirectToAction("sign_in", "User");
                }

                // Get the token from session
                var token = HttpContext.Session.GetString("JWTToken");
                if (string.IsNullOrEmpty(token))
                {
                    return RedirectToAction("sign_in", "User");
                }

                // Set the authorization header with the token from session
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _client.PutAsync($"api/Admin/reject/{vendorId}", null);
                if (response.IsSuccessStatusCode)
                {
                    TempData["Result"] = "Vendor rejected successfully.";
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    TempData["Error"] = errorContent ?? "Failed to reject vendor.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error rejecting vendor: {ex.Message}");
                TempData["Error"] = "An error occurred while rejecting the vendor.";
            }

            return RedirectToAction("VendorRequests");
        }

        [HttpGet]
        public IActionResult AcceptEvent() => View();
        [HttpPost]
        public async Task<IActionResult> AcceptEvent(int eventId)
        {
            var response = await _client.PutAsync($"api/Admin/AcceptEvent/{eventId}", null);
            ViewBag.Result = response.IsSuccessStatusCode ? "Event accepted." : "Failed to accept event.";
            return View();
        }

        [HttpGet]
        public IActionResult RejectEvent() => View();
        [HttpPost]
        public async Task<IActionResult> RejectEvent(int eventId)
        {
            var response = await _client.PutAsync($"api/Admin/RejectEvent/{eventId}", null);
            ViewBag.Result = response.IsSuccessStatusCode ? "Event rejected." : "Failed to reject event.";
            return View();
        }
    }
}
