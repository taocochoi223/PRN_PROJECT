using Microsoft.AspNetCore.Mvc.RazorPages;
using GlassStore.Services.TriCH;

namespace GlassStore.Razor.WebAppTriCH.Pages.ProductTriCh
{
    public class IndexModel : PageModel
    {
        //private readonly GlassStore.Repositories.TriCH.DBContext.PRN222_EYEWEARSHOPContext _context;
        private readonly IProductTriCHService _productService;
        public IndexModel(IProductTriCHService productService)
        {
            _productService = productService;
        }

        public IList<GlassStore.Entities.TriCH.Models.ProductTriCh> ProductTriCh { get;set; } = default!;

        public async Task OnGetAsync()
        {
            ProductTriCh = await _productService.GetAllProductAsync();
        }
    }
}
