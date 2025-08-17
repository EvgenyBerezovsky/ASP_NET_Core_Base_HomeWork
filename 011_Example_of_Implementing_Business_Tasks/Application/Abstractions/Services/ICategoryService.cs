using Application.DTOs.CategoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Services
{
    public interface ICategoryService
    {
        public Task<List<GetCategoryDTO>> GetAllCategoriesAsync(string login);
        public Task<GetCategoryDTO> DeleteCategoryAsync(string login, int categoryId);
        public Task<GetCategoryDTO> GetCategoryByIdAsync(string login, int categoryId);
        public Task<GetCategoryDTO> AddCategoryAsync(string login, AddCategoryDTO category);
        public Task<GetCategoryDTO> UpdateCategoryAsync(string login, UpdateCategoryDTO category);  
    }
}
