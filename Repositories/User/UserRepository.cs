using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PrimeAspApi.Data;
using PrimeAspApi.Models;

namespace PrimeAspApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _context.Users.FindAsync(id);
#pragma warning restore CS8603 // Possible null reference return.
        }

         public async Task<User> GetUserByEmailAsync(string email)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task<User> CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
