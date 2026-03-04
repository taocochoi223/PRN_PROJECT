using GlassStore.Entities.TriCH.Models;

namespace GlassStore.Services.TriCH
{
    public interface IProductColorTriCHService
    {
        Task<List<ProductColorTriCh>> GetColorsByProductIdAsync(int productId);
        Task<ProductColorTriCh?> GetColorByIdAsync(int colorId);
        Task AddColorAsync(ProductColorTriCh color);
        Task UpdateColorAsync(ProductColorTriCh color);
        Task DeleteColorAsync(int colorId);
    }
}
