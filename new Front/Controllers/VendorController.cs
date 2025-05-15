using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication2.Models;
using System.Text.Json;
//using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Json;
using WebApplication2.ViewModels;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace WebApplication2.Controllers
{
    public class VendorController : Controller
    {
        private readonly ILogger<VendorController> _vendorLogger;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _vendorConfiguration;
        private readonly string _vendorApiBaseUrl;
        private readonly string _vendorUploadPath;

        public VendorController(ILogger<VendorController> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _vendorLogger = logger;
            _vendorConfiguration = configuration;
            _httpClient = httpClientFactory.CreateClient();
            _vendorApiBaseUrl = _vendorConfiguration["ApiSettings:BaseUrl"] ?? "https://localhost:7110/";
            _httpClient.BaseAddress = new Uri(_vendorApiBaseUrl);
            _vendorUploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
        }

        public async Task<IActionResult> DashBoard()
        {
            VendorDashboardVM viewModel = new VendorDashboardVM();

            try
            {
                // Get the JWT token from session
                var token = HttpContext.Session.GetString("JWTToken");
                if (string.IsNullOrEmpty(token))
                {
                    TempData["ErrorMessage"] = "Not authenticated. Please log in again.";
                    return RedirectToAction("Login", "Account");
                }

                // Add the token to the request headers
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                // Get sports events
                HttpResponseMessage sportsResponse = await _httpClient.GetAsync("api/Vendor/MySportEvents");
                if (sportsResponse.IsSuccessStatusCode)
                {
                    var sportsJson = await sportsResponse.Content.ReadAsStringAsync();
                    viewModel.SportsEvents = JsonConvert.DeserializeObject<List<SportEventVM>>(sportsJson);

                    //Map the date parts to the separate fields
                    //foreach (var sportEvent in viewModel.SportsEvents)
                    //{
                    //    if (sportEvent.date != null)
                    //    {
                    //        sportEvent.Day = sportEvent.date.Value.Day;
                    //        sportEvent.Month = sportEvent.date.Value.Month;
                    //        sportEvent.Year = sportEvent.date.Value.Year;
                    //        sportEvent.Hour = sportEvent.date.Value.Hour;
                    //        sportEvent.Minute = sportEvent.date.Value.Minute;
                    //    }
                    //    // numberOfSeats is already mapped by the API, but ensure it's set
                    //    // If you want to map available seats, do it here as well
                    //}
                }
                else
                {
                    _vendorLogger.LogWarning($"Failed to get sports events. Status code: {sportsResponse.StatusCode}");
                    viewModel.SportsEvents = new List<SportEventVM>();
                }

                // Get entertainment events
                HttpResponseMessage entertainmentResponse = await _httpClient.GetAsync("api/Vendor/MyEntertainmentEvents");
                if (entertainmentResponse.IsSuccessStatusCode)
                {
                    var entertainmentJson = await entertainmentResponse.Content.ReadAsStringAsync();
                    viewModel.EntertainmentEvents = JsonConvert.DeserializeObject<List<EntertainmentVM>>(entertainmentJson);
                }
                else
                {
                    _vendorLogger.LogWarning($"Failed to get entertainment events. Status code: {entertainmentResponse.StatusCode}");
                    viewModel.EntertainmentEvents = new List<EntertainmentVM>();
                }
            }
            catch (Exception ex)
            {
                _vendorLogger.LogError($"Error loading dashboard data: {ex.Message}");
                viewModel.SportsEvents = new List<SportEventVM>();
                viewModel.EntertainmentEvents = new List<EntertainmentVM>();
                TempData["ErrorMessage"] = "An error occurred while loading the dashboard. Please try again.";
            }

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult AddSportEvent()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddSportEvent(SportEventVM model, IFormFile team1Image_upload, IFormFile team2Image_upload)
        {
            try
            {
                // Get the JWT token from session
                var token = HttpContext.Session.GetString("JWTToken");
                if (string.IsNullOrEmpty(token))
                {
                    ModelState.AddModelError("", "Not authenticated. Please log in again.");
                    return View(model);
                }

                // Set default values
                model.category = "Sport";
                model.status = "Pending";

                // Validate and use the event date
                if (model.date < DateTime.Now)
                {
                    ModelState.AddModelError("", "Event date must be in the future");
                    return View(model);
                }

                // Handle team1 image upload
                if (team1Image_upload != null)
                {
                    var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(team1Image_upload.FileName)}";
                    var filePath = Path.Combine(_vendorUploadPath, "teams", fileName);
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await team1Image_upload.CopyToAsync(stream);
                    }
                    model.team1Image = "/images/teams/" + fileName;
                }
                else
                {
                    ModelState.AddModelError("team1Image_upload", "Team 1 image is required");
                    return View(model);
                }

                // Handle team2 image upload
                if (team2Image_upload != null)
                {
                    var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(team2Image_upload.FileName)}";
                    var filePath = Path.Combine(_vendorUploadPath, "teams", fileName);
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await team2Image_upload.CopyToAsync(stream);
                    }
                    model.team2Image = "/images/teams/" + fileName;
                }
                else
                {
                    ModelState.AddModelError("team2Image_upload", "Team 2 image is required");
                    return View(model);
                }

                // Add the token to the request headers
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                // Validate model after setting all required values
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage);
                    _vendorLogger.LogError($"Model validation errors: {string.Join(", ", errors)}");
                    return View(model);
                }

                // Send the request to the API
                var response = await _httpClient.PostAsJsonAsync("api/Vendor/AddSportEvent", model);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Sports event added successfully!";
                    return RedirectToAction("DashBoard");
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _vendorLogger.LogError($"API Error: Status code: {response.StatusCode}, Content: {errorContent}");
                    ModelState.AddModelError("", $"Failed to add sports event. Status: {response.StatusCode}, Error: {errorContent}");
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                _vendorLogger.LogError($"Error adding sports event: {ex.Message}");
                ModelState.AddModelError("", $"An error occurred while adding the sports event: {ex.Message}");
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult AddEntertainmentEvent()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddEntertainmentEvent(EntertainmentVM model, IFormFile eventImage_upload)
        {
            try
            {
                // Set default values before validation
                model.category = "Entertainment";
                model.status = "Pending";

                // Construct the DateTime from components
                try
                {
                    model.Date = new DateTime(model.Year, model.Month, model.Day, model.Hour, model.Minute, 0);
                }
                catch (ArgumentOutOfRangeException)
                {
                    ModelState.AddModelError("Date", "Invalid date or time values");
                    return View(model);
                }

                // Validate date
                if (model.Date < DateTime.Now)
                {
                    ModelState.AddModelError("Date", "Event date must be in the future");
                    return View(model);
                }

                // Handle event image upload
                if (eventImage_upload != null)
                {
                    var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(eventImage_upload.FileName)}";
                    var filePath = Path.Combine(_vendorUploadPath, "events", fileName);
                    
                    // Ensure directory exists
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                    
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await eventImage_upload.CopyToAsync(stream);
                    }
                    model.eventImage = "/images/events/" + fileName;
                }
                else
                {
                    ModelState.AddModelError("eventImage", "Event image is required");
                    return View(model);
                }

                // Validate model after setting all required values
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                // Send the request to the API
                var response = await _httpClient.PostAsJsonAsync("api/Vendor/AddEntertainmentEvent", model);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Entertainment event added successfully!";
                    return RedirectToAction("DashBoard");
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _vendorLogger.LogError($"API Error: Status code: {response.StatusCode}, Content: {errorContent}");
                    ModelState.AddModelError("", $"Failed to add entertainment event. Status: {response.StatusCode}, Error: {errorContent}");
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                _vendorLogger.LogError($"Error adding entertainment event: {ex.Message}");
                ModelState.AddModelError("", $"An error occurred while adding the entertainment event: {ex.Message}");
                return View(model);
            }
        }

        public IActionResult Update_Event()
        {
            return View();
        }

        public IActionResult Delete_Event()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> DeleteSportEvent(int id)
        {
            try
            {
                // Get the JWT token from session
                var token = HttpContext.Session.GetString("JWTToken");
                if (string.IsNullOrEmpty(token))
                {
                    TempData["ErrorMessage"] = "Not authenticated. Please log in again.";
                    return RedirectToAction("Login", "Account");
                }

                // Add the token to the request headers
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                // Send delete request to API
                var response = await _httpClient.DeleteAsync($"api/Vendor/Event/{id}");

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Sports event deleted successfully!";
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _vendorLogger.LogError($"API Error: Status code: {response.StatusCode}, Content: {errorContent}");
                    TempData["ErrorMessage"] = $"Failed to delete sports event. Status: {response.StatusCode}";
                }
            }
            catch (Exception ex)
            {
                _vendorLogger.LogError($"Error deleting sports event: {ex.Message}");
                TempData["ErrorMessage"] = $"An error occurred while deleting the sports event: {ex.Message}";
            }

            return RedirectToAction("DashBoard");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteEntertainmentEvent(int id)
        {
            try
            {
                // Get the JWT token from session
                var token = HttpContext.Session.GetString("JWTToken");
                if (string.IsNullOrEmpty(token))
                {
                    TempData["ErrorMessage"] = "Not authenticated. Please log in again.";
                    return RedirectToAction("Login", "Account");
                }

                // Add the token to the request headers
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                // Send delete request to API
                var response = await _httpClient.DeleteAsync($"api/Vendor/Event/{id}");

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Entertainment event deleted successfully!";
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _vendorLogger.LogError($"API Error: Status code: {response.StatusCode}, Content: {errorContent}");
                    TempData["ErrorMessage"] = $"Failed to delete entertainment event. Status: {response.StatusCode}";
                }
            }
            catch (Exception ex)
            {
                _vendorLogger.LogError($"Error deleting entertainment event: {ex.Message}");
                TempData["ErrorMessage"] = $"An error occurred while deleting the entertainment event: {ex.Message}";
            }

            return RedirectToAction("DashBoard");
        }

        [HttpGet]
        public async Task<IActionResult> EditSportEvent(int id)
        {
            try
            {
                // Get the JWT token from session
                var token = HttpContext.Session.GetString("JWTToken");
                if (string.IsNullOrEmpty(token))
                {
                    TempData["ErrorMessage"] = "Not authenticated. Please log in again.";
                    return RedirectToAction("Login", "Account");
                }

                // Add the token to the request headers
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                // Get the event details
                var response = await _httpClient.GetAsync($"api/Vendor/Sport/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    _vendorLogger.LogInformation($"API Response for Sport Event {id}: {json}");
                    var sportEvent = JsonConvert.DeserializeObject<SportEventVM>(json);
                    return View(sportEvent);
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _vendorLogger.LogError($"API Error: Status code: {response.StatusCode}, Content: {errorContent}");
                    TempData["ErrorMessage"] = "Failed to load sports event details.";
                    return RedirectToAction("DashBoard");
                }
            }
            catch (Exception ex)
            {
                _vendorLogger.LogError($"Error loading sports event: {ex.Message}");
                TempData["ErrorMessage"] = "An error occurred while loading the sports event.";
                return RedirectToAction("DashBoard");
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditSportEvent(int id, SportEventVM model)
        {
            try
            {
                // Get the JWT token from session
                var token = HttpContext.Session.GetString("JWTToken");
                if (string.IsNullOrEmpty(token))
                {
                    TempData["ErrorMessage"] = "Not authenticated. Please log in again.";
                    return RedirectToAction("Login", "Account");
                }

                // Add the token to the request headers
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                // Send update request to API
                var response = await _httpClient.PutAsJsonAsync($"api/Vendor/Sport/{id}", model);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Sports event updated successfully!";
                    return RedirectToAction("DashBoard");
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _vendorLogger.LogError($"API Error: Status code: {response.StatusCode}, Content: {errorContent}");
                    ModelState.AddModelError("", $"Failed to update sports event. Status: {response.StatusCode}");
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                _vendorLogger.LogError($"Error updating sports event: {ex.Message}");
                ModelState.AddModelError("", $"An error occurred while updating the sports event: {ex.Message}");
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditEntertainmentEvent(int id)
        {
            try
            {
                // Get the JWT token from session
                var token = HttpContext.Session.GetString("JWTToken");
                if (string.IsNullOrEmpty(token))
                {
                    TempData["ErrorMessage"] = "Not authenticated. Please log in again.";
                    return RedirectToAction("Login", "Account");
                }

                // Add the token to the request headers
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                // Get the event details
                var response = await _httpClient.GetAsync($"api/Vendor/Entertainment/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var entertainmentEvent = JsonConvert.DeserializeObject<EntertainmentVM>(json);
                    return View(entertainmentEvent);
                }
                else
                {
                    TempData["ErrorMessage"] = "Failed to load entertainment event details.";
                    return RedirectToAction("DashBoard");
                }
            }
            catch (Exception ex)
            {
                _vendorLogger.LogError($"Error loading entertainment event: {ex.Message}");
                TempData["ErrorMessage"] = "An error occurred while loading the entertainment event.";
                return RedirectToAction("DashBoard");
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditEntertainmentEvent(int id, EntertainmentVM model)
        {
            try
            {
                // Get the JWT token from session
                var token = HttpContext.Session.GetString("JWTToken");
                if (string.IsNullOrEmpty(token))
                {
                    TempData["ErrorMessage"] = "Not authenticated. Please log in again.";
                    return RedirectToAction("Login", "Account");
                }

                // Add the token to the request headers
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                // Send update request to API
                var response = await _httpClient.PutAsJsonAsync($"api/Vendor/Entertainment/{id}", model);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Entertainment event updated successfully!";
                    return RedirectToAction("DashBoard");
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _vendorLogger.LogError($"API Error: Status code: {response.StatusCode}, Content: {errorContent}");
                    ModelState.AddModelError("", $"Failed to update entertainment event. Status: {response.StatusCode}");
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                _vendorLogger.LogError($"Error updating entertainment event: {ex.Message}");
                ModelState.AddModelError("", $"An error occurred while updating the entertainment event: {ex.Message}");
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> sign_in(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }

            try
            {
                // Admin login handling
                if (loginVM.email == "admin@example.com" && loginVM.password == "Admin@123")
                {
                    // Generate a simple token for admin (you might want to generate a proper JWT token)
                    var adminToken = GenerateAdminToken();
                    
                    // Store admin session data
                    HttpContext.Session.SetString("UserRole", "Admin");
                    HttpContext.Session.SetString("UserEmail", loginVM.email);
                    HttpContext.Session.SetString("JWTToken", adminToken);
                    
                    return RedirectToAction("AdminPage", "Admin");
                }

                // Regular user login handling
                var response = await _httpClient.PostAsJsonAsync($"api/Auth/Login", loginVM);

                if (response.IsSuccessStatusCode)
                {
                    var token = await response.Content.ReadAsStringAsync();
                    
                    if (string.IsNullOrEmpty(token))
                    {
                        ModelState.AddModelError("", "Empty response from server");
                        return View(loginVM);
                    }

                    try
                    {
                        var handler = new JwtSecurityTokenHandler();
                        var jwtToken = handler.ReadJwtToken(token);
                        var roleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                        var emailClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

                        if (string.IsNullOrEmpty(roleClaim))
                        {
                            ModelState.AddModelError("", "Invalid token: Missing role claim");
                            return View(loginVM);
                        }

                        // Store session data
                        HttpContext.Session.SetString("UserRole", roleClaim);
                        HttpContext.Session.SetString("UserEmail", emailClaim ?? loginVM.email);
                        HttpContext.Session.SetString("JWTToken", token);

                        // Redirect based on role
                        return roleClaim.ToLower() switch
                        {
                            "vendor" => RedirectToAction("DashBoard", "Vendor"),
                            "client" => RedirectToAction("Sport_events", "Event"),
                            "admin" => RedirectToAction("AdminPage", "Admin"),
                            _ => RedirectToAction("Index", "Home")
                        };
                    }
                    catch (Exception ex)
                    {
                        _vendorLogger.LogError($"Token parsing error: {ex.Message}");
                        ModelState.AddModelError("", "Invalid token format");
                        return View(loginVM);
                    }
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError("", errorContent ?? "Login failed. Please check your credentials.");
                    return View(loginVM);
                }
            }
            catch (Exception ex)
            {
                _vendorLogger.LogError($"Login error: {ex.Message}");
                ModelState.AddModelError("", "An error occurred during login. Please try again.");
                return View(loginVM);
            }
        }

        // Helper method to generate a simple token for admin
        private string GenerateAdminToken()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_vendorConfiguration["JWT:Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Role, "Admin"),
                    new Claim(ClaimTypes.Email, "admin@example.com")
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