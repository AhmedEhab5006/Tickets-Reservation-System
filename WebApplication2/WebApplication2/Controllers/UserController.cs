using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _client;
        

        public UserController(ILogger<HomeController> logger , HttpClient httpClient)
        {
            _logger = logger;
            _client = httpClient;
            _client.BaseAddress = new Uri("https://localhost:7110/");
        }


        public async Task <IActionResult> Register(RegisterVM registerVM)
        {
            await _client.PostAsJsonAsync<RegisterVM>("api/Auth/Register", registerVM);
            return View();
        }

        public async Task <IActionResult> sign_in(LoginVM loginVM)
        {
            await _client.PostAsJsonAsync<LoginVM>("api/Auth/Login", loginVM);
            return View();
        }


        public async Task <IActionResult> Sport_events()
        {
            await _client.GetFromJsonAsync<List<SportEventVM>>("api/Client/SportEvents");
            return View();
        }

        public async Task<IActionResult> Entertainment_events()
        {
            await _client.GetFromJsonAsync<List<EntertainmentVM>>("api/Client/EntertainmentEvents");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    
}
}
