using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Lesson008.Models;
using Lesson008.Services;

namespace Task001.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("authenticated")]
        [Authorize]
        public IActionResult AuthenticatedEndPoint()
        {
            return Ok("You are authenticated.");
        }

        [HttpGet("userPolicy")]
        [Authorize(Policy = "UserPolicy")]
        public IActionResult UserPolicyEndPoint()
        {
            return Ok("Welcome to the User Controller!");
        }

        [HttpGet("GetAllUsers")]
        [Authorize(Policy = "FinanceOnlyPolicy")]
        public ActionResult<IEnumerable<User>> GetAllUsers()
        {
            var users = _userService.GetAllUsers();
            if (users == null || !users.Any())
            {
                return NotFound("No users found.");
            }   
            return Ok(users);
        }

        [HttpGet("GetUserByLogin")]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult GetUserByLogin([FromQuery]string login)
        {
            var user = _userService.GetUserByLogin(login);
            if (user == null)
            {
                return NotFound($"User with login {login} not found.");
            }
            return Ok(user);
        }

        [HttpPost("AddNewUser")]
        [Authorize(Policy = "HRManagerOnlyPolisy")]
        public IActionResult AddNewUser([FromBody] User user)
        {
            if (user == null || string.IsNullOrEmpty(user.Login) || string.IsNullOrEmpty(user.Password))
            {
                return BadRequest("Invalid user data.");
            }
            if (_userService.AddNewUser(user))
            {
                return Ok();
            }
            return Conflict($"User with login {user.Login} already exists.");
        }
    }
}
