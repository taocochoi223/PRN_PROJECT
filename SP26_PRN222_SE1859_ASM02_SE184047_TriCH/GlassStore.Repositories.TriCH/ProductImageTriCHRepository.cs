using GlassStore.Entities.TriCH.Models;
using GlassStore.Repositories.TriCH.Basic;
using GlassStore.Repositories.TriCH.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlassStore.Repositories.TriCH
{
    public class ProductImageTriCHRepository : GenericRepository<ProductImageTriCh>
    {
        public ProductImageTriCHRepository(PRN222_EYEWEARSHOPContext context)
        {
            _context = context;
        }

        public async Task<List<ProductImageTriCh>> GetImagesByProductIdAsync(int productId)
        {
            return await _context.ProductImageTriChes
                .Include(i => i.ColorTriCh)
                .Where(i => i.ProductTriChid == productId)
                .OrderBy(i => i.DisplayOrder)
                .ToListAsync();
        }

        public async Task<ProductImageTriCh?> GetImageByIdAsync(int imageId)
        {
            return await _context.ProductImageTriChes
                .Include(i => i.ColorTriCh)
                .FirstOrDefaultAsync(i => i.ImageTriChid == imageId);
        }
    }
}
