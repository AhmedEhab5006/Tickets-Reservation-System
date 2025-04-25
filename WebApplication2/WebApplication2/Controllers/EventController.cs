using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class EventController : Controller
    {

        private readonly ILogger<EventController> _logger;

        public EventController(ILogger<EventController> logger)
        {
            _logger = logger;
        }


       

        public IActionResult All_events()
        {
            var result = new ViewResult();
            result.ViewName = "All_events";
            return result;
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }

}
