using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pzph.RepositoryLayer;
using Pzph.WebApplication.Models.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pzph.WebApplication.Controllers
{
    public class CategoriesController : Controller 
    {

        private PzphDbContext _dbContext;

        public CategoriesController(PzphDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpGet("/api/categories")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _dbContext.Categories.OrderByDescending(category => category.CreatedAt).ToListAsync();

            var categoryModel = categories.Select(category => new CategoryModel
            {
                CreatedAt = category.CreatedAt,
                Id = category.Id,
                Name = category.Name
            });
            return Ok(categoryModel);
        }


    }
}
