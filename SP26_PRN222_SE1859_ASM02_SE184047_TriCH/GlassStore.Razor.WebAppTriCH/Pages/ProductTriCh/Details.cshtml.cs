using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GlassStore.Entities.TriCH.Models;
using GlassStore.Repositories.TriCH.DBContext;

namespace GlassStore.Razor.WebAppTriCH.Pages.ProductTriCh
{
    public class DetailsModel : PageModel
    {
        private readonly GlassStore.Repositories.TriCH.DBContext.PRN222_EYEWEARSHOPContext _context;

        public DetailsModel(GlassStore.Repositories.TriCH.DBContext.PRN222_EYEWEARSHOPContext context)
        {
            _context = context;
        }

        public GlassStore.Entities.TriCH.Models.ProductTriCh ProductTriCh { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producttrich = await _context.ProductTriChes.FirstOrDefaultAsync(m => m.ProductTriChid == id);
            if (producttrich == null)
            {
                return NotFound();
            }
            else
            {
                ProductTriCh = producttrich;
            }
            return Page();
        }
    }
}
