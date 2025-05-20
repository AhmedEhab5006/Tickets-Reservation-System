using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication2.Models;
using Newtonsoft.Json;
using WebApplication2.ViewModels;

namespace WebApplication2.Controllers
{
    public class EventController : Controller
    {
        private readonly ILogger<EventController> _logger;
        private readonly HttpClient _client;

        public EventController(ILogger<EventController> logger, HttpClient httpClient)
        {
            _logger = logger;
            _client = httpClient;
            _client.BaseAddress = new Uri("https://localhost:7110/"); // API project base URL
        }

        public async Task<IActionResult> Sport_events()
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

                var sportEvents = JsonConvert.DeserializeObject<List<SportEventVM>>(content);
                return View(sportEvents ?? new List<SportEventVM>());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in Sport_events action: {ex.Message}");
                return View(new List<SportEventVM>());
            }
        }

        public async Task<IActionResult> Entertainment_Events()
        {
            try
            {
                var response = await _client.GetAsync("api/Client/EntertainmentEvents");

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError($"API request failed with status code: {response.StatusCode}");
                    return View(new List<EntertainmentVM>());
                }

                var content = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrEmpty(content))
                {
                    _logger.LogWarning("API returned empty response");
                    return View(new List<EntertainmentVM>());
                }

                var entertainmentEvents = JsonConvert.DeserializeObject<List<EntertainmentVM>>(content);
                return View(entertainmentEvents ?? new List<EntertainmentVM>());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in Entertainment_Events action: {ex.Message}");
                return View(new List<EntertainmentVM>());
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
