using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.DTOs;
using Application.DTOs.ExpenceDTOs;
using Domain.Entities;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IDataRepository _dataRepository;

        public ExpenseService(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository ?? throw new ArgumentNullException(nameof(dataRepository));

        }

        public async Task<GetExpenseDTO> AddExpenseAsync(string login, AddExpenseDTO addExpenseDto)
        {
            var expense = await MapAddExpenseDtoToExpenseAsync(addExpenseDto, login);
            var addedExpense = await _dataRepository.AddExpenseAsync(expense);
            return MapToGetExpenseDTO(addedExpense);
        }
        public async Task<GetExpenseDTO> DeleteExpenseAsync(string login, int expenseId)
        {
            var user = await _dataRepository.GetUserByLoginAsync(login);

            if (user == null) 
            { 
                throw new InvalidOperationException($"User with login {login} not found"); 
            }

            var expenseToDelete = user.Expenses.FirstOrDefault(e => e.Id == expenseId);

            if (expenseToDelete == null)
            {
                throw new InvalidOperationException($"Expense with ID {expenseId} not found for user {login}");
            }

            var deletedExpense = await _dataRepository.DeleteExpenseAsync(expenseId);
            if (deletedExpense == null)
            {
                throw new InvalidOperationException($"Failed to delete expense with ID {expenseId} for user {login}");
            }
            return MapToGetExpenseDTO(deletedExpense);
        }

        public async Task<List<GetExpenseDTO>> GetAllExpensesAsync(string login)
        {
            var user = await _dataRepository.GetUserByLoginAsync(login);
            if (user == null)
            {
                throw new ArgumentNullException($"User with login {login} not found", nameof(user));
            }
            return user.Expenses.Select(e => MapToGetExpenseDTO(e)).ToList();
        }

        public async Task<GetExpenseDTO> UpdateExpenseAsync(string login, UpdateExpenseDTO UpdateExpenseDto)
        {
            var expenseToUpdate = await MapUpdateExpenseDtoToExpenseAsync(UpdateExpenseDto, login);
            var updatedExpense = await _dataRepository.UpdateExpenseAsync(expenseToUpdate);
            if (updatedExpense == null)
            {
                throw new InvalidOperationException($"Failed to update expense with ID {UpdateExpenseDto.Id} for user {login}");
            }
            return MapToGetExpenseDTO(updatedExpense);
        }

        public async Task<GetExpenseDTO> GetExpenseByIdAsync(string login, int id)
        {
            var user = await _dataRepository.GetUserByLoginAsync(login);
            if (user == null)
            {
                throw new InvalidOperationException($"User with login {login} not found");
            }
            var expense = user.Expenses.FirstOrDefault(e => e.Id == id);
            if (expense == null)
            {
                throw new InvalidOperationException($"Expense with Id {id} not found");
            }
            return MapToGetExpenseDTO(expense);
        }
        
        private GetExpenseDTO MapToGetExpenseDTO(Expense expense)
        {
            return new GetExpenseDTO
            {
                Id = expense.Id,
                Name = expense.Name,
                Date = expense.Date,
                Amount = expense.Amount,
                Description = expense.Description,
                CategoryName = expense.Category?.Name,
            };

        }
        private async Task<Expense> MapAddExpenseDtoToExpenseAsync(AddExpenseDTO addExpenseDto, string login)
        {
            var user = await _dataRepository.GetUserByLoginAsync(login);

            if (user == null)
            {
                throw new ArgumentException("User not found", nameof(login));
            }

            var expense = new Expense
            {
                Date = addExpenseDto.Date,
                Name = addExpenseDto.Name,
                Amount = addExpenseDto.Amount,
                Description = addExpenseDto.Description,
                UserId = user.Id
            };

            if (!string.IsNullOrEmpty(addExpenseDto.CategoryName))
            {
                var category = user.Categories.FirstOrDefault(c => c.Name == addExpenseDto.CategoryName);
                if (category == null)
                {
                    category = new Category { Name = addExpenseDto.CategoryName, UserId = user.Id };
                    await _dataRepository.AddCategoryAsync(category); // Ensure category is added to the user
                }
                expense.CategoryId = category.Id;
            }

            return expense;
        }
        private async Task<Expense> MapUpdateExpenseDtoToExpenseAsync(UpdateExpenseDTO updateExpenseDto, string login)
        {
            var user = await _dataRepository.GetUserByLoginAsync(login);

            if (user == null)
            {
                throw new ArgumentException("User not found", nameof(login));
            }

            var expense = user.Expenses.FirstOrDefault(e => e.Id == updateExpenseDto.Id);

            if (expense == null)
            {
                throw new ArgumentNullException($"Expense with Id {updateExpenseDto.Id} not found", nameof(expense));
            }

            expense.Name = updateExpenseDto.Name;
            expense.Date = updateExpenseDto.Date;
            expense.Amount = updateExpenseDto.Amount;
            expense.Description = updateExpenseDto.Description;

            if (!string.IsNullOrEmpty(updateExpenseDto.CategoryName))
            {
                var category = user.Categories.FirstOrDefault(c => c.Name == updateExpenseDto.CategoryName);
                if (category == null)
                {
                    category = new Category { Name = updateExpenseDto.CategoryName, UserId = user.Id };
                    await _dataRepository.AddCategoryAsync(category); // Ensure category is added to the user
                }
                expense.CategoryId = category.Id;
                expense.Category = category; 
            }
            
            return expense;
        }
    }
}
