using GlassStore.Entities.TriCH.Models;
using GlassStore.Repositories.TriCH.Basic;
using GlassStore.Repositories.TriCH.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlassStore.Repositories.TriCH
{
    public class UserAccountRepository : GenericRepository<UserAccount>
    {
        public UserAccountRepository()
        {
        }

        public UserAccountRepository (PRN222_EYEWEARSHOPContext context)
        {
            _context = context;
        }

        public async Task<UserAccount?> GetUserAccountAsync(string UserName, string Password)
        {
            var user = await _context.UserAccounts
                .FirstOrDefaultAsync(u => u.Email == UserName && u.Password == Password && u.IsActive == true);
            return user;
        }
    }
}
