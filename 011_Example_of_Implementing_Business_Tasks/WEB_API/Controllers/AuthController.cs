using Application.Abstractions.Services;
using Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginUserDTO loginUserDTO)
        {
            if (loginUserDTO == null)
            {
                return BadRequest("Login data is required.");
            }
            try
            {
                var token = await _authService.LoginUserAsync(loginUserDTO);
                if (token == null)
                {
                    return Unauthorized("Invalid login or password.");
                }
                return Ok(new { Token = token });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
