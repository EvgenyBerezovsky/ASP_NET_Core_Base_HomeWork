using Application.DTOs;
using Application.DTOs.ExpenceDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Services
{
    public interface IExpenseService
    {
        /// <summary>
        /// Retrieves all expenses for a specific user.
        /// </summary>
        /// <param name="login">The login of the user whose expenses are to be retrieved.</param>
        /// <returns>A list of expenses associated with the user.</returns>
        public Task<List<GetExpenseDTO>> GetAllExpensesAsync(string login);

        /// <summary>
        /// Adds a new expense for a specific user.
        /// </summary>
        /// <param name="login">The login of the user to whom the expense belongs.</param>
        /// <param name="expense">The expense to be added.</param>
        /// <returns>The added expense.</returns>
        public Task<GetExpenseDTO> AddExpenseAsync(string login, AddExpenseDTO expense);

        /// <summary>
        /// Updates an existing expense for a specific user.
        /// </summary>
        /// <param name="login">The login of the user to whom the expense belongs.</param>
        /// <param name="expense">The updated expense details.</param>
        /// <returns>The updated expense.</returns>
        public Task<GetExpenseDTO> UpdateExpenseAsync(string login, UpdateExpenseDTO expense);

        /// <summary>
        /// Deletes an expense for a specific user.
        /// </summary>
        /// <param name="login">The login of the user to whom the expense belongs.</param>
        /// <param name="expenseId">The ID of the expense to be deleted.</param>
        public Task<GetExpenseDTO> DeleteExpenseAsync(string login, int expenseId);

        /// <summary>
        /// Retrieves an expense by specific Id.
        /// </summary>
        /// <param name="login"></param>
        /// <param name="expenseId"></param>
        /// <returns>An object type of GetExpenseDTO</returns>
        public Task<GetExpenseDTO> GetExpenseByIdAsync(string login, int expenseId);

    }
}
