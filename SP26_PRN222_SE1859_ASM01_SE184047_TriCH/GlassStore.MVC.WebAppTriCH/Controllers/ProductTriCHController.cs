using GlassStore.Entities.TriCH.Models;
using GlassStore.Services.TriCH;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace GlassStore.MVC.WebAppTriCH.Controllers
{
    // Controller quản lý ProductTriCh
    // Kết hợp cả chức năng hiển thị (Public) và quản lý (Admin) để đảm bảo "giống tên bảng"
    [TypeFilter(typeof(Filters.AuthenticationFilter))]
    public class ProductTriCHController : Controller
    {
        private readonly IProductTriCHService _productService;
        private readonly ICategoryTriCHService _categoryService;

        public ProductTriCHController(IProductTriCHService productService, ICategoryTriCHService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        // --- Helper Methods ---
        private bool IsAdmin()
        {
            var roleId = User.FindFirst(ClaimTypes.Role)?.Value;
            return roleId == "1";
        }

        // --- PUBLIC ACTIONS (Khách hàng xem sản phẩm) ---
        
        // Action duy nhất hiển thị danh sách sản phẩm
        // Hỗ trợ cả tìm kiếm (search) và lọc theo danh mục (categoryId)
        public async Task<IActionResult> Index(string search, int? categoryId)
        {
            // 1. Lấy danh sách danh mục để hiển thị bên Sidebar (cột trái)
            var categories = await _categoryService.GetAllActiveCategoriesAsync();
            ViewData["Categories"] = categories;

            // 2. Lấy danh sách sản phẩm
            List<ProductTriCh> products;

            if (categoryId.HasValue)
            {
                // Nếu có chọn danh mục -> Lấy theo danh mục
                products = await _productService.GetProductByCategoryIdAsync(categoryId.Value);
                // Tìm tên danh mục để hiển thị tiêu đề
                var selectedCategory = categories.FirstOrDefault(c => c.CategoryTriChid == categoryId.Value);
                ViewData["PageTitle"] = selectedCategory?.CategoryName ?? "Sản phẩm";
                ViewData["CurrentCategoryId"] = categoryId.Value;
            }
            else if (!string.IsNullOrEmpty(search))
            {
                // Nếu có từ khóa tìm kiếm -> Tìm theo tên
                products = await _productService.SearchProducts(search);
                ViewData["PageTitle"] = $"Tìm kiếm: {search}";
                ViewData["CurrentFilter"] = search;
            }
            else
            {
                // Mặc định -> Lấy tất cả
                products = await _productService.GetAllProductAsync();
                ViewData["PageTitle"] = "Tất cả sản phẩm";
            }

            return View("Index", products);
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View("Details", product);
        }

        // --- ADMIN ACTIONS (Quản lý sản phẩm) ---
        // Truy cập qua /ProductTriCh/Manage

        public async Task<IActionResult> Manage()
        {
            if (!IsAdmin()) return RedirectToAction("Index", "Home");
            var products = await _productService.GetAllProductAsync();
            
            // Sử dụng View "Manage" (file Manage.cshtml)
            return View("Manage", products);
        }

        public async Task<IActionResult> Create()
        {
            if (!IsAdmin()) return RedirectToAction("Index", "Home");
            // Sử dụng Service để lấy danh sách Category thay vì gọi trực tiếp DBContext
            var categories = await _categoryService.GetAllCategoriesAsync();
            ViewData["CategoryTriChid"] = new SelectList(categories, "CategoryTriChid", "CategoryName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductTriCh product)
        {
            if (!IsAdmin()) return RedirectToAction("Index", "Home");

            if (ModelState.IsValid)
            {
                await _productService.AddProductAsync(product);
                return RedirectToAction(nameof(Manage));
            }
            
            var categories = await _categoryService.GetAllCategoriesAsync();
            ViewData["CategoryTriChid"] = new SelectList(categories, "CategoryTriChid", "CategoryName", product.CategoryTriChid);
            return View(product);
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!IsAdmin()) return RedirectToAction("Index", "Home");

            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            
            var categories = await _categoryService.GetAllCategoriesAsync();
            ViewData["CategoryTriChid"] = new SelectList(categories, "CategoryTriChid", "CategoryName", product.CategoryTriChid);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductTriCh product)
        {
            if (!IsAdmin()) return RedirectToAction("Index", "Home");

            if (id != product.ProductTriChid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _productService.UpdateProductAsync(product);
                return RedirectToAction(nameof(Manage));
            }
            
            var categories = await _categoryService.GetAllCategoriesAsync();
            ViewData["CategoryTriChid"] = new SelectList(categories, "CategoryTriChid", "CategoryName", product.CategoryTriChid);
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!IsAdmin()) return RedirectToAction("Index", "Home");
            await _productService.DeleteProductAsync(id);
            return RedirectToAction(nameof(Manage));
        }
    }
}
