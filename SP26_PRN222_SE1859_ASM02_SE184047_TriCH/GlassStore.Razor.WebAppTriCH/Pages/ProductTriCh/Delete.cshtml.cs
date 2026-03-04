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

namespace GlassStore.Razor.WebAppTriCH.Pages.ProductTriCh
{
    public class DeleteModel : PageModel
    {
        private readonly PRN222_EYEWEARSHOPContext _context;
        private readonly IHubContext<EyewareHub> _hubContext;

        public DeleteModel(PRN222_EYEWEARSHOPContext context, IHubContext<EyewareHub> hubContext)
        {
            _context = context;
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

            var producttrich = await _context.ProductTriChes.FirstOrDefaultAsync(m => m.ProductTriChid == id);

            if (producttrich == null)
            {
                return NotFound();
            }
            else
            {
                ProductTriCh = producttrich;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producttrich = await _context.ProductTriChes.FindAsync(id);
            if (producttrich != null)
            {
                ProductTriCh = producttrich;
                _context.ProductTriChes.Remove(ProductTriCh);
                await _context.SaveChangesAsync();

                // 🔔 Thông báo tới tất cả client đang kết nối về việc xóa sản phẩm
                await _hubContext.Clients.All.SendAsync("ProductDeleted", id);
            }

            return RedirectToPage("./Index");
        }
    }
}
