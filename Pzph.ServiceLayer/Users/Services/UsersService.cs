using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Pzph.ServiceLayer.Common;
using Pzph.ServiceLayer.Contractors.Domain;
using Pzph.ServiceLayer.Customers.Domain;
using Pzph.ServiceLayer.Exceptions;
using Pzph.ServiceLayer.Users.Domain;
using Pzph.ServiceLayer.Users.Models;

namespace Pzph.ServiceLayer.Users.Services
{
    public class UsersService : IUsersService
    {
        private readonly UserManager<User> _userManager;
        private readonly IRepository<Customer> _customersRepository;
        private readonly IRepository<Contractor> _contractorsRepository;
        private readonly JwtConfig _jwtConfig;

        public UsersService(UserManager<User> userManager, IRepository<Customer> customersRepository, IRepository<Contractor> contractorsRepository, IOptions<JwtConfig> jwtConfig)
        {
            _userManager = userManager;
            _customersRepository = customersRepository;
            _contractorsRepository = contractorsRepository;
            _jwtConfig = jwtConfig.Value;
        }

        public async Task<User> Register(string name, string email, string phoneNumber, string password)
        {
            var existingUser = await _userManager.FindByEmailAsync(email);

            if (existingUser != null)
                throw new ValidationException("E-mail address is already in use");

            var user = new User()
            {
                Id = Guid.NewGuid().ToString(),
                Email = email,
                NormalizedEmail = email.ToUpperInvariant(),
                UserName = email,
                NormalizedUserName = email.ToUpperInvariant(),
                PhoneNumber = phoneNumber,
                FullName = name
            };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
                throw new ValidationException(string.Join(", ", result.Errors.Select(x => x.Description)));

            var token = GenerateJwtToken(user);

            return user;
        }

        private string GenerateJwtToken(IdentityUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }
    }
}