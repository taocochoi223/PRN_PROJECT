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


        public async Task<IActionResult> Index(string search, int page = 1)
        {
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
            if (!IsAdmin()) return RedirectToAction(nameof(Index));
            await LoadCategoriesToViewBag();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [TypeFilter(typeof(Filters.AuthenticationFilter))]
        public async Task<IActionResult> Create(ProductTriCh product)
        {
            if (!IsAdmin()) return RedirectToAction(nameof(Index));

            if (ModelState.IsValid)
            {
                try
                {
                    await _productService.AddProductAsync(product);
                    return RedirectToAction(nameof(Index));
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError("Sku", ex.Message);
                }
            }
            await LoadCategoriesToViewBag(product.CategoryTriChid);
            return View(product);
        }

        [TypeFilter(typeof(Filters.AuthenticationFilter))]
        public async Task<IActionResult> Edit(int id)
        {
            if (!IsAdmin()) return RedirectToAction(nameof(Index));

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
            if (!IsAdmin()) return RedirectToAction(nameof(Index));
            if (id != product.ProductTriChid) return NotFound();

            if (ModelState.IsValid)
            {
                await _productService.UpdateProductAsync(product);
                return RedirectToAction(nameof(Index));
            }
            await LoadCategoriesToViewBag(product.CategoryTriChid);
            return View(product);
        }

        [TypeFilter(typeof(Filters.AuthenticationFilter))]
        public async Task<IActionResult> Delete(int id)
        {
            if (!IsAdmin()) return RedirectToAction(nameof(Index));
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null) return NotFound();
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [TypeFilter(typeof(Filters.AuthenticationFilter))]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!IsAdmin()) return RedirectToAction(nameof(Index));
            await _productService.DeleteProductAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task LoadCategoriesToViewBag(int? selectedId = null)
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            ViewData["CategoryTriChid"] = new SelectList(categories, "CategoryTriChid", "CategoryName", selectedId);
        }
    }
}
