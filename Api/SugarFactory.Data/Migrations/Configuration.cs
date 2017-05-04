using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SugarFactory.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SugarFactoryContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;

        }

        protected override void Seed(SugarFactoryContext context)
        {
            
           
            if (!context.Roles.Any(role => role.Name == "Admin"))
            {
                RoleStore<IdentityRole> store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                IdentityRole role = new IdentityRole("Admin");
                manager.Create(role);
            }

            if (!context.Roles.Any(role => role.Name == "Manager"))
            {
                RoleStore<IdentityRole> store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                IdentityRole role = new IdentityRole("Manager");
                manager.Create(role);
            }

            if (!context.Roles.Any(role => role.Name == "SugarUser"))
            {
                RoleStore<IdentityRole> store = new RoleStore<IdentityRole>(context);

                var manager = new RoleManager<IdentityRole>(store);
            
                IdentityRole role = new IdentityRole("SugarUser");
                manager.Create(role);
            }


        }
    }
}
