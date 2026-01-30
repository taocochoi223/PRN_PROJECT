using GlassStore.Entities.TriCH.Models;
using GlassStore.Repositories.TriCH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlassStore.Services.TriCH
{
    public class CategoryTriCHService : ICategoryTriCHService
    {
        private readonly CategoryTriCHRepository _repo;
        public CategoryTriCHService(CategoryTriCHRepository repo)
        {
            _repo = repo;
        }

        public async Task AddCategoryAsync(CategoryTriCh category)
        {
            await _repo.CreateAsync(category);
        }

        public async Task DeleteCategoryByIdAsync(int categoryId)
        {
            var category = await _repo.GetByIdAsync(categoryId);
            if (category != null)
            {
                await _repo.RemoveAsync(category);
            }
        }

        public async Task<List<CategoryTriCh>> GetAllActiveCategoriesAsync()
        {
            return await _repo.GetAllActiveCategoriesAsync();
        }

        public async Task<CategoryTriCh?> GetCategoryByIdAsync(int categoryId)
        {
            var category =  _repo.GetByIdAsync(categoryId);
            return await category;
        }

        public async Task UpdateCategoryAsync(CategoryTriCh category)
        {
            await _repo.UpdateAsync(category);
        }
    }
}
