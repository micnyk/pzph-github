using Microsoft.AspNetCore.Mvc;

namespace Pzph.WebApplication.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/register")]
        public IActionResult Register()
        {
            return View();
        }
        
        [HttpGet("/login")]
        public IActionResult Login()
        {
            return View();
        }
        
        [HttpGet("/choice")]
        public IActionResult Choice()
        {
            return View();
        }
    }
}