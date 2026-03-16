using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GlassStore.Entities.TriCH.Models;
using GlassStore.Services.TriCH;
using GlassStore.Razor.WebAppTriCH.Filters;
using System.Security.Claims;

namespace GlassStore.Razor.WebAppTriCH.Pages.CategoryTriCh
{
    [AuthenticationFilter]
    public class ManageModel : PageModel
    {
        private readonly ICategoryTriCHService _categoryService;

        public ManageModel(ICategoryTriCHService category)
        {
            _categoryService = category;
        }

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public IList<GlassStore.Entities.TriCH.Models.CategoryTriCh> CategoryTriCh { get;set; } = default!;

        private bool IsAdmin()
        {
            var roleId = User.FindFirst(ClaimTypes.Role)?.Value;
            return roleId == "1";
        }

        public async Task<IActionResult> OnGetAsync()
        {
            // Cho phép xem danh sách
            var categories = await _categoryService.GetAllCategoriesAsync();

            if (!string.IsNullOrEmpty(SearchString))
            {
                categories = categories.Where(c => c.CategoryName.Contains(SearchString, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            CategoryTriCh = categories;
            return Page();
        }
    }
}
