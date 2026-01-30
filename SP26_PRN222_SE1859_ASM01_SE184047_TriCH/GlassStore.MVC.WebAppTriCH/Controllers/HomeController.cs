using GlassStore.Services.TriCH;
using Microsoft.AspNetCore.Mvc;

namespace GlassStore.MVC.WebAppTriCH.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryTriCHService _service;
        public HomeController(ICategoryTriCHService service)
        {
           _service = service;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Index()
        {

           var categories = await _service.GetAllActiveCategoriesAsync();

           return View(categories);
        }

    }
}
