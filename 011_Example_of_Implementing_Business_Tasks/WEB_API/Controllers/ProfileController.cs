using Application.Abstractions.Services;
using Application.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WEB_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;
        private readonly IUserContextService _userContextService;
        public ProfileController(IProfileService profileService, IUserContextService userContextService)
        {
            _profileService = profileService ?? throw new ArgumentNullException(nameof(profileService));
            _userContextService = userContextService ?? throw new ArgumentNullException(nameof(userContextService));
        }

        [HttpPost("new-user-profile")]
        public async Task<ActionResult<GetUserDTO>> AddUserAsync([FromBody] AddUserDTO newUser)
        {
            var addedUser = await _profileService.AddUserAsync(newUser);
            return CreatedAtAction(nameof(AddUserAsync), addedUser);
        }

        [HttpGet("user-profile-info")]
        [Authorize]
        public async Task<ActionResult<GetUserDTO>> GetUserProfileDataAsync()
        {
            var userLogin = _userContextService.GetUserLogin();
            if (userLogin == null) return BadRequest("User not authenticated");
            return Ok(await _profileService.GetUserByLoginAsync(userLogin));
        }

        [HttpPut("user-profile-info")]
        [Authorize]
        public async Task<ActionResult<GetUserDTO>> UpdateUserProfileDataAsync([FromBody] UpdateUserDTO newUserData)
        {
            var userLogin = _userContextService.GetUserLogin();
            if (userLogin == null) return BadRequest("User not authenticated");
            return Ok(await _profileService.UpdateUserAsync(newUserData, userLogin));
        }

        [HttpDelete("user-profile")]
        [Authorize]
        public async Task<ActionResult<GetUserDTO>> DeleteUserProfileDataAsync()
        {
            var userLogin = _userContextService.GetUserLogin();
            if (userLogin == null) return BadRequest("User not authenticated");
            return Ok(await _profileService.DeleteUserByLoginAsync(userLogin));
        }
    }
}
