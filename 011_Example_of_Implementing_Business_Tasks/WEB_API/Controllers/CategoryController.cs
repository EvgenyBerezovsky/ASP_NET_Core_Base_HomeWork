using Application.Abstractions.Services;
using Application.DTOs.CategoryDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WEB_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IUserContextService _userContextService;
        public CategoryController(ICategoryService categoryService, IUserContextService userContextService)
        {
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
            _userContextService = userContextService ?? throw new ArgumentNullException(nameof(userContextService));
        }

        [HttpGet("all-categories")]
        [Authorize]
        public async  Task<ActionResult<List<GetCategoryDTO>>> GetAllCategoriesAsync()
        {
            var userLogin = _userContextService.GetUserLogin();
            if (userLogin == null) return BadRequest("User not authenticated");
            var categories = await _categoryService.GetAllCategoriesAsync(userLogin);
            return Ok(categories);
        }

        [HttpDelete("delete-category/{categoryId}")]
        [Authorize]
        public async Task<ActionResult<GetCategoryDTO>> DeleteCategoryAsync(int id)
        {
            var userLogin = _userContextService.GetUserLogin();
            if (userLogin == null) return BadRequest("User not authenticated");
            var deletedCategory = await _categoryService.DeleteCategoryAsync(userLogin, id);
            if (deletedCategory == null) return NotFound($"Category with ID {id} not found");
            return Ok(deletedCategory);
        }

        [HttpPost("new-category")]
        [Authorize]
        public async Task<ActionResult<GetCategoryDTO>> AddCategoryAsync([FromBody] AddCategoryDTO addCategoryDto)
        {
            var userLogin = _userContextService.GetUserLogin();
            if (userLogin == null) return BadRequest("User not authenticated");
            var addedCategory = await _categoryService.AddCategoryAsync(userLogin, addCategoryDto);
            if (addedCategory == null) return BadRequest("Failed to add category");
            return CreatedAtAction(nameof(AddCategoryAsync), addedCategory);
        }

        [HttpGet("category/{categoryId}")]
        [Authorize]
        public async Task<ActionResult<GetCategoryDTO>> GetCategoryAsync(int id)
        {
            var userLogin = _userContextService.GetUserLogin();
            if (userLogin == null) return BadRequest("User not authenticated");
            var category = await _categoryService.GetCategoryByIdAsync(userLogin, id);
            if (category == null) return NotFound($"Category with ID {id} not found");
            return Ok(category);
        }

        [HttpPut("update-category")]
        [Authorize]
        public async Task<ActionResult<GetCategoryDTO>> UpdateCategoryAsync([FromBody] UpdateCategoryDTO updateCategoryDto)
        {
            var userLogin = _userContextService.GetUserLogin();
            if (userLogin == null) return BadRequest("User not authenticated");
            var updatedCategory = await _categoryService.UpdateCategoryAsync(userLogin, updateCategoryDto);
            if (updatedCategory == null) return NotFound($"Category with ID {updateCategoryDto.Id} not found");
            return Ok(updatedCategory);
        }
    }
}
