using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Pzph.ServiceLayer.Exceptions;
using Pzph.ServiceLayer.Users.Domain;
using Pzph.ServiceLayer.Users.Models;

namespace Pzph.ServiceLayer.Users.Services
{
    public class UsersService : IUsersService
    {
        private readonly UserManager<User> _userManager;
        private readonly IdentityServerConfig _config;
        private readonly HttpClient _client;

        public UsersService(UserManager<User> userManager, IOptions<IdentityServerConfig> config,
            IHttpClientFactory httpClientFactory)
        {
            _userManager = userManager;
            _client = httpClientFactory.CreateClient();
            _config = config.Value;
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

            await _userManager.AddClaimsAsync(user,
                new[]
                {
                    new Claim(Claims.ContractorId, user.Contractor.Id),
                    new Claim(Claims.CustomerId, user.Customer.Id),
                    new Claim(Claims.UserId, user.Id),
                    new Claim(Claims.FullName, user.FullName)
                });

            return user;
        }

        public async Task<UserTokens> Login(string email, string password)
        {
            var content = new FormUrlEncodedContent(
                new List<KeyValuePair<string, string>>
                {
                    new("grant_type", "password"),
                    new("client_id", _config.ClientId),
                    new("client_secret", _config.ClientSecret),
                    new("audience", "pzph.api"),
                    new("scope", "pzph.api offline_access"),
                    new("username", email),
                    new("password", password),
                });

            var url = _config.TokenEndpoint;

            var response = await _client.PostAsync(url, content);
            var responseBody = await response.Content.ReadAsStringAsync();
            var responseToken = JsonSerializer.Deserialize<TokenResponse>(responseBody);

            if (responseToken is null || responseToken.IsError)
                throw new ValidationException("Invalid e-mail or password");

            return new UserTokens
            {
                Access = responseToken.AccessToken,
                Refresh = responseToken.RefreshToken
            };
        }
    }
}