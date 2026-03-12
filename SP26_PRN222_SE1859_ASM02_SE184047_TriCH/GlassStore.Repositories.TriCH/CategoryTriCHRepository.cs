using GlassStore.Entities.TriCH.Models;
using GlassStore.Repositories.TriCH.Basic;
using GlassStore.Repositories.TriCH.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlassStore.Repositories.TriCH
{
    public class CategoryTriCHRepository : GenericRepository<CategoryTriCh>
    {
        public CategoryTriCHRepository(PRN222_EYEWEARSHOPContext context)
        {
            _context = context;
        }

        public async Task<List<CategoryTriCh>> GetAllActiveCategoriesAsync()
        {
            var cate = _context.CategoryTriChes
                .Where(c => c.Status == 1)
                .OrderBy(c => c.CategoryName)
                .ToListAsync();
            return await cate;
        }

        // Dùng cho dropdown (không phân trang) - được gọi từ LoadCategoriesToViewBag
        public async Task<List<CategoryTriCh>> GetAllCategoriesAsync()
        {
            return await _context.CategoryTriChes
                .OrderBy(c => c.CategoryName)
                .ToListAsync();
        }

        // Dùng cho trang list có phân trang + tìm kiếm
        public async Task<(List<CategoryTriCh> Items, int TotalCount)> GetAllCategoriesPagedAsync(
            int pageIndex, int pageSize, string? search = null)
        {
            var query = _context.CategoryTriChes.AsQueryable();
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(c => c.CategoryName.Contains(search));
            }
            int total = await query.CountAsync();
            var items = await query
                .OrderBy(c => c.CategoryName)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();
            return (items, total);
        }

        public async Task<CategoryTriCh> GetCategoryByIdAsync(int categoryId)
        {
            var category = await _context.CategoryTriChes
                .FirstOrDefaultAsync(c => c.CategoryTriChid == categoryId);
            return category!;
        }
    }
}
