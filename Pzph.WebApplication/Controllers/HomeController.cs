using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pzph.RepositoryLayer;
using Pzph.ServiceLayer.Users.Domain;
using Pzph.ServiceLayer.Users.Services;
using Pzph.WebApplication.Models.Users;

namespace Pzph.WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly IUsersService _usersService;
        private readonly PzphDbContext _dbContext;

        public HomeController(SignInManager<User> signInManager, IUsersService usersService, PzphDbContext dbContext)
        {
            _signInManager = signInManager;
            _usersService = usersService;
            _dbContext = dbContext;
        }

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
        
        [HttpPost("/register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            var user = await _usersService.Register(model.Name, model.Email, model.PhoneNumber, model.Password);
            await _signInManager.SignInAsync(user, true);
            return RedirectToAction("Choice");
        }
        
        [HttpGet("/login")]
        public IActionResult Login()
        {
            return View();
        }
        
        [HttpGet("/choice")]
        public async Task<IActionResult> Choice()
        {
            var categories = await _dbContext.Category.ToListAsync();
            return View(categories);
        }

        [HttpGet("/category/{categoryId}")]
        public async Task<IActionResult> Category(string categoryId)
        {
            var category = await _dbContext.Category.FirstOrDefaultAsync(x => x.Id == categoryId);
            return View(category);
        }
        
        [HttpGet("/service/{serviceId}")]
        public async Task<IActionResult> Service(string serviceId)
        {
            var service = await _dbContext.Services.FirstOrDefaultAsync(x => x.Id == serviceId);
            return View(service);
        }
        
        [HttpGet("/newservice")]
        public IActionResult NewService()
        {
            return View();
        }
        
        [HttpGet("/status")]
        public IActionResult Status()
        {
            return View();
        }
    }
}