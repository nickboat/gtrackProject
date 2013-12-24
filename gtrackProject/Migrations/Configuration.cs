namespace gtrackProject.Migrations
{
    using gtrackProject.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<gtrackDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(gtrackDbContext context)
        {
            var userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>());
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());
            
            string[] defineRoles = {"admin", "manager", "cs", "qc", "manu", "install", "customer"};

            foreach (var roleName in defineRoles)
            {
                var role = new IdentityRole(roleName);
                roleManager.Create(role);
            }

            var adminUser = new IdentityUser()
            {
                UserName = "admin"
            };

            var adminresult = userManager.Create(adminUser, "1234");
            if (adminresult.Succeeded)
            {
                var result = userManager.AddToRole(adminUser.Id, "admin");
            }
        }
    }
}
