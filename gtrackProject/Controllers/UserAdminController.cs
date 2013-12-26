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
using gtrackProject.Models;
using gtrackProject.Models.account;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.Validation;

namespace gtrackProject.Controllers
{
    //[Authorize(Roles = "admin")]
    public class UserAdminController : ApiController
    {
        public UserManager<IdentityUser> UserManager { get; private set; }
        public RoleManager<IdentityRole> RoleManager { get; private set; }
        public IdentityDbContext AspContext { get; private set; }
        public GtrackDbContext GtrackContext { get; private set; }

        public UserAdminController()
        {
            AspContext = new IdentityDbContext();
            GtrackContext = new GtrackDbContext();
            UserManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>());
            RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());
        }

        // GET api/useradmin
        public IEnumerable<UserAdminModel> GetUsers()
        {
            var users = AspContext.Users;

            return (from usr in users
                let usrRoles = usr.Roles
                let roleAdmins = usrRoles.Select(role => new RoleAdminModel
                {
                    Id = role.Role.Id, Name = role.Role.Name
                }).ToList()
                select new UserAdminModel
                {
                    Id = usr.Id, Name = usr.UserName, usrRoles = roleAdmins
                }).ToList();
        }

        // GET api/useradmin/(Id)
        [HttpGet]
        [ResponseType(typeof(UserAdminModel))]
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

            var roles = usr.Roles;
            var roleAdmins = roles.Select(role => new RoleAdminModel
            {
                Id = role.Role.Id, Name = role.Role.Name
            }).ToList();

            var usrAdmin = new UserAdminModel
            {
                Id = usr.Id,
                Name = usr.UserName,
                usrRoles = roleAdmins
            };


            return Ok(usrAdmin);
        }

        // POST api/useradmin
        [HttpPost]
        [ResponseType(typeof(UserAdminModel))]
        public async Task<IHttpActionResult> PostUser(UserAdminModel usr)
        {
            if (!ModelState.IsValid)
            {
                //{
                //"Id":"",
                //"Pass":"???",
                //"Name":"???",
                //"usrRoles":[{"Id":"???","Name":"???"}]
                //}

                return BadRequest(ModelState);
            }

            var usrRoles = usr.usrRoles;

            var roleAdminModels = usrRoles as RoleAdminModel[] ?? usrRoles.ToArray();
            if (!roleAdminModels.Any())
            {
                return BadRequest("User much have a role or more!!!");
            }

            if (roleAdminModels.Any(role => !RoleManager.RoleExists(role.Name)))
            {
                return BadRequest("Invalid Role!!!");
            }

            var usrIden = new IdentityUser(usr.Name);
            var usrresult = await UserManager.CreateAsync(usrIden,usr.Pass);
            if (!usrresult.Succeeded)
            {
                ModelState.AddModelError("", usrresult.Errors.First());
                return BadRequest(ModelState);
            }

            foreach (var role in roleAdminModels)
            {
                var result = await UserManager.AddToRoleAsync(usrIden.Id, role.Name);
                if (result.Succeeded) continue;
                ModelState.AddModelError("", result.Errors.First());
                BadRequest(ModelState);
            }

            return Ok(usr);
        }

        // PUT api/useradmin/(Id)
        [HttpPut]
        public async Task<IHttpActionResult> PutUser(string id, UserAdminModel usr)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != usr.Id)
            {
                return BadRequest();
            }

            var usrRoles = usr.usrRoles;

            var roleAdminModels = usrRoles as RoleAdminModel[] ?? usrRoles.ToArray();
            if (!roleAdminModels.Any())
            {
                return BadRequest("User much have a role or more!!!");
            }

            if (roleAdminModels.Any(role => !RoleManager.RoleExists(role.Name)))
            {
                return BadRequest("Invalid Role!!!");
            }

            var usrIden = UserManager.FindById(id);
            if (usrIden.UserName != usr.Name)
            {
                return BadRequest("Change username Not Allow!!!");
            }

            var currentRoles = new List<IdentityUserRole>();
            currentRoles.AddRange(usrIden.Roles);
            foreach (var role in currentRoles)
            {
                UserManager.RemoveFromRole(id, role.Role.Name);
            }

            foreach (var role in roleAdminModels)
            {
                var result = await UserManager.AddToRoleAsync(usrIden.Id, role.Name);
                if (result.Succeeded) continue;
                ModelState.AddModelError("", result.Errors.First());
                BadRequest(ModelState);
            }

            return Ok(usr);
        }

        // DELETE api/useradmin/5
        [HttpDelete]
        [ResponseType(typeof(UserAdminModel))]
        public async Task<IHttpActionResult> DeleteRole(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            //var usr = await UserManager.FindByIdAsync(id);
            var usr = AspContext.Users.First(u => u.Id == id);

            if (usr == null)
            {
                return NotFound();
            }

            AspContext.Users.Remove(usr);
            await AspContext.SaveChangesAsync();
            //If you look at DeleteAsync with a decompiler you'll see it throws a NotImplementedException, and so does not provide the ability to delete a role!

            /*var usrProfile = GtrackContext.user_profile.First(u => u.ASP_Id == id);
            if (usrProfile == null)
            {
                return Ok(usr);
            }

            GtrackContext.user_profile.Remove(usrProfile);
            try
            {
                GtrackContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }*/

            return Ok(usr);
        }
    }
}
