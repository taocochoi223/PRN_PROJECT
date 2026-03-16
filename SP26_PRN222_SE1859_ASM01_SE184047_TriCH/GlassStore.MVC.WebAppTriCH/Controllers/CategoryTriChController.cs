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

        public async Task<IActionResult> Create()
        {
            await LoadParentsToViewBag();
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
            await LoadParentsToViewBag(category.ParentId);
            return View(category);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var category = await _service.GetCategoryByIdAsync(id);
            if (category == null) return NotFound();

            await LoadParentsToViewBag(category.ParentId, id);
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
            await LoadParentsToViewBag(category.ParentId, id);
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

        private async Task LoadParentsToViewBag(int? selectedId = null, int? excludeId = null)
        {
            var categories = await _service.GetAllCategoriesAsync();
            // Không cho phép chọn chính nó làm danh mục cha trong trang Edit
            if (excludeId.HasValue)
            {
                categories = categories.Where(c => c.CategoryTriChid != excludeId.Value).ToList();
            }
            ViewData["ParentId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(categories, "CategoryTriChid", "CategoryName", selectedId);
        }
    }
}
