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
                var userRole = HttpContext.Session.GetString("UserRole");
                if (userRole != "Admin")
                {
                    return RedirectToAction("sign_in", "User");
                }

                var token = HttpContext.Session.GetString("JWTToken");
                if (string.IsNullOrEmpty(token))
                {
                    return RedirectToAction("sign_in", "User");
                }

             //   _client.DefaultRequestHeaders.Remove("Authorization");
                
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
            var response = await _client.PutAsync($"api/Admin/accept/{vendorId}", null);
            ViewBag.Result = response.IsSuccessStatusCode ? "Vendor accepted." : "Failed to accept vendor.";
            return View();
        }

        [HttpGet]
        public IActionResult RejectVendor() => View();
        [HttpPost]
        public async Task<IActionResult> RejectVendor(string vendorId)
        {
            var response = await _client.PutAsync($"api/Admin/reject/{vendorId}", null);
            ViewBag.Result = response.IsSuccessStatusCode ? "Vendor rejected." : "Failed to reject vendor.";
            return View();
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

        private string GenerateAdminToken()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JWT:Secret"] ?? "ksfjskfjwiejwiefjwoinewimxiwncowinwinecwc");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Role, "Admin"),
                    new Claim(ClaimTypes.Email, "admin@example.com"),
                    new Claim(ClaimTypes.NameIdentifier, "admin-id")
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
