using SugarFactory.Models.EntityModels;

namespace SugarFactory.Services.Contracts
{
    public interface IAccountService
    {
        void CreateSugarUser(ApplicationUser user);
        int UsersCount();
    }
}