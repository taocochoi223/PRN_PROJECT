using GlassStore.Entities.TriCH.Models;

namespace GlassStore.Services.TriCH
{
    public interface IProductTriCHService
    {
        Task<List<ProductTriCh>> GetAllProductAsync(string? search = null);
        Task<ProductTriCh?> GetProductByIdAsync(int productId);
        Task<List<ProductTriCh>> GetProductByCategoryIdAsync(int categoryId);
        Task AddProductAsync(ProductTriCh product);
        Task UpdateProductAsync(ProductTriCh product);
        Task DeleteProductAsync(int id);
        Task<bool> SkuExistsAsync(string sku);
    }
}
