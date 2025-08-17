using Application.Abstractions.Services;
using Application.DTOs;
using Application.DTOs.ExpenceDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WEB_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _expenseService;
        private readonly IUserContextService _userContextService;

        public ExpenseController(IExpenseService expenseService, IUserContextService userContextService)
        {
            _expenseService = expenseService ?? throw new ArgumentNullException(nameof(expenseService));
            _userContextService = userContextService ?? throw new ArgumentNullException(nameof(userContextService));
        }

        [HttpPost("new-expense")]
        [Authorize]
        public async Task<ActionResult<GetExpenseDTO>> AddExpenseAsync([FromBody] AddExpenseDTO addExpenseDto)
        {
            var userLogin = _userContextService.GetUserLogin();
            if (userLogin == null) return BadRequest("User not authenticated");
            var addedExpense = await _expenseService.AddExpenseAsync(userLogin, addExpenseDto);
            return CreatedAtAction(nameof(AddExpenseAsync), addedExpense);
        }

        [HttpGet("all-expenses")]
        [Authorize]
        public async Task<ActionResult<List<GetExpenseDTO>>> GetAllExpensesAsync()
        {
            var userLogin = _userContextService.GetUserLogin();
            if (userLogin == null) return BadRequest("User not authenticated");
            var expenses = await _expenseService.GetAllExpensesAsync(userLogin);
            return Ok(expenses);
        }

        [HttpGet("expense/{expenseId}")]
        [Authorize]
        public async Task<ActionResult<GetExpenseDTO>> GetExpenseAsync([FromBody]int expenseId)
        {
            var userLogin = _userContextService.GetUserLogin();
            if (userLogin == null) return BadRequest("User not authenticated");
            var expense = await _expenseService.GetExpenseByIdAsync(userLogin, expenseId);
            if (expense == null) return NotFound($"Expense with ID {expenseId} not found");
            return Ok(expense);
        }

        [HttpPut("update-expense")]
        [Authorize]
        public async Task<ActionResult<GetExpenseDTO>> UpdateExpenseAsync([FromBody] UpdateExpenseDTO updateExpenseDto)
        {
            var userLogin = _userContextService.GetUserLogin();
            if (userLogin == null) return BadRequest("User not authenticated");
            var updatedExpense = await _expenseService.UpdateExpenseAsync(userLogin, updateExpenseDto);
            return Ok(updatedExpense);
        }

        [HttpDelete("delete-expense/{expenseId}")]
        [Authorize]
        public async Task<ActionResult<GetExpenseDTO>> DeleteExpenseAsync(int expenseId)
        {
            var userLogin = _userContextService.GetUserLogin();
            if (userLogin == null) return BadRequest("User not authenticated");
            var deletedExpense = await _expenseService.DeleteExpenseAsync(userLogin, expenseId);
            return Ok(deletedExpense);
        }
    }
}