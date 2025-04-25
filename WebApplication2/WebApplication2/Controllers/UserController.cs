using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public UserController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        public IActionResult Register()
        {
            var result = new ViewResult();
            result.ViewName = "Register";
            return result;
        }

        public IActionResult sign_in()
        {
            var result = new ViewResult();
            result.ViewName = "Sign_in";
            return result;
        }


        public IActionResult Sport_events()
        {
            var result = new ViewResult();
            result.ViewName = "Sports_events";
            return result;
        }

        public IActionResult Entertainment_events()
        {
            var result = new ViewResult();
            result.ViewName = "Entertaimnet_events";
            return result;
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    
}
}
