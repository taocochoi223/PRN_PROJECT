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

        public async Task DeleteCategoryAsync(int categoryId)
        {
            var cate = await _repo.GetCategoryByIdAsync(categoryId);
            if(cate != null)
            {
                cate.Status = 0;
                await _repo.UpdateAsync(cate);
            }
        }

        public async Task<List<CategoryTriCh>> GetAllActiveCategoriesAsync()
        {
            return await _repo.GetAllActiveCategoriesAsync();
        }

        // Dùng cho dropdown (không phân trang) - lấy toàn bộ
        public async Task<List<CategoryTriCh>> GetAllCategoriesAsync()
        {
            return await _repo.GetAllCategoriesAsync();
        }

        // Dùng cho trang list có phân trang
        public async Task<(List<CategoryTriCh> Items, int TotalCount)> GetAllCategoriesPagedAsync(
            int pageIndex, int pageSize, string? search = null)
        {
            return await _repo.GetAllCategoriesPagedAsync(pageIndex, pageSize, search);
        }

        public async Task<CategoryTriCh?> GetCategoryByIdAsync(int categoryId)
        {
            return await _repo.GetCategoryByIdAsync(categoryId);
        }

        public async Task UpdateCategoryAsync(CategoryTriCh category)
        {
            await _repo.UpdateAsync(category);
        }
    }
}
