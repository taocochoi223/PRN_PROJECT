using GlassStore.Entities.TriCH.Models;
using GlassStore.Repositories.TriCH;

namespace GlassStore.Services.TriCH
{
    public class ProductImageTriCHService : IProductImageTriCHService
    {
        private readonly ProductImageTriCHRepository _repo;

        public ProductImageTriCHService(ProductImageTriCHRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<ProductImageTriCh>> GetImagesByProductIdAsync(int productId)
            => await _repo.GetImagesByProductIdAsync(productId);

        public async Task<ProductImageTriCh?> GetImageByIdAsync(int imageId)
            => await _repo.GetImageByIdAsync(imageId);

        public async Task AddImageAsync(ProductImageTriCh image)
            => await _repo.CreateAsync(image);

        public async Task UpdateImageAsync(ProductImageTriCh image)
            => await _repo.UpdateAsync(image);

        public async Task DeleteImageAsync(int imageId)
        {
            var image = await _repo.GetImageByIdAsync(imageId);
            if (image != null)
                await _repo.RemoveAsync(image);
        }
    }
}
