using Application.Abstractions.Repositories;
using Domain.Entities;
using Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class DataRepository : IDataRepository
    {
        private readonly AppDbContext _context;
        public DataRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // Users
        public async Task<List<User>> GetAllUsersAsync()
        {
            var users = await _context.Users
                .Include(u => u.Expenses)
                .Include(u => u.Categories)
                .ToListAsync();
            return users;
        }
        public async Task<User?> AddUserAsync(User user)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Login == user.Login);
            if (existingUser != null)
            {
                return null; // User with the same login already exists
            }
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
        public async Task<User?> UpdateUserAsync(User user)
        {
            var updatedUser = await _context.Users
                .Include(u => u.Expenses)
                .Include(u => u.Categories)
                .FirstOrDefaultAsync(u => u.Id == user.Id);

            if (updatedUser == null)
            {
                return null; // User not found
            }

            updatedUser.Name = user.Name;
            updatedUser.Password = user.Password;
            _context.Users.Update(updatedUser);
            await _context.SaveChangesAsync();
            return updatedUser; // Return the updated user
        }
        public async Task<User?> GetUserByLoginAsync(string login)
        {
            return await _context.Users
                .Include(u => u.Expenses)
                .Include(u => u.Categories)
                .FirstOrDefaultAsync(u => u.Login == login);
        }
        public async Task<User?> DeleteUserByLoginAsync(string login)
        {
            var deletedUser = await _context.Users.FirstOrDefaultAsync(u => u.Login == login);
            if (deletedUser == null)
            {
                return null; // User not found
            }
            _context.Users.Remove(deletedUser);
            await _context.SaveChangesAsync();
            return deletedUser; // Return the deleted user
        }

        // Categories
        public async Task<Category?> GetCategoryAsync(int id)
        {
            return await _context.Categories
                .Include(c => c.Expenses)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<Category?> DeleteCategoryAsync(int id)
        {
            var deletedCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (deletedCategory == null)
            {
                return null; // Category not found
            }
            _context.Categories.Remove(deletedCategory);
            await _context.SaveChangesAsync();
            return deletedCategory; // Return the deleted category
        }
        public async Task<Category?> AddCategoryAsync(Category category)
        {
            var existingCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Name == category.Name && c.UserId == category.UserId);
            if (existingCategory != null)
            {
                return null; // Category with the same name already exists for this user
            }
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }
        public async Task<Category?> UpdateCategoryAsync(Category category)
        {
            var updatedCategory = await _context.Categories
                .Include(c => c.Expenses)
                .FirstOrDefaultAsync(c => c.Id == category.Id);
            if (updatedCategory == null)
            {
                return null; // Category not found
            }
            updatedCategory.Name = category.Name;
            updatedCategory.Description = category.Description;
            await _context.SaveChangesAsync();
            return updatedCategory; // Return the updated category
        }

        // Expenses
        public async Task<Expense?> GetExpenseAsync(int id)
        {
            return await _context.Expenses
                .Include(e => e.User)
                .Include(e => e.Category)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
        public async Task<Expense?> DeleteExpenseAsync(int id)
        {
            var deletedExpense = await _context.Expenses.FirstOrDefaultAsync(e => e.Id == id);
            if (deletedExpense == null)
            {
                return null; // Expense not found
            }
            _context.Expenses.Remove(deletedExpense);
            await _context.SaveChangesAsync();
            return deletedExpense; // Return the deleted expense
        }
        public async Task<Expense> AddExpenseAsync(Expense expense)
        {
            await _context.Expenses.AddAsync(expense);
            await _context.SaveChangesAsync();
            return expense; // Return the added expense
        }
        public async Task<Expense?> UpdateExpenseAsync(Expense expense)
        {
            var updatedExpense = await _context.Expenses
                .Include(e => e.User)
                .Include(e => e.Category)
                .FirstOrDefaultAsync(e => e.Id == expense.Id);

            if (updatedExpense == null)
            {
                return null; // Expense not found
            }

            updatedExpense.Name = expense.Name;
            updatedExpense.Date = expense.Date;
            updatedExpense.Amount = expense.Amount;
            updatedExpense.Description = expense.Description;
            await _context.SaveChangesAsync();

            return updatedExpense;
        }
    }
}
