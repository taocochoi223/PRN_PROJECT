using GlassStore.Entities.TriCH.Models;

namespace GlassStore.Services.TriCH
{
    public interface ICategoryTriCHService
    {
        Task<List<CategoryTriCh>> GetAllActiveCategoriesAsync();
        Task<List<CategoryTriCh>> GetAllCategoriesAsync();
    }
}
