using System.Collections.Generic;
using System.Threading.Tasks;
using PrimeAspApi.Models;

namespace PrimeAspApi.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<User> CreateUserAsync(User user, string password);
        Task<bool> VerifyUserAsync(string email, string password);
    }
}
