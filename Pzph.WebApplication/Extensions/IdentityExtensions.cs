using System.Security.Claims;
using Pzph.ServiceLayer.Users.Models;

namespace Pzph.WebApplication.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetCustomerId(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirstValue(Claims.CustomerId);
        }
    }
}