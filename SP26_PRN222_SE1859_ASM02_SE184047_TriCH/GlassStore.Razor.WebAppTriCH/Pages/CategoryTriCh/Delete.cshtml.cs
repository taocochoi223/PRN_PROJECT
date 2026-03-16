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
using Microsoft.AspNetCore.SignalR;
using GlassStore.Razor.WebAppTriCH.Hubs;
using GlassStore.Razor.WebAppTriCH.Filters;
using System.Security.Claims;

namespace GlassStore.Razor.WebAppTriCH.Pages.CategoryTriCh
{
    [AuthenticationFilter]
    public class DeleteModel : PageModel
    {
        private readonly ICategoryTriCHService _categoryService;
        private readonly IHubContext<EyewareHub> _hubContext;

        public DeleteModel(ICategoryTriCHService cate, IHubContext<EyewareHub> hubContext)
        {
            _categoryService = cate;
            _hubContext = hubContext;
        }

        [BindProperty]
        public GlassStore.Entities.TriCH.Models.CategoryTriCh CategoryTriCh { get; set; } = default!;

        private bool IsAdmin()
        {
            var roleId = User.FindFirst(ClaimTypes.Role)?.Value;
            return roleId == "1";
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (!IsAdmin()) return RedirectToPage("/Index");
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
            if (!IsAdmin()) return RedirectToPage("/Index");
            if (id == null)
            {
                return NotFound();
            }

            var categorytrich = await _categoryService.GetCategoryByIdAsync(id.Value);
            if (categorytrich != null)
            {
                await _categoryService.DeleteCategoryAsync(id.Value);
                await _hubContext.Clients.All.SendAsync("ReceiveHubDelete_categoryTriCh", id.Value);

            }

            return RedirectToPage("./Manage");
        }
    }
}
