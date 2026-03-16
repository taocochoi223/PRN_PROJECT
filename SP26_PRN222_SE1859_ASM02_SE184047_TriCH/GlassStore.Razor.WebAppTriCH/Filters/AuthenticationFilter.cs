using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GlassStore.Razor.WebAppTriCH.Filters
{
    /// <summary>
    /// Filter này dùng để kiểm tra đăng nhập cho Razor Pages.
    /// Nó tương đương với AuthenticationFilter bên MVC mà bạn đã cung cấp.
    /// </summary>
    public class AuthenticationFilter : Attribute, IPageFilter
    {
        public void OnPageHandlerSelected(PageHandlerSelectedContext context)
        {
            // Không cần xử lý ở đây
        }

        public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            // Kiểm tra xem người dùng đã đăng nhập chưa
            if (context.HttpContext.User.Identity == null || !context.HttpContext.User.Identity.IsAuthenticated)
            {
                // Nếu chưa đăng nhập, chuyển hướng sang trang Login (/Account/Login)
                // Trong Razor Pages, chúng ta dùng RedirectToPageResult thay vì RedirectToActionResult
                context.Result = new RedirectToPageResult("/Account/Login");
            }
        }

        public void OnPageHandlerExecuted(PageHandlerExecutedContext context)
        {
            // Không cần xử lý ở đây
        }
    }
}
