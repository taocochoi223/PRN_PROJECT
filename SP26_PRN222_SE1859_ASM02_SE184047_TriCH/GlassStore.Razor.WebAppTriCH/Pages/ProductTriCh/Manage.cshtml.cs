using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using GlassStore.Services.TriCH;

namespace GlassStore.Razor.WebAppTriCH.Pages.ProductTriCh
{
    [Authorize(Roles = "1")]
    public class ManageModel : PageModel
    {
        //private readonly GlassStore.Repositories.TriCH.DBContext.PRN222_EYEWEARSHOPContext _context;
        private readonly IProductTriCHService _productService;
        public ManageModel(IProductTriCHService productService)
        {
            _productService = productService;
        }

        public IList<GlassStore.Entities.TriCH.Models.ProductTriCh> ProductTriCh { get;set; } = default!;
        
        [Microsoft.AspNetCore.Mvc.BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;

        [Microsoft.AspNetCore.Mvc.BindProperty(SupportsGet = true)]
        public string? SearchToken { get; set; }

        public int TotalPages { get; set; }
        public int PageSize { get; set; } = 6;

        public async Task OnGetAsync()
        {
            var result = await _productService.GetAllProductPagedAsync(PageIndex, PageSize, SearchToken);
            ProductTriCh = result.Items;
            TotalPages = (int)Math.Ceiling(result.TotalCount / (double)PageSize);
        }
    }
}
