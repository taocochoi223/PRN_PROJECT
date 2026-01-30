using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlassStore.Entities.TriCH.Models;

namespace GlassStore.Services.TriCH
{
    public interface IProductTriCHService
    {
        Task<List<ProductTriCh>> GetAllProductAsync();
        Task<ProductTriCh?> GetProductByIdAsync(int productId);
        Task<List<ProductTriCh>> SearchProducts(string search);
        Task<List<ProductTriCh>> GetProductByCategoryIdAsync(int categoryId);
        Task AddProductAsync(ProductTriCh product);
        Task UpdateProductAsync(ProductTriCh product);
        Task DeleteProductAsync(int productId);
    }
}
