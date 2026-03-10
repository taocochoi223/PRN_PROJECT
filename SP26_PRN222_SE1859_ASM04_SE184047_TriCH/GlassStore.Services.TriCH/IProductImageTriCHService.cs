using GlassStore.Entities.TriCH.Models;

namespace GlassStore.Services.TriCH
{
    public interface IProductImageTriCHService
    {
        Task<List<ProductImageTriCh>> GetImagesByProductIdAsync(int productId);
        Task<ProductImageTriCh?> GetImageByIdAsync(int imageId);
        Task AddImageAsync(ProductImageTriCh image);
        Task UpdateImageAsync(ProductImageTriCh image);
        Task DeleteImageAsync(int imageId);
    }
}
