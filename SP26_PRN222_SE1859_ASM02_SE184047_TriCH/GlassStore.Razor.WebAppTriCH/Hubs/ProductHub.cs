using Microsoft.AspNetCore.SignalR;

namespace GlassStore.Razor.WebAppTriCH.Hubs
{
    public class ProductHub : Hub
    {
        // Gọi từ client: connection.invoke("DeleteProduct", id)
        // → broadcast tới tất cả client đang kết nối
        public async Task DeleteProduct(int productId)
        {
            await Clients.All.SendAsync("ProductDeleted", productId);
        }
    }
}
