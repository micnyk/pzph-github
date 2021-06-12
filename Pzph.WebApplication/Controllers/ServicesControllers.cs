using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pzph.RepositoryLayer;
using Pzph.ServiceLayer.Contractors.Domain;
using Pzph.WebApplication.Extensions;
using Pzph.WebApplication.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Pzph.WebApplication.Controllers
{

    [Authorize(AuthenticationSchemes = "Bearer")]

    public class ServicesControllers : Controller
    {
        private PzphDbContext _dbContext;

        public ServicesControllers(PzphDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("/api/services")]
        public async Task<IActionResult> AddService([FromBody] AddServiceModel addServiceModel)
        {
            var user = HttpContext.User;
            var contractorId = user.GetContractorId();
            var contractor = await _dbContext.Contractors.FindAsync(contractorId);
            await _dbContext.Services.AddAsync(new Service(contractor, addServiceModel.Name, addServiceModel.Description));
            await _dbContext.SaveChangesAsync();

            //return Ok("Hello");
            return BadRequest(2021);
        }


        [HttpGet("/api/services/{categoryId}")]
        public async Task<IActionResult> GetServices(string categoryId)
        {
            var services = await _dbContext.Services.OrderByDescending(service => service.CreatedAt).Where(service => service.Category.Id == categoryId).ToListAsync();

            var serviceModel = services.Select(service => new ServiceModel
            {
                ContractorId = service.Contractor.Id, CreatedAt = service.CreatedAt, Description = service.Description, Id = service.Id,
                  Name = service.Name                
            });
            return Ok(serviceModel);
        }


    }
}
