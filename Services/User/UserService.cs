using System.Collections.Generic;
using System.Threading.Tasks;
using PrimeAspApi.Models;
using PrimeAspApi.Repositories;
using BCrypt.Net;

namespace PrimeAspApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }

        public async Task<User> CreateUserAsync(User user, string password)
        {
            // Hash password sebelum menyimpan ke database
            user.Password = BCrypt.Net.BCrypt.HashPassword(password);
            return await _userRepository.CreateUserAsync(user);
        }

        public async Task<bool> VerifyUserAsync(string email, string password)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null) return false;

            // Cek apakah password cocok
            return BCrypt.Net.BCrypt.Verify(password, user.Password);
        }
    }
}
