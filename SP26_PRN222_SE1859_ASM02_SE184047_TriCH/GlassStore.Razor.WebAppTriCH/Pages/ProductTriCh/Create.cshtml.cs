using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using GlassStore.Entities.TriCH.Models;
using GlassStore.Repositories.TriCH.DBContext;
using GlassStore.Services.TriCH;

namespace GlassStore.Razor.WebAppTriCH.Pages.ProductTriCh
{
    public class CreateModel : PageModel
    {
        //private readonly GlassStore.Repositories.TriCH.DBContext.PRN222_EYEWEARSHOPContext _context;
        private readonly IProductTriCHService _productService;
        public CreateModel(IProductTriCHService productService)
        {
            _productService = productService;
        }

        public IActionResult OnGet()
        {
        ViewData["CategoryTriChid"] = new SelectList(_context.CategoryTriChes, "CategoryTriChid", "CategoryName");
            return Page();
        }

        [BindProperty]
        public ProductTriCh ProductTriCh { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ProductTriChes.Add(ProductTriCh);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
