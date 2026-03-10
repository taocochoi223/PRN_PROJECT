using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using GlassStore.Entities.TriCH.Models;
using GlassStore.Repositories.TriCH.DBContext;

namespace GlassStore.Razor.WebAppTriCH.Pages.CategoryTriCh
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
        ViewData["ParentId"] = new SelectList(_context.CategoryTriChes, "CategoryTriChid", "CategoryName");
            return Page();
        }

        [BindProperty]
        public GlassStore.Entities.TriCH.Models.CategoryTriCh CategoryTriCh { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.CategoryTriChes.Add(CategoryTriCh);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
