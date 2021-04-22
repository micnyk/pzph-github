using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pzph.ServiceLayer.Exceptions;
using Pzph.ServiceLayer.Users.Services;
using Pzph.WebApplication.Models;
using Pzph.WebApplication.Models.Users;

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
                return Ok();
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("/api/auth/login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            try
            {
                var tokens = await _usersService.Login(model.Email, model.Password);
                return Ok(tokens);
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}