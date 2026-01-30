using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlassStore.Repositories.TriCH;
using GlassStore.Entities.TriCH.Models;

namespace GlassStore.Services.TriCH
{
    public class ProductTriCHService : IProductTriCHService
    {
        private readonly ProductTriCHRepository _repo;

        public ProductTriCHService(ProductTriCHRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<ProductTriCh>> GetAllProductAsync()
        {
            return await _repo.GetAllProductAsync();
        }

        public async Task<ProductTriCh?> GetProductByIdAsync(int productId)
        {
            return await _repo.GetProductByIdAsync(productId);
        }

        public async Task<List<ProductTriCh>> SearchProducts( string search)
        {
            return await _repo.SearchProducts(search);
        }

        public async Task<List<ProductTriCh>> GetProductByCategoryIdAsync(int categoryId)
        {
            return await _repo.GetProductByCategoryIdAsync(categoryId);
        }

        public async Task AddProductAsync(ProductTriCh product)
        {
            await _repo.CreateAsync(product);
        }

        public async Task UpdateProductAsync(ProductTriCh product)
        {
            await _repo.UpdateAsync(product);
        }

        public async Task DeleteProductAsync(int productId)
        {
            var product = await _repo.GetProductByIdAsync(productId);
            if (product != null)
            {
                await _repo.RemoveAsync(product);
            }
        }
    }
}
