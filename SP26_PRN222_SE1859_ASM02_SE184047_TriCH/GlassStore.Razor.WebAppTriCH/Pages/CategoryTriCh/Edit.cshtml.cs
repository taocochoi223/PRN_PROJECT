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
using GlassStore.Razor.WebAppTriCH.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace GlassStore.Razor.WebAppTriCH.Pages.CategoryTriCh
{
    public class EditModel : PageModel
    {
        private readonly ICategoryTriCHService _categoryService;
        private readonly IHubContext<EyewareHub> _hubContext;
        public EditModel(ICategoryTriCHService service, IHubContext<EyewareHub> hubContext)
        {
            _categoryService = service;
            _hubContext = hubContext;
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
            var categories = await _categoryService.GetAllCategoriesAsync();
            ViewData["ParentId"] = new SelectList(categories, "CategoryTriChid", "CategoryName", CategoryTriCh.ParentId);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var categories = await _categoryService.GetAllCategoriesAsync();
                ViewData["ParentId"] = new SelectList(categories, "CategoryTriChid", "CategoryName", CategoryTriCh.ParentId);
                return Page();
            }
            await _categoryService.UpdateCategoryAsync(CategoryTriCh);
            await _hubContext.Clients.All.SendAsync("ReceiveHubUpdate_categoryTriCh", new
            {
                categoryTriChid = CategoryTriCh.CategoryTriChid,
                categoryName = CategoryTriCh.CategoryName,
                slug = CategoryTriCh.Slug,
                parentId = CategoryTriCh.ParentId,
                status = CategoryTriCh.Status
            });
            return RedirectToPage("./Manage");

        }
    }
}
