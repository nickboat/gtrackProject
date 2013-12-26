using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Globalization;
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
    public class EmployeeAdminController : ApiController
    {
        public UserManager<IdentityUser> UserManager { get; private set; }
        public RoleManager<IdentityRole> RoleManager { get; private set; }
        public IdentityDbContext AspContext { get; private set; }
        public GtrackDbContext GtrackContext { get; private set; }

        public EmployeeAdminController()
        {
            AspContext = new IdentityDbContext();
            GtrackContext = new GtrackDbContext();
            UserManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>());
            RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());
        }

        // GET api/useradmin
        public IEnumerable<EmployeeAdminModel> GetUsers()
        {
            /*foreach (var emp in emps)
            {
                var userIden = UserManager.FindByIdAsync(emp.AspId);
                var roleIdens = userIden.Result.Roles;
                var roleAdmins = roleIdens.Select(roleIden => new RoleAdminModel
                {
                    Id = roleIden.Role.Id, Name = roleIden.Role.Name
                }).ToList();

                var employee = new EmployeeAdminModel
                {
                    Id = emp.Id,
                    UserName = userIden.Result.UserName,
                    FullName = emp.FullName,
                    Phone = emp.Phone,
                    Gender = emp.Gender,
                    BirthDate = emp.BirthDate,
                    EmployeeRoles = roleAdmins
                };
                employeeAdmins.Add(employee);
            }*/

            var emps = GtrackContext.Employees;

            return (from emp in emps
                let userIden = UserManager.FindByIdAsync(emp.AspId)
                let roleIdens = userIden.Result.Roles
                let roleAdmins = roleIdens.Select(roleIden => new RoleAdminModel
                {
                    Id = roleIden.Role.Id, Name = roleIden.Role.Name
                }).ToList()
                select new EmployeeAdminModel
                {
                    Id = emp.Id, UserName = userIden.Result.UserName, FullName = emp.FullName, Phone = emp.Phone, Gender = emp.Gender, BirthDate = emp.BirthDate, EmployeeRoles = roleAdmins
                }).ToList();
        }

        // GET api/useradmin/(Id)
        [HttpGet]
        [ResponseType(typeof(EmployeeAdminModel))]
        public async Task<IHttpActionResult> GetUser(int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }

            var emp = await GtrackContext.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (emp == null)
            {
                return NotFound();
            }

            var userIden = UserManager.FindByIdAsync(emp.AspId);
            var roleIdens = userIden.Result.Roles;
            var roleAdmins = roleIdens.Select(roleIden => new RoleAdminModel
            {
                Id = roleIden.Role.Id,
                Name = roleIden.Role.Name
            }).ToList();

            var employeeAdmin = new EmployeeAdminModel
            {
                Id = emp.Id,
                UserName = userIden.Result.UserName,
                FullName = emp.FullName,
                Phone = emp.Phone,
                Gender = emp.Gender,
                BirthDate = emp.BirthDate,
                EmployeeRoles = roleAdmins
            };


            return Ok(employeeAdmin);
        }

        // POST api/useradmin
        [HttpPost]
        [ResponseType(typeof(EmployeeAdminModel))]
        public async Task<IHttpActionResult> PostUser(EmployeeAdminModel postEmp)
        {
            if (!ModelState.IsValid)
            {
                /*{
                Id = "?",
                AspId = "?",
                UserName = "?",
                FullName = "?",
                Phone = "?",
                Gender = "?",
                BirthDate = "?",
                "EmployeeRoles":[{"Id":"???","Name":"???"},{"Id":"???","Name":"???"} ...]
                }*/

                return BadRequest(ModelState);
            }

            var postEmpRoles = postEmp.EmployeeRoles;

            var roleAdminModels = postEmpRoles as RoleAdminModel[] ?? postEmpRoles.ToArray();
            if (!roleAdminModels.Any())
            {
                return BadRequest("User much have a role or more!!!");
            }

            if (roleAdminModels.Any(role => !RoleManager.RoleExists(role.Name)))
            {
                return BadRequest("Invalid Role!!!");
            }

            //add to asp.net Identity
            var usrIden = new IdentityUser(postEmp.UserName);
            var usrResult = await UserManager.CreateAsync(usrIden, postEmp.UserName);//pass is same username **by default**
            if (!usrResult.Succeeded)
            {
                ModelState.AddModelError("", usrResult.Errors.First());
                return BadRequest(ModelState);
            }
            //add user to role
            foreach (var role in roleAdminModels)
            {
                var result = await UserManager.AddToRoleAsync(usrIden.Id, role.Name);
                if (result.Succeeded) continue;
                ModelState.AddModelError("", result.Errors.First());
                BadRequest(ModelState);
            }

            //add to _db.employee
            var newEmp = new Employee
            {
                AspId = usrIden.Id,
                FullName = postEmp.FullName,
                Phone = postEmp.Phone,
                Gender = postEmp.Gender,
                BirthDate = postEmp.BirthDate
            };
            GtrackContext.Employees.Add(newEmp);
            await GtrackContext.SaveChangesAsync();
            
            return Ok(postEmp);
        }

        // PUT api/useradmin/(Id)
        [HttpPut]
        public async Task<IHttpActionResult> PutUser(int id, EmployeeAdminModel putEmp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != putEmp.Id)
            {
                return BadRequest();
            }

            var usrRoles = putEmp.EmployeeRoles;

            var roleAdminModels = usrRoles as RoleAdminModel[] ?? usrRoles.ToArray();
            if (!roleAdminModels.Any())
            {
                return BadRequest("User much have a role or more!!!");
            }

            if (roleAdminModels.Any(role => !RoleManager.RoleExists(role.Name)))
            {
                return BadRequest("Invalid Role!!!");
            }

            var usrIden = UserManager.FindById(putEmp.AspId);
            if (usrIden.UserName != putEmp.UserName)
            {
                return BadRequest("Change Username Not Allow!!!");
            }

            //remove all user's role
            var currentRoles = new List<IdentityUserRole>();
            currentRoles.AddRange(usrIden.Roles);
            foreach (var role in currentRoles)
            {
                UserManager.RemoveFromRole(usrIden.Id, role.Role.Name);
            }

            //add new role to user
            foreach (var role in roleAdminModels)
            {
                var result = await UserManager.AddToRoleAsync(usrIden.Id, role.Name);
                if (result.Succeeded) continue;
                ModelState.AddModelError("", result.Errors.First());
                BadRequest(ModelState);
            }

            //edit employee
            var newEmp = new Employee
            {
                Id = putEmp.Id,
                FullName = putEmp.FullName,
                Phone = putEmp.Phone,
                AspId = usrIden.Id,
                Gender = putEmp.Gender,
                BirthDate = putEmp.BirthDate
            };
            GtrackContext.Entry(newEmp).State = EntityState.Modified;
            try
            {
                await GtrackContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                if (!EmployeeAdminExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE api/useradmin/5
        [HttpDelete]
        [ResponseType(typeof(EmployeeAdminModel))]
        public async Task<IHttpActionResult> DeleteRole(int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }

            var emp = GtrackContext.Employees.FirstOrDefaultAsync(e => e.Id == id).Result;
            if (emp == null)
            {
                return NotFound();
            }

            //remove asp.net identity user
            var usr = AspContext.Users.FirstAsync(u => u.Id == emp.AspId).Result;
            AspContext.Users.Remove(usr);
            await AspContext.SaveChangesAsync();

            GtrackContext.Employees.Remove(emp);
            await GtrackContext.SaveChangesAsync();

            return Ok(usr);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                GtrackContext.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeeAdminExists(int id)
        {
            return GtrackContext.Employees.Count(e => e.Id == id) > 0;
        }
    }
}
