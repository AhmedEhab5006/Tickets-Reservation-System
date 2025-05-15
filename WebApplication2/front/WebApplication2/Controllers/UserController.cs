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
                if (loginVM.email == "admin@example.com" && loginVM.password == "Admin@123")
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
                    var tokenString = tokenHandler.WriteToken(token);
                    
                    // Store admin session data
                    HttpContext.Session.SetString("UserRole", "Admin");
                    HttpContext.Session.SetString("UserEmail", loginVM.email);
                    HttpContext.Session.SetString("JWTToken", tokenString);
                    
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
}
