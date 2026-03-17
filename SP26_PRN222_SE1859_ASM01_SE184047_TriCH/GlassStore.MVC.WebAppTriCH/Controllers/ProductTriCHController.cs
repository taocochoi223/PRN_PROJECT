using GlassStore.Entities.TriCH.Models;
using GlassStore.Services.TriCH;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace GlassStore.MVC.WebAppTriCH.Controllers
{
    // Controller quản lý ProductTriCh
    // Kết hợp cả chức năng hiển thị (Public) và quản lý (Admin) để đảm bảo "giống tên bảng"
    public class ProductTriCHController : Controller
    {
        private readonly IProductTriCHService _productService;
        private readonly ICategoryTriCHService _categoryService;

        public ProductTriCHController(IProductTriCHService productService, ICategoryTriCHService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        private bool IsAdmin()
        {
            var roleId = User.FindFirst(ClaimTypes.Role)?.Value;
            return roleId == "1";
        }

        private bool IsManager()
        {
            var roleId = User.FindFirst(ClaimTypes.Role)?.Value;
            return roleId == "2";
        }

        public async Task<IActionResult> Index(string search, int? categoryId, int page = 1)
        {
            int pageSize = 8; 
            int pageIndex = page - 1;
            var allCategories = await _categoryService.GetAllActiveCategoriesAsync();
            ViewData["Categories"] = allCategories;

            List<ProductTriCh> products;
            int totalCount;

            if (categoryId != null)
            {
                products = await _productService.GetProductByCategoryIdAsync(categoryId.Value);
                totalCount = products.Count;
                products = products.Skip(pageIndex * pageSize).Take(pageSize).ToList();
                var currentCat = allCategories.FirstOrDefault(c => c.CategoryTriChid == categoryId);
                ViewData["PageTitle"] = currentCat?.CategoryName;
            }
            else
            {
                var result = await _productService.GetAllProductPagedAsync(pageIndex, pageSize, search);
                products = result.Items;
                totalCount = result.TotalCount;
                ViewData["PageTitle"] = string.IsNullOrEmpty(search) ? "Tất cả sản phẩm" : $"Kết quả tìm kiếm: {search}";
            }

            ViewData["CurrentPage"] = page;
            ViewData["TotalPages"] = (int)Math.Ceiling((double)totalCount / pageSize);
            ViewData["Search"] = search;
            ViewData["CategoryId"] = categoryId;

            return View(products);
        }

        public async Task<IActionResult> Manage(string search, int page = 1)
        {
            // Cả Admin (1) và Manager (2) đều được xem danh sách management
            if (!IsAdmin() && !IsManager()) return RedirectToAction("Index", "Home");

            int pageSize = 5; 
            int pageIndex = page - 1;

            var result = await _productService.GetAllProductPagedAsync(pageIndex, pageSize, search);

            ViewData["CurrentPage"] = page;
            ViewData["TotalPages"] = result.TotalCount;
            ViewData["Search"] = search;

            return View(result.Items);
        }


        public async Task<IActionResult> Details(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null) return NotFound();

            return View(product);
        }


        [TypeFilter(typeof(Filters.AuthenticationFilter))]
        public async Task<IActionResult> Create()
        {
            if (!IsAdmin()) return RedirectToAction(nameof(Manage));
            await LoadCategoriesToViewBag();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [TypeFilter(typeof(Filters.AuthenticationFilter))]
        public async Task<IActionResult> Create(ProductTriCh product)
        {
            if (!IsAdmin()) return RedirectToAction(nameof(Manage));

            if (ModelState.IsValid)
            {
                try
                {
                    await _productService.AddProductAsync(product);
                    return RedirectToAction(nameof(Manage));
                }
                catch (InvalidOperationException ex)
                {
                    // SKU trùng hoặc lỗi logic từ Service → hiện thông báo trên form
                    ModelState.AddModelError("Sku", ex.Message);
                }
            }
            await LoadCategoriesToViewBag(product.CategoryTriChid);
            return View(product);
        }

        [TypeFilter(typeof(Filters.AuthenticationFilter))]
        public async Task<IActionResult> Edit(int id)
        {
            if (!IsAdmin()) return RedirectToAction(nameof(Manage));

            var product = await _productService.GetProductByIdAsync(id);
            if (product == null) return NotFound();
            
            await LoadCategoriesToViewBag(product.CategoryTriChid);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [TypeFilter(typeof(Filters.AuthenticationFilter))]
        public async Task<IActionResult> Edit(int id, ProductTriCh product)
        {
            if (!IsAdmin()) return RedirectToAction(nameof(Manage));
            if (id != product.ProductTriChid) return NotFound();

            if (ModelState.IsValid)
            {
                await _productService.UpdateProductAsync(product);
                return RedirectToAction(nameof(Manage));
            }
            await LoadCategoriesToViewBag(product.CategoryTriChid);
            return View(product);
        }

        [TypeFilter(typeof(Filters.AuthenticationFilter))]
        public async Task<IActionResult> Delete(int id)
        {
            if (!IsAdmin()) return RedirectToAction(nameof(Manage));
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null) return NotFound();
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [TypeFilter(typeof(Filters.AuthenticationFilter))]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!IsAdmin()) return RedirectToAction(nameof(Manage));
            await _productService.DeleteProductAsync(id);
            return RedirectToAction(nameof(Manage));
        }

        private async Task LoadCategoriesToViewBag(int? selectedId = null)
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            ViewData["CategoryTriChid"] = new SelectList(categories, "CategoryTriChid", "CategoryName", selectedId);
        }
    }
}
