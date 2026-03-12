using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using GlassStore.Entities.TriCH.Models;
using GlassStore.Repositories.TriCH.DBContext;
using GlassStore.Services.TriCH;
using Microsoft.AspNetCore.SignalR;
using GlassStore.Razor.WebAppTriCH.Hubs;

namespace GlassStore.Razor.WebAppTriCH.Pages.ProductTriCh
{
    public class CreateModel : PageModel
    {
        private readonly IProductTriCHService _productService;
        private readonly ICategoryTriCHService _categoryService;
        private readonly IHubContext<EyewareHub> _hubContext;

        public CreateModel(IProductTriCHService productService, ICategoryTriCHService categoryService, IHubContext<EyewareHub> hubContext)
        {
            _productService = productService;
            _categoryService = categoryService;
            _hubContext = hubContext;
        }

        [BindProperty]
        public GlassStore.Entities.TriCH.Models.ProductTriCh ProductTriCh { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
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
            // check for duplicate SKU before trying to save
            if (!string.IsNullOrWhiteSpace(ProductTriCh.Sku))
            {
                bool exists = await _productService.SkuExistsAsync(ProductTriCh.Sku);
                if (exists)
                {
                    ModelState.AddModelError("ProductTriCh.Sku", "SKU đã tồn tại. Vui lòng nhập mã khác.");
                    var cate = await _categoryService.GetAllCategoriesAsync();
                    ViewData["CategoryTriChid"] = new SelectList(cate, "CategoryTriChid", "CategoryName");
                    return Page();
                }
            }
            await _productService.AddProductAsync(ProductTriCh);
            // Fetch with category name for broadcast
            var newItem = await _productService.GetProductByIdAsync(ProductTriCh.ProductTriChid);
            if (newItem != null)
            {
                await _hubContext.Clients.All.SendAsync("ReceiveHubCreate_productTriCh", new
                {
                    productTriChid = newItem.ProductTriChid,
                    productName = newItem.ProductName,
                    sku = newItem.Sku,
                    brand = newItem.Brand,
                    price = newItem.Price,
                    description = newItem.Description,
                    frameType = newItem.FrameType,
                    material = newItem.Material,
                    dimensions = newItem.Dimensions,
                    stockQuantity = newItem.StockQuantity,
                    status = newItem.Status,
                    createdAt = newItem.CreatedAt,
                    updatedAt = newItem.UpdatedAt,
                    categoryName = newItem.CategoryTriCh != null ? newItem.CategoryTriCh.CategoryName : null
                });
            }
            return RedirectToPage("./Manage");
        }
    }
}