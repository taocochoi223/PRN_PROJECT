using GlassStore.Entities.TriCH.Models;
using GlassStore.Repositories.TriCH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlassStore.Services.TriCH
{
    public class CategoryTriCHService : ICategoryTriCHService
    {
        private readonly CategoryTriCHRepository _repo;
        public CategoryTriCHService(CategoryTriCHRepository repo)
        {
            _repo = repo;
        }
        public async Task<List<CategoryTriCh>> GetAllActiveCategoriesAsync()
        {
            return await _repo.GetAllActiveCategoriesAsync();
        }

        public async Task<List<CategoryTriCh>> GetAllCategoriesAsync()
        {
            return await _repo.GetAllCategoriesAsync();
        }
    }
}
