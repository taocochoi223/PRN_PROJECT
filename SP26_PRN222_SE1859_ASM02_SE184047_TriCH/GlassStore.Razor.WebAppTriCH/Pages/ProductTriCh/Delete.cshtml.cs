using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GlassStore.Entities.TriCH.Models;
using GlassStore.Repositories.TriCH.DBContext;
using Microsoft.AspNetCore.SignalR;
using GlassStore.Razor.WebAppTriCH.Hubs;
using GlassStore.Services.TriCH;

namespace GlassStore.Razor.WebAppTriCH.Pages.ProductTriCh
{
    public class DeleteModel : PageModel
    {
        private readonly IProductTriCHService _productService;
        private readonly IHubContext<EyewareHub> _hubContext;
        public DeleteModel(IProductTriCHService productService, IHubContext<EyewareHub> hubContext)
        {
            _productService = productService;
            _hubContext = hubContext;
        }

        [BindProperty]
        public GlassStore.Entities.TriCH.Models.ProductTriCh ProductTriCh { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = await _productService.GetProductByIdAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }
            ProductTriCh = product;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            await _productService.DeleteProductAsync(id.Value);
            await _hubContext.Clients.All.SendAsync("ProductDeleted", id);
            return RedirectToPage("./Manage");
        }
    }
}
