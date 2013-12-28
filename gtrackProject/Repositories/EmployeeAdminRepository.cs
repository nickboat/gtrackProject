using System.IO;
using gtrackProject.Models;
using gtrackProject.Models.account;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace gtrackProject.Repositories
{
    public class EmployeeAdminRepository : IEmployeeAdminRepository
    {

        private UserManager<IdentityUser> UserManager { get; set; }
        private RoleManager<IdentityRole> RoleManager { get; set; }
        private IdentityDbContext AspContext { get; set; }
        private GtrackDbContext _db { get; set; }


        public EmployeeAdminRepository()
        {
            AspContext = new IdentityDbContext();
            _db = new GtrackDbContext();
            UserManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>());
            RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());
        }

        public IEnumerable<EmployeeAdminModel> GetAll()
        {
            var emps = _db.Employees;

            return (from emp in emps
                let userIden = UserManager.FindByIdAsync(emp.AspId)
                let roleIdens = userIden.Result.Roles
                let roleAdmins = roleIdens.Select(roleIden => new RoleAdminModel
                {
                    Id = roleIden.Role.Id,
                    Name = roleIden.Role.Name
                }).ToList()
                select new EmployeeAdminModel
                {
                    Id = emp.Id,
                    UserName = userIden.Result.UserName,
                    FullName = emp.FullName,
                    Phone = emp.Phone,
                    Gender = emp.Gender,
                    BirthDate = emp.BirthDate,
                    EmployeeRoles = roleAdmins
                }).ToList();
        }

        public EmployeeAdminModel Get(int id)
        {
            var emp = _db.Employees.FirstOrDefault(e => e.Id == id);
            if (emp == null)
            {
                return null;
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

            return employeeAdmin;
        }

        public EmployeeAdminModel Add(EmployeeAdminModel item)
        {
            //add to asp.net Identity
            var usrIden = new IdentityUser(item.UserName);
            var usrResult = UserManager.Create(usrIden, item.UserName);//pass is same username **by default**
            if (!usrResult.Succeeded)
            {
                //return BadRequest(ModelState);
            }
            //add user to role
            var postEmpRoles = item.EmployeeRoles;
            var roleAdminModels = postEmpRoles as RoleAdminModel[] ?? postEmpRoles.ToArray();

            if (!roleAdminModels.Any())
            {
                //return BadRequest("User much have a role or more!!!");
            }

            if (roleAdminModels.Any(role => !RoleManager.RoleExists(role.Name)))
            {
                //return BadRequest("Invalid Role!!!");
            }

            if (roleAdminModels.Any(role => role.Name == "admin" || role.Name == "customer"))
            {
                //return BadRequest("This Role Not Allow!!!");
            }

            if (usrIden.UserName != item.UserName)
            {
                //return BadRequest("Change Username Not Allow!!!");
            }

            foreach (var role in roleAdminModels)
            {
                var result = UserManager.AddToRole(usrIden.Id, role.Name);
                if (result.Succeeded) continue;
                //ModelState.AddModelError("", result.Errors.First());
                //BadRequest(ModelState);
            }

            //add to _db.employee
            var newEmp = new Employee
            {
                AspId = usrIden.Id,
                FullName = item.FullName,
                Phone = item.Phone,
                Gender = item.Gender,
                BirthDate = item.BirthDate
            };
            _db.Employees.Add(newEmp);
            _db.SaveChanges();

            return item;
        }

        public bool Remove(int id)
        {
            var emp = _db.Employees.FirstOrDefault(e => e.Id == id);
            if (emp == null)
            {
                return false;
            }

            //remove asp.net identity user
            var usr = AspContext.Users.First(u => u.Id == emp.AspId);
            AspContext.Users.Remove(usr);
            AspContext.SaveChanges();

            _db.Employees.Remove(emp);
            _db.SaveChanges();

            return true;
        }

        public bool Update(EmployeeAdminModel item)
        {
            var usrRoles = item.EmployeeRoles;
            var roleAdminModels = usrRoles as RoleAdminModel[] ?? usrRoles.ToArray();
            var usrIden = UserManager.FindById(item.AspId);

            if (!roleAdminModels.Any())
            {
                //return BadRequest("User much have a role or more!!!");
            }

            if (roleAdminModels.Any(role => !RoleManager.RoleExists(role.Name)))
            {
                //return BadRequest("Invalid Role!!!");
            }

            if (roleAdminModels.Any(role => role.Name == "admin" || role.Name == "customer"))
            {
                //return BadRequest("This Role Not Allow!!!");
            }

            if (usrIden.UserName != item.UserName)
            {
                //return BadRequest("Change Username Not Allow!!!");
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
                var result = UserManager.AddToRole(usrIden.Id, role.Name);
                if (result.Succeeded) continue;
                //ModelState.AddModelError("", result.Errors.First());
                //BadRequest(ModelState);
            }

            //edit employee
            var newEmp = new Employee
            {
                Id = item.Id,
                FullName = item.FullName,
                Phone = item.Phone,
                AspId = usrIden.Id,
                Gender = item.Gender,
                BirthDate = item.BirthDate
            };
            _db.Entry(newEmp).State = EntityState.Modified;
            _db.SaveChanges();

            return true;
        }
    }
}