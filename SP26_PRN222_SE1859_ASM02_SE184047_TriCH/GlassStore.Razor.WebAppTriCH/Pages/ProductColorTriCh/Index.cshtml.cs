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

namespace GlassStore.Razor.WebAppTriCH.Pages.ProductColorTriCh
{
    public class IndexModel : PageModel
    {
        private readonly IProductColorTriCHService _service;
        private readonly IProductTriCHService _productService;
        public IndexModel(IProductColorTriCHService service, IProductTriCHService productService)
        {
            _service = service;
            _productService = productService;
        }

        public IList<GlassStore.Entities.TriCH.Models.ProductColorTriCh> ProductColorTriCh { get;set; } = default!;
        public GlassStore.Entities.TriCH.Models.ProductTriCh Product { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync (int productId)
        {
            Product = await _productService.GetProductByIdAsync(productId);
            if (Product == null)
            {
                return NotFound();
            }
            ProductColorTriCh = await _service.GetColorsByProductIdAsync(productId);
            return Page();
        }
    }
}
