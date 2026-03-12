using GlassStore.Entities.TriCH.Models;
using GlassStore.Services.TriCH;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GlassStore.MVC.WebAppTriCH.Controllers
{
    [TypeFilter(typeof(Filters.AuthenticationFilter))]
    public class ProductImageTriCHController : Controller
    {
        private readonly IProductImageTriCHService _imageService;
        private readonly IProductTriCHService _productService;
        private readonly IProductColorTriCHService _colorService;

        public ProductImageTriCHController(
            IProductImageTriCHService imageService,
            IProductTriCHService productService,
            IProductColorTriCHService colorService)
        {
            _imageService = imageService;
            _productService = productService;
            _colorService = colorService;
        }

        private bool IsAdmin()
        {
            var roleId = User.FindFirst(ClaimTypes.Role)?.Value;
            return roleId == "1";
        }

        // 1. DANH SÁCH hình ảnh theo sản phẩm
        public async Task<IActionResult> Index(int productId)
        {
            if (!IsAdmin()) return RedirectToAction("Index", "Home");
            var images = await _imageService.GetImagesByProductIdAsync(productId);
            ViewBag.ProductId = productId;
            var product = await _productService.GetProductByIdAsync(productId);
            ViewBag.ProductName = product?.ProductName;
            return View(images);
        }

        // 2. THÊM ẢNH MỚI
        public async Task<IActionResult> Create(int productId)
        {
            if (!IsAdmin()) return RedirectToAction("Index", "Home");
            ViewBag.ProductId = productId;
            await LoadColorsToViewBag(productId);
            return View(new ProductImageTriCh { ProductTriChid = productId, IsPrimary = false, DisplayOrder = 0 });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductImageTriCh image)
        {
            if (!IsAdmin()) return RedirectToAction("Index", "Home");

            if (string.IsNullOrWhiteSpace(image.ImageUrl))
                ModelState.AddModelError("ImageUrl", "Link ảnh không được để trống.");

            if (ModelState.IsValid)
            {
                await _imageService.AddImageAsync(image);
                return RedirectToAction(nameof(Index), new { productId = image.ProductTriChid });
            }
            ViewBag.ProductId = image.ProductTriChid;
            await LoadColorsToViewBag(image.ProductTriChid);
            return View(image);
        }

        // 3. SỬA ẢNH
        public async Task<IActionResult> Edit(int id)
        {
            if (!IsAdmin()) return RedirectToAction("Index", "Home");
            var image = await _imageService.GetImageByIdAsync(id);
            if (image == null) return NotFound();
            ViewBag.ProductId = image.ProductTriChid;
            await LoadColorsToViewBag(image.ProductTriChid, image.ColorTriChid);
            return View(image);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductImageTriCh image)
        {
            if (!IsAdmin()) return RedirectToAction("Index", "Home");
            if (id != image.ImageTriChid) return NotFound();

            if (string.IsNullOrWhiteSpace(image.ImageUrl))
                ModelState.AddModelError("ImageUrl", "Link ảnh không được để trống.");

            if (ModelState.IsValid)
            {
                await _imageService.UpdateImageAsync(image);
                return RedirectToAction(nameof(Index), new { productId = image.ProductTriChid });
            }
            ViewBag.ProductId = image.ProductTriChid;
            await LoadColorsToViewBag(image.ProductTriChid, image.ColorTriChid);
            return View(image);
        }

        private async Task LoadColorsToViewBag(int productId, int? selectedId = null)
        {
            var colors = await _colorService.GetColorsByProductIdAsync(productId);
            ViewBag.Colors = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(colors, "ColorTriChid", "ColorName", selectedId);
        }

        // 4. XÓA ẢNH
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int productId)
        {
            if (!IsAdmin()) return RedirectToAction("Index", "Home");
            await _imageService.DeleteImageAsync(id);
            return RedirectToAction(nameof(Index), new { productId });
        }
    }
}
