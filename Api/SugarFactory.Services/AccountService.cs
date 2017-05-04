using SugarFactory.Models.EntityModels;

namespace SugarFactory.Services
{
    public class AccountService : Service, IAccountService
    {
        public void CreateSugarUser(ApplicationUser user)
        {
            SugarUser sugarUser = new SugarUser();
            ApplicationUser appUser = this.Context.Users.Find(user.Id);
            sugarUser.User = appUser;
            this.Context.SugarUsers.Add(sugarUser);
            this.Context.SaveChanges();
        }
    }
}
