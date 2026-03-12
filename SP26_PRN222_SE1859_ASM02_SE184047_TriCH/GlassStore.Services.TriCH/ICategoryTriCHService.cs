using GlassStore.Entities.TriCH.Models;

namespace GlassStore.Services.TriCH
{
    public interface ICategoryTriCHService
    {
        Task<List<CategoryTriCh>> GetAllActiveCategoriesAsync();
        Task<List<CategoryTriCh>> GetAllCategoriesAsync();                                              // Dùng cho dropdown (không phân trang)
        Task<(List<CategoryTriCh> Items, int TotalCount)> GetAllCategoriesPagedAsync(int pageIndex, int pageSize, string? search = null); // Dùng cho trang list
        Task<CategoryTriCh?> GetCategoryByIdAsync(int categoryId);
        Task AddCategoryAsync(CategoryTriCh category);
        Task DeleteCategoryAsync(int categoryId);
        Task UpdateCategoryAsync(CategoryTriCh category);
    }
}
