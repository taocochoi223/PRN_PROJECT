using Microsoft.AspNetCore.SignalR;
using GlassStore.Entities.TriCH.Models;
using GlassStore.Services.TriCH;

namespace GlassStore.Razor.WebAppTriCH.Hubs
{
    public class EyewareHub : Hub
    {
        private readonly IProductTriCHService _productService;
        private readonly ICategoryTriCHService _categoryService;
        private readonly IProductColorTriCHService _colorService;
        private readonly IProductImageTriCHService _imageService;

        public EyewareHub(
            IProductTriCHService productService,
            ICategoryTriCHService categoryService,
            IProductColorTriCHService colorService,
            IProductImageTriCHService imageService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _colorService = colorService;
            _imageService = imageService;
        }

        // Bổ sung thêm SendMessage để Chat.js không bị lỗi khi người dùng click
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
        #endregion

        #region ProductColorTriCh Hubs
        public async Task HubDelete_colorTriCh(int _id)
        {
            await _colorService.DeleteColorAsync(_id);
            await Clients.All.SendAsync("ReceiveHubDelete_colorTriCh", _id);
        }

        public async Task HubCreate_colorTriCh(ProductColorTriCh item)
        {
            await _colorService.AddColorAsync(item);
            var newItem = await _colorService.GetColorByIdAsync(item.ColorTriChid);
            if (newItem != null)
            {
                var broadcastData = new
                {
                    colorTriChid = newItem.ColorTriChid,
                    productTriChid = newItem.ProductTriChid,
                    colorName = newItem.ColorName,
                    colorCode = newItem.ColorCode,
                    stockQuantity = newItem.StockQuantity,
                    productName = newItem.ProductTriCh != null ? newItem.ProductTriCh.ProductName : null
                };
                await Clients.All.SendAsync("ReceiveHubCreate_colorTriCh", broadcastData);
            }
        }
        #endregion

        #region ProductImageTriCh Hubs
        public async Task HubDelete_imageTriCh(int _id)
        {
            await _imageService.DeleteImageAsync(_id);
            await Clients.All.SendAsync("ReceiveHubDelete_imageTriCh", _id);
        }

        public async Task HubCreate_imageTriCh(ProductImageTriCh item)
        {
            await _imageService.AddImageAsync(item);
            var newItem = await _imageService.GetImageByIdAsync(item.ImageTriChid);
            if (newItem != null)
            {
                var broadcastData = new
                {
                    imageTriChid = newItem.ImageTriChid,
                    productTriChid = newItem.ProductTriChid,
                    imageUrl = newItem.ImageUrl,
                    isPrimary = newItem.IsPrimary,
                    displayOrder = newItem.DisplayOrder,
                    colorTriChid = newItem.ColorTriChid,
                    productName = newItem.ProductTriCh != null ? newItem.ProductTriCh.ProductName : null,
                    colorName = newItem.ColorTriCh != null ? newItem.ColorTriCh.ColorName : null
                };
                await Clients.All.SendAsync("ReceiveHubCreate_imageTriCh", broadcastData);
            }
        }
        #endregion
    }
}
