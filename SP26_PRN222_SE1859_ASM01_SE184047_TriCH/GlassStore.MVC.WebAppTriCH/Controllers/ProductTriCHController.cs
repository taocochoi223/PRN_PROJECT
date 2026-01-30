using GlassStore.Entities.TriCH.Models;
using GlassStore.Repositories.TriCH.DBContext; // Nhớ using namespace này
using GlassStore.Services.TriCH;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GlassStore.MVC.WebAppTriCH.Controllers
{
    [TypeFilter(typeof(Filters.AuthenticationFilter))]
    public class ProductTriCHController : Controller
    {
        private readonly IProductTriCHService _service;
        private readonly PRN222_EYEWEARSHOPContext _context;

        public ProductTriCHController(IProductTriCHService service, PRN222_EYEWEARSHOPContext context)
        {
            _service = service;
            _context = context;
        }

        private void LoadCategoriesToSidebar()
        {
            var categories = _context.CategoryTriChes.Where(c => c.Status == 1).ToList();
            ViewData["Categories"] = categories;
        }

        public async Task<IActionResult> Index(string search)
        {
          
            LoadCategoriesToSidebar();

            ViewData["CurrentFilter"] = search;
            ViewData["PageTitle"] = "Tất cả sản phẩm";

            var products = await _service.SearchProducts(search);
            return View("Index", products);
        }

        public async Task<IActionResult> ProductsByCategory(int categoryId)
        {
            LoadCategoriesToSidebar();
            var category = await _context.CategoryTriChes.FindAsync(categoryId);
            ViewData["PageTitle"] = category?.CategoryName ?? "Sản phẩm";
            ViewData["CurrentCategoryId"] = categoryId;
            var products = await _service.GetProductByCategoryIdAsync(categoryId);
            return View("Index", products);
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _service.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View("Details", product);
        }
    }
}
