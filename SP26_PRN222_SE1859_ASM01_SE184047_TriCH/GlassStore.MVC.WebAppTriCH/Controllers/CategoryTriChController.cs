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

        // GET: CategoryTriCh/Index
        // Hiển thị danh sách categories
        public async Task<IActionResult> Index()
        {
            var categories = await _service.GetAllCategoriesAsync();
            return View(categories);
        }

        // GET: CategoryTriCh/Details/5
        // Xem chi tiết
        public async Task<IActionResult> Details(int id)
        {
            var category = await _service.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // GET: CategoryTriCh/Create
        // Trang tạo mới
        public IActionResult Create()
        {
            return View();
        }

        // POST: CategoryTriCh/Create
        // Xử lý tạo mới
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

        // GET: CategoryTriCh/Edit/5
        // Trang chỉnh sửa
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _service.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: CategoryTriCh/Edit/5
        // Xử lý chỉnh sửa
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoryTriCh category)
        {
            if (id != category.CategoryTriChid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _service.UpdateCategoryAsync(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: CategoryTriCh/Delete/5
        // Trang xác nhận xóa
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _service.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: CategoryTriCh/Delete/5
        // Xử lý xóa
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteCategoryAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
