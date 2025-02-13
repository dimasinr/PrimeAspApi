using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PrimeAspApi.Models;
using PrimeAspApi.Services;

namespace PrimeAspApi.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return Ok(await _userService.GetAllUsersAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> RegisterUser([FromBody] RegisterUserDto userDto)
        {
            var newUser = await _userService.CreateUserAsync(
                new User { Name = userDto.Name, Email = userDto.Email, Password = userDto.Password },
                userDto.Password
            );

            return CreatedAtAction(nameof(GetUser), new { id = newUser.Id }, newUser);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var isValid = await _userService.VerifyUserAsync(loginDto.Email, loginDto.Password);
            if (!isValid)
                return Unauthorized(new { message = "Invalid email or password" });

            return Ok(new { message = "Login successful" });
        }
    }

    public class RegisterUserDto
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }

    public class LoginDto
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
