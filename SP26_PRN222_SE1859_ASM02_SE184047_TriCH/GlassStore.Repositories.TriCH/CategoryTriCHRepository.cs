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

        public async Task<List<CategoryTriCh>> GetAllCategoriesAsync()
        {
            var cate = _context.CategoryTriChes
                .OrderBy(c => c.CategoryName)
                .ToListAsync();
            return await cate;
        }

        public async Task<CategoryTriCh> GetCategoryByIdAsync(int categoryId)
        {
            var category = await _context.CategoryTriChes
                .FirstOrDefaultAsync(c => c.CategoryTriChid == categoryId);
            return category!;
        }
    }
}
