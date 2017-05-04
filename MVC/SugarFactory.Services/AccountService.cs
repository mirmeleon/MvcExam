using System;
using System.Linq;
using System.Web.Mvc;
using SugarFactory.Models.EntityModels;
using SugarFactory.Services.Contracts;

namespace SugarFactory.Services
{
    [HandleError(ExceptionType = typeof(ArgumentNullException), View = "Error")]
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

       public int UsersCount()
       {
           return this.Context.Users.Count();
        }
   }
}
