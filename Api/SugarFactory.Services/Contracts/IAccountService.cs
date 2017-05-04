using SugarFactory.Models.EntityModels;

namespace SugarFactory.Services
{
    public interface IAccountService
    {
        void CreateSugarUser(ApplicationUser user);
    }
}