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

        public async Task OnGetAsync()
        {
            ProductTriCh = await _productService.GetAllProductAsync();
        }
    }
}
