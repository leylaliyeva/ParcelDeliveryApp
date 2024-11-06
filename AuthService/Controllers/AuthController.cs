using AuthService.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _authService;

        public AuthController(UserService userService)
        {
            _authService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto user)
        {
            user.Role = Role.User;
            var token = await _authService.Register(user);
            if (token == null)
            {
                return BadRequest("Registration failed. Username or email already exists.");
            }
            return Ok(new { Token = token });
        }

        [HttpPost("register/courier")]
        public async Task<IActionResult> RegisterCourier(RegisterDto user)
        {
            user.Role = Role.Courier;
            var token = await _authService.Register(user);
            if (token == null)
            {
                return BadRequest("Registration failed. Username or email already exists.");
            }
            return Ok(new { Token = token });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var token = await _authService.Login(loginDto);
            if (token == null)
            {
                return Unauthorized("Invalid login attempt.");
            }
            return Ok(new { Token = token });
        }

        [HttpPost("me")]
        public async Task<IActionResult> UserInfo(int id)
        {
            var token = await _authService.GetUserInfoAsync(id);
            if (token == null)
            {
                return Unauthorized("Invalid login attempt.");
            }
            return Ok(new { Token = token });
        }



    }
}
