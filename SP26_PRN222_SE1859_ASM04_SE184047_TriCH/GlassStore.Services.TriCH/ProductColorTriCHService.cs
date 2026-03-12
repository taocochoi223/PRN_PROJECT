using GlassStore.Entities.TriCH.Models;
using GlassStore.Repositories.TriCH;

namespace GlassStore.Services.TriCH
{
    public class ProductColorTriCHService : IProductColorTriCHService
    {
        private readonly ProductColorTriCHRepository _repo;
        private readonly ProductTriCHRepository _productRepo;

        public ProductColorTriCHService(ProductColorTriCHRepository repo, ProductTriCHRepository productRepo)
        {
            _repo = repo;
            _productRepo = productRepo;
        }

        public async Task<List<ProductColorTriCh>> GetColorsByProductIdAsync(int productId)
            => await _repo.GetColorsByProductIdAsync(productId);

        public async Task<ProductColorTriCh?> GetColorByIdAsync(int colorId)
            => await _repo.GetColorByIdAsync(colorId);

        public async Task AddColorAsync(ProductColorTriCh color)
        {
            await _repo.CreateAsync(color);
            await SyncProductStockAsync(color.ProductTriChid);
        }

        public async Task UpdateColorAsync(ProductColorTriCh color)
        {
            await _repo.UpdateAsync(color);
            await SyncProductStockAsync(color.ProductTriChid);
        }

        public async Task DeleteColorAsync(int colorId)
        {
            var color = await _repo.GetColorByIdAsync(colorId);
            if (color != null)
            {
                int productId = color.ProductTriChid;
                await _repo.RemoveAsync(color);
                await SyncProductStockAsync(productId);
            }
        }

        private async Task SyncProductStockAsync(int productId)
        {
            var product = await _productRepo.GetProductByIdAsync(productId);
            if (product != null)
            {
                var colors = await _repo.GetColorsByProductIdAsync(productId);
                product.StockQuantity = colors.Sum(c => c.StockQuantity ?? 0);
                await _productRepo.UpdateAsync(product);
            }
        }
    }
}
