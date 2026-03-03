using GlassStore.Entities.TriCH.Models;
using GlassStore.Repositories.TriCH.Basic;
using GlassStore.Repositories.TriCH.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GlassStore.Repositories.TriCH
{
    public class ProductColorTriCHRepository : GenericRepository<ProductColorTriCh>
    {
        public ProductColorTriCHRepository(PRN222_EYEWEARSHOPContext context)
        {
            _context = context;
        }
        
        public async Task<List<ProductColorTriCh>> GetColorsByProductIdAsync( int productId)
        {
            return await _context.ProductColorTriChes
                .Where(c => c.ProductTriChid == productId)
                .ToListAsync();
        }

        public async Task<ProductColorTriCh?> GetColorByIdAsync(int colorId)
        {
            return await _context.ProductColorTriChes
                .FirstOrDefaultAsync(c => c.ColorTriChid == colorId);
        }
    }
}
