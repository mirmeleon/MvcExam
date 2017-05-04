using System.Collections.Generic;

namespace SugarFactory.Models.EntityModels
{
  public class SugarUser
    {
        public SugarUser()
        {
            this.SugarSachets = new HashSet<SugarSachet>();
        }
        public int Id { get; set; }

        public bool IsActivated { get; set; }

       public string ClientPrefix { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<SugarSachet> SugarSachets{ get; set; }

    }
}
