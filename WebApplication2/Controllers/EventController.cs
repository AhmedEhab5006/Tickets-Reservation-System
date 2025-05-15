using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication2.Models;
using System.Text.Json;
using Newtonsoft.Json;
using WebApplication2.ViewModels;

namespace WebApplication2.Controllers
{
    public class EventController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _client;


        public EventController(ILogger<HomeController> logger, HttpClient httpClient)
        {
            _logger = logger;
            _client = httpClient;
            _client.BaseAddress = new Uri("https://localhost:7110/");
        }


        //it will be decommented later

        //public async Task<IActionResult> All_events()
        //{
        //    await _client.GetFromJsonAsync<List<SportEventVM>>("api/Client/SportEvents");
        //    return View();

        //}


        public async Task<IActionResult> Sport_events(SportEventVM sportevent)
        {
            try
            {
                var response = await _client.GetAsync("api/Client/SportEvents");
                
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError($"API request failed with status code: {response.StatusCode}");
                    return View(new List<SportEventVM>());
                }

                var content = await response.Content.ReadAsStringAsync();
                
                if (string.IsNullOrEmpty(content))
                {
                    _logger.LogWarning("API returned empty response");
                    return View(new List<SportEventVM>());
                }

                //try
                {
                    var sportEvents = JsonConvert.DeserializeObject<List<SportEventVM>>(content);
                    return View(sportEvents ?? new List<SportEventVM>());
                }
               // catch (JsonException ex)
                //{
                //    _logger.LogError($"JSON deserialization error: {ex.Message}");
                //    _logger.LogError($"Raw content: {content}");
                //    return View(new List<SportEventVM>());
                //}
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in Sport_events action: {ex.Message}");
                return View(new List<SportEventVM>());
            }
        }


        public async Task<IActionResult> Entertainment_Events()
        {
            var entertainments = await _client.GetFromJsonAsync<List<EntertainmentVM>>("api/Client/EntertainmentEvents");
            return View(entertainments);
        }
       





        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }

}
