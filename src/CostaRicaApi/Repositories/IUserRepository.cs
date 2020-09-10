using System.Collections.Generic;
using System.Threading.Tasks;
using CostaRicaApi.Models;

namespace CostaRicaApi.Repositories {
    public interface IUserRepository {
        Task<List<User>> GetAllUsersAsync();
        
        Task<User> GetUserByIdAsync(int id);

        Task<int> SaveChangesAsync();

        void RemoveUser(User user);
    }
}