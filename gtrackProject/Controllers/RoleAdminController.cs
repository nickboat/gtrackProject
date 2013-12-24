using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace gtrackProject.Controllers
{
    //[Authorize(Roles = "admin")]
    public class RoleAdminController : ApiController
    {
        public RoleManager<IdentityRole> RoleManager { get; private set; }
        public IdentityDbContext AspContext { get; private set; }


        public RoleAdminController()
        {
            AspContext = new IdentityDbContext();
            RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());
        }

        // GET api/RoleAdmin
        public IQueryable<IdentityRole> GetRoles()
        {
            return AspContext.Roles;
        }

        // GET api/RoleAdmin/(Id)
        [HttpGet]
        [ResponseType(typeof(IdentityRole))]
        public async Task<IHttpActionResult> GetRole(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var role = await RoleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            
            return Ok(role);
        }

        // POST api/RoleAdmin/
        [HttpPost]
        [ResponseType(typeof(IdentityRole))]
        public async Task<IHttpActionResult> PostRole(IdentityRole rolePost)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var role = new IdentityRole(rolePost.Name);
            var roleresult = await RoleManager.CreateAsync(role);

            if (!roleresult.Succeeded)
            {
                ModelState.AddModelError("", roleresult.Errors.First());
                return BadRequest(ModelState);
            }

            return Ok(role);
        }

        // PUT api/RoleAdmin/(Id)
        [HttpPut]
        public async Task<IHttpActionResult> PutRole(string id, IdentityRole rolePut)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rolePut.Id)
            {
                return BadRequest();
            }

            var role = await RoleManager.FindByNameAsync(rolePut.Name);
            if (role == null)
            {
                AspContext.Entry(rolePut).State = EntityState.Modified;
                try
                {
                    await AspContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest("This name ( " + rolePut.Name + " ) is already taken");
            }

            /*var roleresult = await RoleManager.UpdateAsync(rolePut);
            if (!roleresult.Succeeded)
            {
                ModelState.AddModelError("", roleresult.Errors.First());
                return BadRequest(ModelState);
            }*/
            //UpdateAsync is not work! or I don't know how to use it. Database not update value.

            return Ok(rolePut);
        }

        // DELETE api/RoleAdmin/(Id)
        [HttpDelete]
        [ResponseType(typeof(IdentityRole))]
        public async Task<IHttpActionResult> DeleteRole(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var role = await RoleManager.FindByIdAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            AspContext.Roles.Remove(role);
            AspContext.SaveChanges();
            //If you look at DeleteAsync with a decompiler you'll see it throws a NotImplementedException, and so does not provide the ability to delete a role!
            return Ok(role);
        }
    }
}