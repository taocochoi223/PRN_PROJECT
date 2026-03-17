using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GlassStore.Services.TriCH;
using GlassStore.Razor.WebAppTriCH.Filters;
using System.Security.Claims;

namespace GlassStore.Razor.WebAppTriCH.Pages.ProductTriCh
{
    [AuthenticationFilter]
    public class ManageModel : PageModel
    {
        private readonly IProductTriCHService _productService;
        public ManageModel(IProductTriCHService productService)
        {
            _productService = productService;
        }

        private bool IsAdmin()
        {
            var roleId = User.FindFirst(ClaimTypes.Role)?.Value;
            return roleId == "1";
        }

        public IList<GlassStore.Entities.TriCH.Models.ProductTriCh> ProductTriCh { get;set; } = default!;
        
        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public string? SearchToken { get; set; }

        public int TotalPages { get; set; }
        public int PageSize { get; set; } = 5;

        public async Task<IActionResult> OnGetAsync()
        {

            var result = await _productService.GetAllProductPagedAsync(PageIndex - 1, PageSize, SearchToken);
            ProductTriCh = result.Items;
            TotalPages = (int)Math.Ceiling(result.TotalCount / (double)PageSize);
            return Page();
        }
    }
}
