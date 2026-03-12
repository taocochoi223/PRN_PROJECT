using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using GlassStore.Entities.TriCH.Models;
using GlassStore.Repositories.TriCH.DBContext;

namespace GlassStore.Razor.WebAppTriCH.Pages.ProductColorTriCh
{
    public class CreateModel : PageModel
    {
        private readonly GlassStore.Repositories.TriCH.DBContext.PRN222_EYEWEARSHOPContext _context;

        public CreateModel(GlassStore.Repositories.TriCH.DBContext.PRN222_EYEWEARSHOPContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["ProductTriChid"] = new SelectList(_context.ProductTriChes, "ProductTriChid", "Brand");
            return Page();
        }

        [BindProperty]
        public GlassStore.Entities.TriCH.Models.ProductColorTriCh ProductColorTriCh { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ProductColorTriChes.Add(ProductColorTriCh);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
