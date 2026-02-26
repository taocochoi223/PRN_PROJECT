using GlassStore.Entities.TriCH.Models;
using GlassStore.Repositories.TriCH.Basic;
using GlassStore.Repositories.TriCH.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace GlassStore.Repositories.TriCH
{
    public class ProductTriCHRepository : GenericRepository<ProductTriCh>
    {
        public ProductTriCHRepository(PRN222_EYEWEARSHOPContext context)
        {
            _context = context;
        }

        public async Task<List<ProductTriCh>> GetAllProductAsync(string search = null)
        {
            var query = _context.ProductTriChes
                .Include(p => p.CategoryTriCh)
                .Include(p => p.ProductImageTriChes)
                .Where(p => p.Status == 1);
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(p => p.ProductName.Contains(search));
            }

            var products = await query.OrderByDescending(p => p.CreatedAt).ToListAsync();
            return products;
        }

        public async Task<ProductTriCh?> GetProductByIdAsync(int productId)
        {
            var product = _context.ProductTriChes
                .Include(p => p.CategoryTriCh)
                .Include(p => p.ProductImageTriChes)
                .Include(p => p.ProductColorTriChes)
                .FirstOrDefaultAsync(p => p.ProductTriChid == productId && p.Status == 1);
            return await product;
        }


        public async Task<List<ProductTriCh>> GetProductByCategoryIdAsync(int categoryId)
        {
            var products = _context.ProductTriChes
                .Include(p => p.CategoryTriCh)
                .Include(p => p.ProductImageTriChes)
                .Where(p => p.CategoryTriChid == categoryId && p.Status == 1)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
            return await products;
        }
    }
}
