using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace gtrackProject.Controllers
{
    public class UserAdminController : ApiController
    {
        public UserManager<IdentityUser> UserManager { get; private set; }
        public RoleManager<IdentityRole> RoleManager { get; private set; }
        public IdentityDbContext AspContext { get; private set; }


        public UserAdminController()
        {
            AspContext = new IdentityDbContext();
            UserManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>());
            RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());
        }

        // GET api/useradmin
        public IQueryable<IdentityUser> GetUsers()
        {
            return AspContext.Users;
        }

        // GET api/useradmin/(Id)
        [HttpGet]
        [ResponseType(typeof(IdentityUser))]
        public async Task<IHttpActionResult> GetUser(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var usr = await UserManager.FindByIdAsync(id);
            if (usr == null)
            {
                return NotFound();
            }

            return Ok(usr);
        }

        // POST api/useradmin
        [HttpPost]
        [ResponseType(typeof(IdentityUser))]
        public async Task<IHttpActionResult> PostUser(IdentityUser usr)
        {
            /*if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var usrRoles = usr.Roles;

            if (usrRoles.Count > 0)
            {
                foreach (var role in usrRoles)
                {
                    if (!RoleManager.RoleExists(role.Role.Name))
                    {
                        return BadRequest();
                    }

                    var usrIden = new IdentityUser(usr.UserName);

                    var result = await UserManager.AddToRoleAsync(usrIden.Id, role.RoleId);
                    if (!result.Succeeded)
                    {
                        ModelState.AddModelError("", result.Errors.First());
                        BadRequest(ModelState);
                    }
                }
            }

            var usrresult = await UserManager.CreateAsync(usrIden);

            if (!usrresult.Succeeded)
            {
                ModelState.AddModelError("", usrresult.Errors.First());
                return BadRequest(ModelState);
            }*/

            return Ok(usr);
        }

        // PUT api/useradmin/(Id)?roleId=(role id)
        [HttpPut]
        public async Task<IHttpActionResult> PutUser(string id, IdentityUser usr, string roleId)
        {//todo user with multi role
            /*if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != usr.Id)
            {
                return BadRequest();
            }

            var role = await UserManager.FindByIdAsync(id);
            if (role != null)
            {
                AspContext.Entry(usr).State = EntityState.Modified;
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
                return NotFound();
            }*/

            /*var roleresult = await RoleManager.UpdateAsync(rolePut);
            if (!roleresult.Succeeded)
            {
                ModelState.AddModelError("", roleresult.Errors.First());
                return BadRequest(ModelState);
            }*/
            //UpdateAsync is not work! or I don't know how to use it. Database not update value.

            return Ok(usr);
        }

        // DELETE api/useradmin/5
        [HttpDelete]
        [ResponseType(typeof(IdentityUser))]
        public async Task<IHttpActionResult> DeleteRole(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var usr = await UserManager.FindByIdAsync(id);

            if (usr == null)
            {
                return NotFound();
            }

            AspContext.Users.Remove(usr);
            AspContext.SaveChanges();
            //If you look at DeleteAsync with a decompiler you'll see it throws a NotImplementedException, and so does not provide the ability to delete a role!
            return Ok(usr);
        }
    }
}
