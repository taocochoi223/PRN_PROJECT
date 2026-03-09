using GlassStore.Entities.TriCH.Models;
using GlassStore.Repositories.TriCH;

namespace GlassStore.Services.TriCH
{
    public class UserAccoutService
    {
        private readonly UserAccountRepository _userAccoutRepository;

        public UserAccoutService(UserAccountRepository userAccoutRepository)
        {
            _userAccoutRepository = userAccoutRepository;
        }

        public async Task<UserAccount> GetUserAccountAsync(string UserName, string Password)
        {
            try
            {
                return await _userAccoutRepository.GetUserAccountAsync(UserName, Password);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}