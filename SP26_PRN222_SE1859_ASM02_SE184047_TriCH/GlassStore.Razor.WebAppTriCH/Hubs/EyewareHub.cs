using Microsoft.AspNetCore.SignalR;
using GlassStore.Entities.TriCH.Models;
using GlassStore.Services.TriCH;

namespace GlassStore.Razor.WebAppTriCH.Hubs
{
    public class EyewareHub : Hub
    {
        private readonly IProductTriCHService _productService;
        private readonly ICategoryTriCHService _categoryService;


        public EyewareHub(
            IProductTriCHService productService,
            ICategoryTriCHService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;

        }
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        #region ProductTriCh Hubs
        public async Task HubDelete_productTriCh(int _id)
        {
            await _productService.DeleteProductAsync(_id);
            await Clients.All.SendAsync("ReceiveHubDelete_productTriCh", _id);
        }

        public async Task HubCreate_productTriCh(ProductTriCh item)
        {
            await _productService.AddProductAsync(item);
            var newItem = await _productService.GetProductByIdAsync(item.ProductTriChid);
            if (newItem != null)
            {
                var broadcastData = new
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
                };
                await Clients.All.SendAsync("ReceiveHubCreate_productTriCh", broadcastData);
            }
        }

        public async Task HubUpdate_productTriCh(ProductTriCh item)
        {
            await _productService.UpdateProductAsync(item);
            var newItem = await _productService.GetProductByIdAsync(item.ProductTriChid);
            if (newItem != null)
            {
                var broadcastData = new
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
                };
                await Clients.All.SendAsync("ReceiveHubUpdate_productTriCh", broadcastData);
            }
        }
        #endregion

        #region CategoryTriCh Hubs
        public async Task HubDelete_categoryTriCh(int _id)
        {
            await _categoryService.DeleteCategoryAsync(_id);
            await Clients.All.SendAsync("ReceiveHubDelete_categoryTriCh", _id);
        }

        public async Task HubCreate_categoryTriCh(CategoryTriCh item)
        {
            await _categoryService.AddCategoryAsync(item);
            var newItem = await _categoryService.GetCategoryByIdAsync(item.CategoryTriChid);
            if (newItem != null)
            {
                var broadcastData = new
                {
                    categoryTriChid = newItem.CategoryTriChid,
                    categoryName = newItem.CategoryName,
                    slug = newItem.Slug,
                    parentId = newItem.ParentId,
                    status = newItem.Status
                };
                await Clients.All.SendAsync("ReceiveHubCreate_categoryTriCh", broadcastData);
            }
        }

        public async Task HubUpdate_categoryTriCh(CategoryTriCh item)
        {
            await _categoryService.UpdateCategoryAsync(item);
            var newItem = await _categoryService.GetCategoryByIdAsync(item.CategoryTriChid);
            if (newItem != null)
            {
                var broadcastData = new
                {
                    categoryTriChid = newItem.CategoryTriChid,
                    categoryName = newItem.CategoryName,
                    slug = newItem.Slug,
                    parentId = newItem.ParentId,
                    status = newItem.Status,
                    parentName = newItem.Parent != null ? newItem.Parent.CategoryName : ""
                };
                await Clients.All.SendAsync("ReceiveHubUpdate_categoryTriCh", broadcastData);
            }
        }
        #endregion


    }
}
