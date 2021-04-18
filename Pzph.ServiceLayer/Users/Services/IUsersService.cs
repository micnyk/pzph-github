using System.Threading.Tasks;
using Pzph.ServiceLayer.Users.Domain;

namespace Pzph.ServiceLayer.Users.Services
{
    public interface IUsersService
    {
        Task<User> Register(string name, string email, string phoneNumber, string password);
    }
}