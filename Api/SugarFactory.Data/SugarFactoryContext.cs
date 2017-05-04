using Microsoft.AspNet.Identity.EntityFramework;
using SugarFactory.Models.EntityModels;

namespace SugarFactory.Data
{
    using System.Data.Entity;

    public class SugarFactoryContext : IdentityDbContext<ApplicationUser>
    {
        public SugarFactoryContext()
            : base("data source=.;initial catalog=SugarFactoryDb;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework", throwIfV1Schema: false)
        {
        }

        public virtual DbSet<SugarUser> SugarUsers { get; set; }
        public virtual DbSet<SugarSachet> SugarSachets { get; set; }

        public virtual DbSet<Counter> Counters { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<ClientPrefix> ClientPrefixes { get; set; }

        public static SugarFactoryContext Create()
        {
            return new SugarFactoryContext();
        }
       
       
    }

   
}