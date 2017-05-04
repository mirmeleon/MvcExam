using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SugarFactory.Models.ViewModels.AdminArea
{
  public class UserWithAssignedRoleViewModel
    {
        public string UserEmail { get; set; }  
        public IEnumerable<IdentityRole> Roles { get; set; }
    }
}
