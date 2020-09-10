using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CostaRicaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CostaRicaApi.Repositories {
    public class UserRepository : IUserRepository
    {
        private readonly ExpenseContext _context;

        public UserRepository(ExpenseContext context) => _context = context;

        public Task<List<User>> GetAllUsersAsync()
        {
            return _context.Users.ToListAsync();
        }

        public Task<User> GetUserByIdAsync(int id)
        {
            return _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void RemoveUser(User user)
        {
            _context.Users.Remove(user);
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}