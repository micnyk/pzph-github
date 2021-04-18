using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pzph.ServiceLayer.Exceptions;
using Pzph.ServiceLayer.Users.Services;
using Pzph.WebApplication.Models;

namespace Pzph.WebApplication.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly IUsersService _usersService;

        public AuthorizationController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpPost("/api/auth/register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            try
            {
                await _usersService.Register(model.Name, model.Email, model.PhoneNumber, model.Password);
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

            return Ok();
        }
    }
}