using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GlassStore.Entities.TriCH.Models;
using GlassStore.Repositories.TriCH.DBContext;

namespace GlassStore.Razor.WebAppTriCH.Pages.ProductColorTriCh
{
    public class DetailsModel : PageModel
    {
        private readonly GlassStore.Repositories.TriCH.DBContext.PRN222_EYEWEARSHOPContext _context;

        public DetailsModel(GlassStore.Repositories.TriCH.DBContext.PRN222_EYEWEARSHOPContext context)
        {
            _context = context;
        }

        public GlassStore.Entities.TriCH.Models.ProductColorTriCh ProductColorTriCh { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productcolortrich = await _context.ProductColorTriChes.FirstOrDefaultAsync(m => m.ColorTriChid == id);
            if (productcolortrich == null)
            {
                return NotFound();
            }
            else
            {
                ProductColorTriCh = productcolortrich;
            }
            return Page();
        }
    }
}
