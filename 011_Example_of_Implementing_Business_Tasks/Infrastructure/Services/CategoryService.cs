using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.DTOs;
using Application.DTOs.CategoryDTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IDataRepository _dataRepository;
        public CategoryService(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository ?? throw new ArgumentNullException(nameof(dataRepository));
        }
        public async Task<List<GetCategoryDTO>> GetAllCategoriesAsync(string login)
        {
            var user = await _dataRepository.GetUserByLoginAsync(login);
            if (user == null)
            {
                throw new ArgumentException("User not found");
            }
            return user.Categories.Select(c => new GetCategoryDTO
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                Expenses = c.Expenses?.Select(e => new GetExpenseDTO
                {
                    Id = e.Id,
                    Name = e.Name,
                    Date = e.Date,
                    Amount = e.Amount,
                    Description = e.Description,
                    CategoryName = e.Category?.Name
                }).ToList()
            }).ToList();
        }
        public async Task<GetCategoryDTO> DeleteCategoryAsync(string login, int categoryId)
        {
            var user = await _dataRepository.GetUserByLoginAsync(login);
            if (user == null)
            {
                throw new ArgumentException("User not found");
            }
            var category = user.Categories.FirstOrDefault(c => c.Id == categoryId);
            if (category == null)
            {
                throw new ArgumentException($"Category with ID {categoryId} for user {login} not found");
            }
            var deletedCategory = await _dataRepository.DeleteCategoryAsync(categoryId);
            if (deletedCategory == null)
            {
                throw new InvalidOperationException($"Failed to delete category with ID {categoryId} for user {login}");
            }
            return new GetCategoryDTO
            {
                Id = deletedCategory.Id,
                Name = deletedCategory.Name,
                Description = deletedCategory.Description
            };
        }
        public async Task<GetCategoryDTO> GetCategoryByIdAsync(string login, int categoryId)
        {
            var user = await _dataRepository.GetUserByLoginAsync(login);
            if (user == null)
            {
                throw new ArgumentException("User not found");
            }
            var category = user.Categories.FirstOrDefault(c => c.Id == categoryId);
            if (category == null)
            {
                throw new ArgumentException($"Category with ID {categoryId} for user {login} not found");
            }
            return new GetCategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                Expenses = category.Expenses?.Select(e => new GetExpenseDTO
                {
                    Id = e.Id,
                    Name = e.Name,
                    Date = e.Date,
                    Amount = e.Amount,
                    Description = e.Description,
                    CategoryName = e.Category?.Name
                }).ToList()
            };
        }
        public async Task<GetCategoryDTO> AddCategoryAsync(string login, AddCategoryDTO addCategoryDto)
        {
            var user = await _dataRepository.GetUserByLoginAsync(login);
            if (user == null)
            {
                throw new ArgumentException("User not found");
            }
            var category = user.Categories.FirstOrDefault(c => c.Name == addCategoryDto.Name);
            if (category != null)
            {
                throw new ArgumentException($"Category with name {addCategoryDto.Name} for user {login} already exists");
            }
            category = new Category
            {
                UserId = user.Id,
                Name = addCategoryDto.Name,
                Description = addCategoryDto.Description
            };
            if(await _dataRepository.AddCategoryAsync(category) == null)
            {
                throw new InvalidOperationException($"Failed to add category {addCategoryDto.Name} for user {login}");
            }
            return new GetCategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }
        public async Task<GetCategoryDTO> UpdateCategoryAsync(string login, UpdateCategoryDTO updateCategoryDto)
        {
            var user = await _dataRepository.GetUserByLoginAsync(login);
            if (user == null)
            {
                throw new ArgumentException("User not found");
            }
            var categoryToUpdate = user.Categories.FirstOrDefault(c => c.Id == updateCategoryDto.Id);
            if (categoryToUpdate == null)
            {
                throw new ArgumentException($"Category with ID {updateCategoryDto.Id} for user {login} not found");
            }
            categoryToUpdate.Name = updateCategoryDto.Name;
            categoryToUpdate.Description = updateCategoryDto.Description;
            var updatedCategory = await _dataRepository.UpdateCategoryAsync(categoryToUpdate);
            if (updatedCategory == null)
            {
                throw new InvalidOperationException($"Failed to update category with ID {updateCategoryDto.Id} for user {login}");
            }
            return new GetCategoryDTO
            {
                Id = updatedCategory.Id,
                Name = updatedCategory.Name,
                Description = updatedCategory.Description,
                Expenses = updatedCategory.Expenses?.Select(e => new GetExpenseDTO
                {
                    Id = e.Id,
                    Name = e.Name,
                    Date = e.Date,
                    Amount = e.Amount,
                    Description = e.Description,
                    CategoryName = e.Category?.Name
                }).ToList()
            };
        }
    }
}
