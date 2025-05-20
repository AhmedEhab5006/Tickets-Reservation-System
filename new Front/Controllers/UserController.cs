using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication2.Models;
using System.Text.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace WebApplication2.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public UserController(
            ILogger<UserController> logger,
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7110/");
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult sign_in()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View(registerVM);
            }

            //try
            //{
            var response = await _httpClient.PostAsJsonAsync($"api/Auth/Register", registerVM);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("sign_in");
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError("", errorContent ?? "Registration failed. Please try again.");
                return View(registerVM);
            }
        }
        //catch (Exception ex)
        //{
        //    _logger.LogError($"Registration error: {ex.Message}");
        //    ModelState.AddModelError("", "An error occurred during registration. Please try again.");
        //   return View(registerVM);
        // }
        //}




        [HttpPost]
        public async Task<IActionResult> sign_in(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }

            try
            {
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
                        var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                        if (string.IsNullOrEmpty(roleClaim))
                        {
                            ModelState.AddModelError("", "Invalid token: Missing role claim");
                            return View(loginVM);
                        }

                        // Store session data
                        HttpContext.Session.SetString("UserRole", roleClaim);
                        HttpContext.Session.SetString("UserEmail", emailClaim ?? loginVM.email);
                        HttpContext.Session.SetString("UserId", userIdClaim ?? "");
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
                        _logger.LogError($"Token parsing error: {ex.Message}");
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
                _logger.LogError($"Login error: {ex.Message}");
                ModelState.AddModelError("", "An error occurred during login. Please try again.");
                return View(loginVM);
            }
        }

        [HttpGet]
        public IActionResult Logout()
        {
            // Clear all session data
            HttpContext.Session.Clear();

            // Redirect to home page
            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
