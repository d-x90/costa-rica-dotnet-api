using System.Threading.Tasks;
using CostaRicaApi.Models;

namespace CostaRicaApi.Services {
    public interface IAuthenticationService {
        Task<int> Register(User user, string password);
        Task<string> Login(string usernameOrEmail, string password);

        Task<bool> IsUserPresent(string username, string email);
    }
}