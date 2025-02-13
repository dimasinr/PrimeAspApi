using System.Collections.Generic;
using System.Threading.Tasks;
using PrimeAspApi.Models;

namespace PrimeAspApi.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<User> CreateUserAsync(User user);
        Task<User> GetUserByEmailAsync(string email);
    }
}
