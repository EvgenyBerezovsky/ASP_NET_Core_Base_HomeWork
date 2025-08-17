using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Repositories
{
    public interface IDataRepository
    {
        // Users
        public Task<List<User>> GetAllUsersAsync();
        public Task<User?> AddUserAsync(User user);
        public Task<User?> UpdateUserAsync(User user);
        public Task<User?> GetUserByLoginAsync(string login);
        public Task<User?> DeleteUserByLoginAsync(string login);

        // Categories
        public Task<Category?> GetCategoryAsync(int id);
        public Task<Category?> DeleteCategoryAsync(int id);
        public Task<Category?> AddCategoryAsync(Category category);
        public Task<Category?> UpdateCategoryAsync(Category category);
        
        // Expenses
        public Task<Expense?> GetExpenseAsync(int id);
        public Task<Expense?> DeleteExpenseAsync(int id);
        public Task<Expense> AddExpenseAsync(Expense expense);
        public Task<Expense?> UpdateExpenseAsync(Expense expense);
       
    }
}
