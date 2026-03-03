using GlassStore.Entities.TriCH.Models;
using GlassStore.Services.TriCH;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace GlassStore.MVC.WebAppTriCH.Controllers
{
    [TypeFilter(typeof(Filters.AuthenticationFilter))]
    public class ProductColorTriCHController : Controller
    {
        private readonly IProductColorTriCHService _colorService;
        private readonly IProductTriCHService _productService;

        public ProductColorTriCHController(
            IProductColorTriCHService colorService,
            IProductTriCHService productService)
        {
            _colorService = colorService;
            _productService = productService;
        }

        private bool IsAdmin()
        {
            var roleId = User.FindFirst(ClaimTypes.Role)?.Value;
            return roleId == "1";
        }

        // 1. DANH SÁCH màu theo sản phẩm
        public async Task<IActionResult> Index(int productId)
        {
            if (!IsAdmin()) return RedirectToAction("Index", "Home");
            var colors = await _colorService.GetColorsByProductIdAsync(productId);
            ViewBag.ProductId = productId;
            return View(colors);
        }

        // 2. TẠO MÀU MỚI
        public IActionResult Create(int productId)
        {
            if (!IsAdmin()) return RedirectToAction("Index", "Home");
            ViewBag.ProductId = productId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductColorTriCh color)
        {
            if (!IsAdmin()) return RedirectToAction("Index", "Home");
            if (ModelState.IsValid)
            {
                await _colorService.AddColorAsync(color);
                return RedirectToAction(nameof(Index), new { productId = color.ProductTriChid });
            }
            ViewBag.ProductId = color.ProductTriChid;
            return View(color);
        }

        // 3. SỬA MÀU
        public async Task<IActionResult> Edit(int id)
        {
            if (!IsAdmin()) return RedirectToAction("Index", "Home");
            var color = await _colorService.GetColorByIdAsync(id);
            if (color == null) return NotFound();
            return View(color);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductColorTriCh color)
        {
            if (!IsAdmin()) return RedirectToAction("Index", "Home");
            if (id != color.ColorTriChid) return NotFound();
            if (ModelState.IsValid)
            {
                await _colorService.UpdateColorAsync(color);
                return RedirectToAction(nameof(Index), new { productId = color.ProductTriChid });
            }
            return View(color);
        }

        // 4. XÓA MÀU
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int productId)
        {
            if (!IsAdmin()) return RedirectToAction("Index", "Home");
            await _colorService.DeleteColorAsync(id);
            return RedirectToAction(nameof(Index), new { productId });
        }
    }
}
