using GlassStore.Entities.TriCH.Models;
using GlassStore.Services.TriCH;
using Microsoft.AspNetCore.Mvc;

namespace GlassStore.MVC.WebAppTriCH.Controllers
{
    // Controller quản lý CategoryTriCh
    // Tên controller giống tên bảng CategoryTriCh theo yêu cầu
    [TypeFilter(typeof(Filters.AuthenticationFilter))]
    public class CategoryTriChController : Controller
    {
        private readonly ICategoryTriCHService _service;

        // Dependency Injection cho Service
        public CategoryTriChController(ICategoryTriCHService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index(string? search, int page = 1)
        {
            int pageSize = 5;
            int pageIndex = page - 1;

            var result = await _service.GetAllCategoriesPagedAsync(pageIndex, pageSize, search);

            ViewData["CurrentPage"] = page;
            ViewData["TotalPages"] = (int)Math.Ceiling((double)result.TotalCount / pageSize);
            ViewData["Search"] = search;

            return View(result.Items);
        }


        public async Task<IActionResult> Details(int id)
        {
            var category = await _service.GetCategoryByIdAsync(id);
            if (category == null) return NotFound();

            return View(category);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryTriCh category)
        {
            if (ModelState.IsValid)
            {
                await _service.AddCategoryAsync(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var category = await _service.GetCategoryByIdAsync(id);
            if (category == null) return NotFound();

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoryTriCh category)
        {
            if (id != category.CategoryTriChid) return NotFound();

            if (ModelState.IsValid)
            {
                await _service.UpdateCategoryAsync(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var category = await _service.GetCategoryByIdAsync(id);
            if (category == null) return NotFound();

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteCategoryAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
