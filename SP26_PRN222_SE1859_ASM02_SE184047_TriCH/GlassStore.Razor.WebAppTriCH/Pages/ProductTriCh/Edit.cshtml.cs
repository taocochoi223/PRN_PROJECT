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

namespace GlassStore.Razor.WebAppTriCH.Pages.ProductTriCh
{
    public class EditModel : PageModel
    {
        private readonly GlassStore.Repositories.TriCH.DBContext.PRN222_EYEWEARSHOPContext _context;

        public EditModel(GlassStore.Repositories.TriCH.DBContext.PRN222_EYEWEARSHOPContext context)
        {
            _context = context;
        }

        [BindProperty]
        public GlassStore.Entities.TriCH.Models.ProductTriCh ProductTriCh { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producttrich =  await _context.ProductTriChes.FirstOrDefaultAsync(m => m.ProductTriChid == id);
            if (producttrich == null)
            {
                return NotFound();
            }
            ProductTriCh = producttrich;
           ViewData["CategoryTriChid"] = new SelectList(_context.CategoryTriChes, "CategoryTriChid", "CategoryName");
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

            _context.Attach(ProductTriCh).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductTriChExists(ProductTriCh.ProductTriChid))
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

        private bool ProductTriChExists(int id)
        {
            return _context.ProductTriChes.Any(e => e.ProductTriChid == id);
        }
    }
}
