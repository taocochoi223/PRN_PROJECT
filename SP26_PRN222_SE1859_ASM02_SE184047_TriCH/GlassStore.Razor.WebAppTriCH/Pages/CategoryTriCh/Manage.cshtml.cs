using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GlassStore.Entities.TriCH.Models;
using GlassStore.Repositories.TriCH.DBContext;
using GlassStore.Services.TriCH;

namespace GlassStore.Razor.WebAppTriCH.Pages.CategoryTriCh
{
    public class ManageModel : PageModel
    {
        private readonly ICategoryTriCHService _categoryService;

        public ManageModel(ICategoryTriCHService category)
        {
            _categoryService = category;
        }

        public IList<GlassStore.Entities.TriCH.Models.CategoryTriCh> CategoryTriCh { get;set; } = default!;

        public async Task OnGetAsync()
        {
            CategoryTriCh = await _categoryService.GetAllCategoriesAsync();
        }
    }
}
