using GlassStore.Entities.TriCH.Models;

namespace GlassStore.Services.TriCH
{
    public interface IProductTriCHService
    {
        // Thay đổi định nghĩa hàm GetAll
        Task<(List<ProductTriCh> Items, int TotalCount)> GetAllProductPagedAsync(int pageIndex, int pageSize, string? search = null);
        Task<ProductTriCh?> GetProductByIdAsync(int productId);
        Task<List<ProductTriCh>> GetProductByCategoryIdAsync(int categoryId);
        Task AddProductAsync(ProductTriCh product);
        Task UpdateProductAsync(ProductTriCh product);
        Task DeleteProductAsync(int id);
        Task<bool> SkuExistsAsync(string sku);
    }
}
