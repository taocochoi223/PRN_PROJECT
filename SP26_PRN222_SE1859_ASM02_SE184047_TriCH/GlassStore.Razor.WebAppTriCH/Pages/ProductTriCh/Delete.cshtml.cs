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
    public class DeleteModel : PageModel
    {
        private readonly GlassStore.Repositories.TriCH.DBContext.PRN222_EYEWEARSHOPContext _context;

        public DeleteModel(GlassStore.Repositories.TriCH.DBContext.PRN222_EYEWEARSHOPContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ProductTriCh ProductTriCh { get; set; } = default!;

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producttrich = await _context.ProductTriChes.FindAsync(id);
            if (producttrich != null)
            {
                ProductTriCh = producttrich;
                _context.ProductTriChes.Remove(ProductTriCh);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
