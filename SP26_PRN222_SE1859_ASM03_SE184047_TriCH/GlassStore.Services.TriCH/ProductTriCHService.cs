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

        public async Task<(List<ProductTriCh> Items, int TotalCount)> GetAllProductPagedAsync(int pageIndex, int pageSize, string? search = null)
        {
            return await _repo.GetAllProductAsync(pageIndex, pageSize, search);
        }


        public async Task<bool> SkuExistsAsync(string sku)
        {
            return await _repo.SkuExistsAsync(sku);
        }

        public async Task<ProductTriCh?> GetProductByIdAsync(int productId)
        {
            return await _repo.GetProductByIdAsync(productId);
        }

        public async Task<List<ProductTriCh>> GetProductByCategoryIdAsync(int categoryId)
        {
            return await _repo.GetProductByCategoryIdAsync(categoryId);
        }

        public async Task AddProductAsync(ProductTriCh product)
        {
            if (!string.IsNullOrWhiteSpace(product.Sku))
            {
                var exists = await _repo.SkuExistsAsync(product.Sku);
                if (exists)
                {
                    throw new InvalidOperationException("SKU đã tồn tại. Vui lòng nhập mã khác.");
                }
            }
            product.Status = 1; 
            product.CreatedAt = DateTime.Now;
            await _repo.CreateAsync(product);
        }

        public async Task UpdateProductAsync(ProductTriCh product)
        {
            await _repo.UpdateAsync(product);
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _repo.GetProductByIdAsync(id);
            if (product != null)
            {
                product.Status = 0;
                await _repo.UpdateAsync(product);
            }
        }
    }
}
