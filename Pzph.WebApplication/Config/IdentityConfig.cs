using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using Pzph.ServiceLayer.Users.Models;
using static IdentityServer4.IdentityServerConstants;

namespace Pzph.WebApplication.Config
{
    internal class IdentityConfig
    {
        private static readonly string[] RoleClaims = {JwtClaimTypes.Role, ClaimTypes.Role};

        private static readonly IdentityResource RolesResource = new()
        {
            Name = "roles",
            DisplayName = "Roles",
            Description = "Allow the service access to your user roles.",
            UserClaims = RoleClaims,
            ShowInDiscoveryDocument = true,
            Required = true,
            Emphasize = true,
        };

        public static IEnumerable<IdentityResource> GetIdentityResources() => new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email(),
            RolesResource,
        };

        public static IEnumerable<ApiScope> GetApiScopes() => new List<ApiScope>
        {
            new("pzph.api"),
            new(StandardScopes.OfflineAccess),
        };

        public static IEnumerable<ApiResource> GetApiResources() => new List<ApiResource>
        {
            new(
                "pzph.api",
                "PZPH API",
                new[] {JwtClaimTypes.Role, ClaimTypes.Role, JwtClaimTypes.Name, JwtClaimTypes.Email, Claims.ContractorId, Claims.CustomerId, Claims.UserId, Claims.FullName})
            {
                Scopes = new List<string> {"pzph.api", StandardScopes.OfflineAccess,},
            },
        };

        public static IEnumerable<Client> GetClients(IConfiguration config) => new List<Client>
        {
            new()
            {
                ClientId = "pzph.web-client",
                ClientName = "PZPH Web client",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                ClientSecrets =
                {
                    new Secret(config["IdentityServer:ClientSecret"].Sha256()),
                },
                AllowedScopes = {"pzph.api", RolesResource.Name, StandardScopes.OpenId, StandardScopes.Profile, StandardScopes.Email, StandardScopes.OfflineAccess},
                AllowOfflineAccess = true,
                UpdateAccessTokenClaimsOnRefresh = true,
            },
        };
    }
}