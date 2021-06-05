using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pzph.RepositoryLayer;
using Pzph.WebApplication.Models.Services;

namespace Pzph.WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly PzphDbContext _dbcontext;

        public HomeController(PzphDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        [HttpGet("/")]

        public async Task<IActionResult> Index()
        {
            var services = await _dbcontext.Services.OrderByDescending(service => service.CreatedAt).ToListAsync();

            var serviceModel = services.Select(service => new ServiceModel
            {
                ContractorId = service.Contractor.Id, CreatedAt = service.CreatedAt, Description = service.Description, Id = service.Id,
                Name = service.Name                
            }); 
            return View(serviceModel);
        }
    }
}