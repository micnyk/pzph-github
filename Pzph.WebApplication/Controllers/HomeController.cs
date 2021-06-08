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
        
        [HttpGet("/tourism")]
        public IActionResult Tourism()
        {
            return View();
        }
        
        [HttpGet("/transport")]
        public IActionResult Transport()
        {
            return View();
        }
        
        [HttpGet("/construction")]
        public IActionResult Construction()
        {
            return View();
        }
        
        [HttpGet("/gastronomy")]
        public IActionResult Gastronomy()
        {
            return View();
        }
        
        [HttpGet("/it")]
        public IActionResult IT()
        {
            return View();
        }
        
        [HttpGet("/carmechanics")]
        public IActionResult CarMechanics()
        {
            return View();
        }
        
        [HttpGet("/hairdressing")]
        public IActionResult Hairdressing()
        {
            return View();
        }
        
        [HttpGet("/photography")]
        public IActionResult Photography()
        {
            return View();
        }
        
        [HttpGet("/gardening")]
        public IActionResult Gardening()
        {
            return View();
        }
        
        [HttpGet("/medicine")]
        public IActionResult Medicine()
        {
            return View();
        }
        
        [HttpGet("/electronics")]
        public IActionResult Electronics()
        {
            return View();
        }
        
        [HttpGet("/geodesy")]
        public IActionResult Geodesy()
        {
            return View();
        }
    }
}