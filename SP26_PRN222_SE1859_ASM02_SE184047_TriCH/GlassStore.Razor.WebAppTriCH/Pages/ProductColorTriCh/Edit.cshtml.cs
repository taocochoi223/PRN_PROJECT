using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GlassStore.Entities.TriCH.Models;
using GlassStore.Repositories.TriCH.DBContext;

namespace GlassStore.Razor.WebAppTriCH.Pages.ProductColorTriCh
{
    public class EditModel : PageModel
    {
        private readonly GlassStore.Repositories.TriCH.DBContext.PRN222_EYEWEARSHOPContext _context;

        public EditModel(GlassStore.Repositories.TriCH.DBContext.PRN222_EYEWEARSHOPContext context)
        {
            _context = context;
        }

        [BindProperty]
        public GlassStore.Entities.TriCH.Models.ProductColorTriCh ProductColorTriCh { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productcolortrich =  await _context.ProductColorTriChes.FirstOrDefaultAsync(m => m.ColorTriChid == id);
            if (productcolortrich == null)
            {
                return NotFound();
            }
            ProductColorTriCh = productcolortrich;
           ViewData["ProductTriChid"] = new SelectList(_context.ProductTriChes, "ProductTriChid", "Brand");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ProductColorTriCh).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductColorTriChExists(ProductColorTriCh.ColorTriChid))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ProductColorTriChExists(int id)
        {
            return _context.ProductColorTriChes.Any(e => e.ColorTriChid == id);
        }
    }
}
