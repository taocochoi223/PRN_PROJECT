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

        public async Task<(List<ProductTriCh> Items, int TotalCount)> GetAllProductAsync(int pageIndex, int pageSize, string search = null)
        {
            var query = _context.ProductTriChes
                .Include(p => p.CategoryTriCh)
                .Include(p => p.ProductImageTriChes)
                .Include(p => p.ProductColorTriChes)
                .Where(p => p.Status == 1);
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(p => p.ProductName.Contains(search));
            }

            int total = await query.CountAsync();

            var items = await query
                .OrderByDescending(p => p.CreatedAt)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();

            return (items, total);
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
                .Include(p => p.ProductColorTriChes)
                .Where(p => p.CategoryTriChid == categoryId && p.Status == 1)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
            return await products;
        }

        public async Task<bool> SkuExistsAsync(string sku)
        {
            if (string.IsNullOrWhiteSpace(sku)) return false;
            return await _context.ProductTriChes.AnyAsync(p => p.Sku == sku);
        }
    }
}
