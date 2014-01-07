using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using gtrackProject.Repositories.account.IRepos;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace gtrackProject.Repositories.account
{
    public class RoleAdminRepository : IRoleAdminRepository
    {
        private RoleManager<IdentityRole> RoleManager { get; set; }
        private IdentityDbContext AspContext { get; set; }

        public RoleAdminRepository()
        {
            AspContext = new IdentityDbContext();
            RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());
        }

        public IQueryable<IdentityRole> GetAll()
        {
            return AspContext.Roles;
        }

        public async Task<IdentityRole> Get(string id)
        {
            var role = await RoleManager.FindByIdAsync(id);
            if (role != null) return role;
            throw new KeyNotFoundException("id");
        }

        public async Task<IdentityRole> Add(IdentityRole role)
        {
            var newRole = new IdentityRole(role.Name);
            var roleresult = await RoleManager.CreateAsync(newRole);

            if (roleresult.Succeeded) return newRole;
            throw new DbUpdateException(roleresult.Errors.First());
        }

        public async Task<bool> Update(IdentityRole role)
        {
            if (!RoleManager.RoleExists(role.Name))
            {
                var roleOle = await RoleManager.FindByIdAsync(role.Id);
                if (roleOle == null) throw new KeyNotFoundException("id");
                roleOle.Name = role.Name;
                AspContext.Entry(roleOle).State = EntityState.Modified;
                try
                {
                    await AspContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException exception)
                {
                    throw new DbUpdateConcurrencyException(exception.Message);
                }
            }
            else
            {
                throw new ArgumentException("This name ( " + role.Name + " ) is already taken");
            }

            /*var roleresult = await RoleManager.UpdateAsync(rolePut);
            if (!roleresult.Succeeded)
            {
                ModelState.AddModelError("", roleresult.Errors.First());
                return BadRequest(ModelState);
            }*/
            //UpdateAsync is not work! or I don't know how to use it. Database not update value.

            return true;
        }

        public async Task<bool> Remove(string id)
        {
            var role = await AspContext.Roles.FirstOrDefaultAsync(r => r.Id == id);
            if (role == null) throw new KeyNotFoundException("id");

            AspContext.Roles.Remove(role);
            try
            {
                await AspContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException exception)
            {
                throw new DbUpdateConcurrencyException(exception.Message);
            }
                //If you look at DeleteAsync with a decompiler you'll see it throws a NotImplementedException, and so does not provide the ability to delete a role!

            return true;
        }
    }
}