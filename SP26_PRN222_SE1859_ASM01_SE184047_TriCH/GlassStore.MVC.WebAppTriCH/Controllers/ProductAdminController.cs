using GlassStore.Entities.TriCH.Models;
using GlassStore.Services.TriCH;
using GlassStore.Repositories.TriCH.DBContext;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GlassStore.MVC.WebAppTriCH.Controllers
{
    [TypeFilter(typeof(Filters.AuthenticationFilter))]
    public class ProductAdminController : Controller
    {
        private readonly IProductTriCHService _service;
        private readonly PRN222_EYEWEARSHOPContext _context;

        public ProductAdminController(IProductTriCHService service, PRN222_EYEWEARSHOPContext context)
        {
            _service = service;
            _context = context;
        }

        private bool IsAdmin()
        {
            var roleId = User.FindFirst(ClaimTypes.Role)?.Value;
            return roleId == "1";
        }

        public async Task<IActionResult> Index()
        {
            if (!IsAdmin()) return RedirectToAction("Index", "Home");

            var products = await _service.GetAllProductAsync();
            return View(products);
        }

        public IActionResult Create()
        {
            if (!IsAdmin()) return RedirectToAction("Index", "Home");
            ViewData["CategoryTriChid"] = new SelectList(_context.CategoryTriChes, "CategoryTriChid", "CategoryName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductTriCh product)
        {
            if (!IsAdmin()) return RedirectToAction("Index", "Home");

            if (ModelState.IsValid)
            {
                await _service.AddProductAsync(product);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryTriChid"] = new SelectList(_context.CategoryTriChes, "CategoryTriChid", "CategoryName", product.CategoryTriChid);
            return View(product);
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!IsAdmin()) return RedirectToAction("Index", "Home");

            var product = await _service.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryTriChid"] = new SelectList(_context.CategoryTriChes, "CategoryTriChid", "CategoryName", product.CategoryTriChid);
            return View(product);
        }

        // POST: Admin/Edit/5
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
                await _service.UpdateProductAsync(product);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryTriChid"] = new SelectList(_context.CategoryTriChes, "CategoryTriChid", "CategoryName", product.CategoryTriChid);
            return View(product);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!IsAdmin()) return RedirectToAction("Index", "Home");
            
            await _service.DeleteProductAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
