namespace gtrackProject.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<Models.dbContext.GtrackDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Models.dbContext.GtrackDbContext context)
        {
            var userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>());
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());

            string[] defineRoles = { "admin", "manager", "cs", "qc", "manu", "install", "customer", "driver", "server" };
            foreach (var role in from roleName in defineRoles where !roleManager.RoleExists(roleName) select new IdentityRole(roleName))
            {
                roleManager.Create(role);
            }

            var adminUser = new IdentityUser
            {
                UserName = "admin"
            };

            var adminresult = userManager.Create(adminUser, "pass1234");
            if (adminresult.Succeeded)
            {
                userManager.AddToRole(adminUser.Id, "admin");
            }
        }
    }
}
