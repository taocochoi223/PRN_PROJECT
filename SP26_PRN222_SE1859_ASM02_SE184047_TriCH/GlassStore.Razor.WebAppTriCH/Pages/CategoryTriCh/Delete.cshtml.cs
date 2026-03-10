using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GlassStore.Entities.TriCH.Models;
using GlassStore.Repositories.TriCH.DBContext;
using GlassStore.Services.TriCH;

namespace GlassStore.Razor.WebAppTriCH.Pages.CategoryTriCh
{
    public class DeleteModel : PageModel
    {
        private readonly ICategoryTriCHService _categoryService;

        public DeleteModel(ICategoryTriCHService cate)
        {
            _categoryService = cate;
        }

        [BindProperty]
        public GlassStore.Entities.TriCH.Models.CategoryTriCh CategoryTriCh { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cate = await _categoryService.GetCategoryByIdAsync(id.Value);


            if (cate == null)
            {
                return NotFound();
            }
           CategoryTriCh = cate;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categorytrich = await _context.CategoryTriChes.FindAsync(id);
            if (categorytrich != null)
            {
                CategoryTriCh = categorytrich;
                _context.CategoryTriChes.Remove(CategoryTriCh);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
