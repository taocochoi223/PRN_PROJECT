using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GlassStore.Entities.TriCH.Models;
using GlassStore.Repositories.TriCH.DBContext;
using GlassStore.Services.TriCH;
using Microsoft.AspNetCore.SignalR;
using GlassStore.Razor.WebAppTriCH.Hubs;
namespace GlassStore.Razor.WebAppTriCH.Pages.ProductTriCh
{
    public class EditModel : PageModel
    {
        private readonly IProductTriCHService _productService;
        private readonly ICategoryTriCHService _categoryService;
        private readonly IHubContext<EyewareHub> _hubContext;

        public EditModel(IProductTriCHService productService, ICategoryTriCHService categoryService, IHubContext<EyewareHub> hubContext)
        {
            _productService = productService;
            _categoryService = categoryService;
            _hubContext = hubContext;
        }

        [BindProperty]
        public GlassStore.Entities.TriCH.Models.ProductTriCh ProductTriCh { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = await _productService.GetProductByIdAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }
            ProductTriCh = product;
            var cate = await _categoryService.GetAllCategoriesAsync();
            ViewData["CategoryTriChid"] = new SelectList(cate, "CategoryTriChid", "CategoryName");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var cate = await _categoryService.GetAllCategoriesAsync();
                ViewData["CategoryTriChid"] = new SelectList(cate, "CategoryTriChid", "CategoryName");
                return Page();
            }
            if (!string.IsNullOrWhiteSpace(ProductTriCh.Sku))
            {
                var exists = await _productService.SkuExistsAsync(ProductTriCh.Sku);
                var current = await _productService.GetProductByIdAsync(ProductTriCh.ProductTriChid);
                if (exists && current != null && !string.Equals(current.Sku, ProductTriCh.Sku, StringComparison.OrdinalIgnoreCase))
                {
                    ModelState.AddModelError("ProductTriCh.Sku", "SKU đã tồn tại. Vui lòng nhập mã khác.");
                    var cate = await _categoryService.GetAllCategoriesAsync();
                    ViewData["CategoryTriChid"] = new SelectList(cate, "CategoryTriChid", "CategoryName");
                    return Page();
                }
            }
            await _productService.UpdateProductAsync(ProductTriCh);            
            var updatedItem = await _productService.GetProductByIdAsync(ProductTriCh.ProductTriChid);
            if (updatedItem != null)
            {
                await _hubContext.Clients.All.SendAsync("ReceiveHubUpdate_productTriCh", new
                {
                    productTriChid = updatedItem.ProductTriChid,
                    productName = updatedItem.ProductName,
                    sku = updatedItem.Sku,
                    brand = updatedItem.Brand,
                    price = updatedItem.Price,
                    description = updatedItem.Description,
                    frameType = updatedItem.FrameType,
                    material = updatedItem.Material,
                    dimensions = updatedItem.Dimensions,
                    stockQuantity = updatedItem.StockQuantity,
                    status = updatedItem.Status,
                    updatedAt = updatedItem.UpdatedAt,
                    categoryName = updatedItem.CategoryTriCh != null ? updatedItem.CategoryTriCh.CategoryName : null
                });
            }

            return RedirectToPage("./Manage");
        }
    }
}
