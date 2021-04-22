using System.Threading.Tasks;
using Pzph.ServiceLayer.Users.Domain;
using Pzph.ServiceLayer.Users.Models;

namespace Pzph.ServiceLayer.Users.Services
{
    public interface IUsersService
    {
        Task<User> Register(string name, string email, string phoneNumber, string password);
        Task<UserTokens> Login(string modelEmail, string modelPassword);
    }
}