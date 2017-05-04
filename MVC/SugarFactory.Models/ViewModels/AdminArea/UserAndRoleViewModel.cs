using System.Collections.Generic;

using Microsoft.AspNet.Identity.EntityFramework;

namespace SugarFactory.Models.ViewModels.AdminArea
{
  public class UserAndRoleViewModel
    {
        public string UserId { get; set; }  
        public string Email { get; set; }
   
        public IEnumerable<IdentityRole> Roles { get; set; }
    }
}
