using System.Collections.Generic;
using SugarFactory.Models.EntityModels;

namespace SugarFactory.Models.ViewModels.AdminArea
{
  public class AllUsersWithAllPrefixesVm
    {
        public AllUsersWithAllPrefixesVm()
        {
            this.Prefixes = new HashSet<ClientPrefix>();
            this.UsersWithoutPrefix = new HashSet<SugarUser>();
        }
      
        public IEnumerable<SugarUser> UsersWithoutPrefix { get; set; }
       
        public IEnumerable<ClientPrefix> Prefixes { get; set; }
    }
}
