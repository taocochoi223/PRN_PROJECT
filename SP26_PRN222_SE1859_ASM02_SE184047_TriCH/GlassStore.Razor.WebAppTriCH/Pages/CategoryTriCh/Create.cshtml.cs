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
using Microsoft.AspNetCore.SignalR;
using GlassStore.Razor.WebAppTriCH.Hubs;

namespace GlassStore.Razor.WebAppTriCH.Pages.CategoryTriCh
{
    public class CreateModel : PageModel
    {
        private readonly ICategoryTriCHService _categoryService;
        private readonly IHubContext<EyewareHub> _hubContext;

        public CreateModel(ICategoryTriCHService categoryService, IHubContext<EyewareHub> hubContext)
        {
            _categoryService = categoryService;
            _hubContext = hubContext;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            ViewData["ParentId"] = new SelectList(categories, "CategoryTriChid", "CategoryName");
            return Page();
        }

        [BindProperty]
        public GlassStore.Entities.TriCH.Models.CategoryTriCh CategoryTriCh { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var categories = await _categoryService.GetAllCategoriesAsync();
                ViewData["ParentId"] = new SelectList(categories, "CategoryTriChid", "CategoryName");
                return Page();
            }

            await _categoryService.AddCategoryAsync(CategoryTriCh);

            // Fetch the newly created category with potential related data for broadcast
            var newItem = await _categoryService.GetCategoryByIdAsync(CategoryTriCh.CategoryTriChid);
            if (newItem != null)
            {
                await _hubContext.Clients.All.SendAsync("ReceiveHubCreate_categoryTriCh", new
                {
                    categoryTriChid = newItem.CategoryTriChid,
                    categoryName = newItem.CategoryName,
                    slug = newItem.Slug,
                    parentId = newItem.ParentId,
                    status = newItem.Status,
                    parentName = newItem.Parent != null ? newItem.Parent.CategoryName : ""
                });
            }

            return RedirectToPage("./Manage");
        }
    }
}
