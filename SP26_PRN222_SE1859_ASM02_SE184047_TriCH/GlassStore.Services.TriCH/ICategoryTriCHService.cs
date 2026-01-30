using GlassStore.Entities.TriCH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlassStore.Services.TriCH
{
    public interface ICategoryTriCHService
    {
        Task<List<CategoryTriCh>> GetAllActiveCategoriesAsync();
        Task AddCategoryAsync (CategoryTriCh category);
        Task UpdateCategoryAsync (CategoryTriCh category);
        Task<CategoryTriCh?> GetCategoryByIdAsync(int categoryId);
        Task DeleteCategoryByIdAsync (int categoryId);
    }
}
