using Microsoft.AspNetCore.Mvc;
using Lesson008.Models;
using Lesson008.Services;

namespace Task001.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController: ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            if (loginRequest == null)
            {
                return BadRequest("Login request cannot be null.");
            }
            var token = _authService.LoginUser(loginRequest);
            if (token == null)
            {
                return Unauthorized("Invalid login credentials.");
            }
            return Ok(new { Token = token });
        }
    }
}
