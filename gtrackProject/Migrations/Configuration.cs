namespace gtrackProject.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<gtrackProject.Models.gtrackDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(gtrackProject.Models.gtrackDbContext context)
        {
            var userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>());
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());

            string[] defineRoles = { "admin", "manager", "cs", "qc", "manu", "install", "customer" };

            foreach (var roleName in defineRoles)
            {
                var role = new IdentityRole(roleName);
                roleManager.Create(role);
            }

            var adminUser = new IdentityUser()
            {
                UserName = "admin"
            };

            var adminresult = userManager.Create(adminUser, "pass1234");
            if (adminresult.Succeeded)
            {
                var result = userManager.AddToRole(adminUser.Id, "admin");
            }
        }
    }
}
