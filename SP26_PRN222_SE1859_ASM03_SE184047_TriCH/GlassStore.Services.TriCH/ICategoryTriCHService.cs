using GlassStore.Entities.TriCH.Models;

namespace GlassStore.Services.TriCH
{
    public interface ICategoryTriCHService
    {
        Task<List<CategoryTriCh>> GetAllActiveCategoriesAsync();
        Task<List<CategoryTriCh>> GetAllCategoriesAsync();
        Task<CategoryTriCh?> GetCategoryByIdAsync(int categoryId);
        Task AddCategoryAsync(CategoryTriCh category);
        Task DeleteCategoryAsync(int categoryId);
        Task UpdateCategoryAsync(CategoryTriCh category);
    }
}
