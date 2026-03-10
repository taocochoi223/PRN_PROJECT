using GlassStore.Entities.TriCH.Models;
using GlassStore.Repositories.TriCH;

namespace GlassStore.Services.TriCH
{
    public class ProductColorTriCHService : IProductColorTriCHService
    {
        private readonly ProductColorTriCHRepository _repo;

        public ProductColorTriCHService(ProductColorTriCHRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<ProductColorTriCh>> GetColorsByProductIdAsync(int productId)
            => await _repo.GetColorsByProductIdAsync(productId);

        public async Task<ProductColorTriCh?> GetColorByIdAsync(int colorId)
            => await _repo.GetColorByIdAsync(colorId);

        public async Task AddColorAsync(ProductColorTriCh color)
            => await _repo.CreateAsync(color);

        public async Task UpdateColorAsync(ProductColorTriCh color)
            => await _repo.UpdateAsync(color);

        public async Task DeleteColorAsync(int colorId)
        {
            var color = await _repo.GetColorByIdAsync(colorId);
            if (color != null)
                await _repo.RemoveAsync(color);
        }
    }
}
