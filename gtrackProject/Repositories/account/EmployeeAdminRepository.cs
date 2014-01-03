using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using gtrackProject.Models;
using gtrackProject.Models.account;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace gtrackProject.Repositories.account
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
            var list = new List<EmployeeAdminModel>();
            var emps = _db.Employees;
            
            foreach (var emp in emps)
            {
                var userIden = UserManager.FindById(emp.AspId);
                var roleIdens = userIden.Roles;
                var roleAdmins = new string[roleIdens.Count];
                var i = 0;
                foreach (var result in roleIdens)
                {
                    roleAdmins[i] = result.Role.Name;
                    i++;
                }

                var employeeAdmin = new EmployeeAdminModel
                {
                    Id = emp.Id,
                    UserName = userIden.UserName,
                    FullName = emp.FullName,
                    Phone = emp.Phone,
                    Gender = emp.Gender,
                    BirthDate = emp.BirthDate,
                    Roles = roleAdmins
                };
                list.Add(employeeAdmin);
            }


            return list;
        }

        public async Task<EmployeeAdminModel> Get(int id)
        {
            var emp = await _db.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (emp == null) throw new KeyNotFoundException("id");

            var userIden = await UserManager.FindByIdAsync(emp.AspId);
            var roleIdens = userIden.Roles;
            var roleAdmins = new string[roleIdens.Count];
            var i = 0;
            foreach (var result in roleIdens)
            {
                roleAdmins[i] = result.Role.Name;
                i++;
            }

            var employeeAdmin = new EmployeeAdminModel
            {
                Id = emp.Id,
                UserName = userIden.UserName,
                FullName = emp.FullName,
                Phone = emp.Phone,
                Gender = emp.Gender,
                BirthDate = emp.BirthDate,
                Roles = roleAdmins
            };

            return employeeAdmin;
        }

        public async Task<EmployeeAdminModel> Add(EmployeeAdminModel item)
        {
            //add user to role
            var roleAdminModels = item.Roles;

            if (roleAdminModels.Any(string.IsNullOrEmpty))
            {
                throw new ArgumentNullException("Roles", "Roles cannot be null!!!");
            }

            if (roleAdminModels.Any(role => !RoleManager.RoleExists(role)))
            {
                throw new ArgumentException("Invalid Role!!!");
            }

            if (roleAdminModels.Any(role => role == "admin" || role == "customer"))
            {
                throw new ArgumentException("This Role Not Allow To Use!!!");
            }

            //add to asp.net Identity
            var usrIden = new IdentityUser(item.UserName);
            var usrResult = await UserManager.CreateAsync(usrIden, item.UserName);//pass is same username **by default**
            if (!usrResult.Succeeded)
            {
                throw new DbUpdateException(usrResult.Errors.First());
            }

            foreach (var result in roleAdminModels.Select(role => UserManager.AddToRole(usrIden.Id, role)).Where(result => !result.Succeeded))
            {
                throw new DbUpdateException(result.Errors.First());
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
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                //remove asp.net identity user
                var usr = AspContext.Users.First(u => u.Id == usrIden.Id);
                AspContext.Users.Remove(usr);
                AspContext.SaveChanges();

                throw new DbUpdateConcurrencyException(ex.Message);
            }
            item.Id = newEmp.Id;

            return item;
        }

        public async Task<bool> Remove(int id)
        {
            var emp = await _db.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (emp == null) throw new KeyNotFoundException("id");

            //remove asp.net identity user
            var usr = await AspContext.Users.FirstAsync(u => u.Id == emp.AspId);
            AspContext.Users.Remove(usr);
            
            try
            {
                await AspContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new DbUpdateConcurrencyException(ex.Message);
            }

            _db.Employees.Remove(emp);
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new DbUpdateConcurrencyException(ex.Message);
            }

            return true;
        }

        public async Task<bool> Update(EmployeeAdminModel item)
        {
            var roleAdminModels = item.Roles;
            var emp = await _db.Employees.FirstOrDefaultAsync(e => e.Id == item.Id);
            if (emp == null)
            {
                throw new KeyNotFoundException("id");
            }

            if (roleAdminModels.Any(string.IsNullOrEmpty))
            {
                throw new ArgumentNullException("Roles", "Roles cannot be null!!!");
            }

            if (roleAdminModels.Any(role => !RoleManager.RoleExists(role)))
            {
                throw new ArgumentException("Invalid Role!!!");
            }

            if (roleAdminModels.Any(role => role == "admin" || role == "customer"))
            {
                throw new ArgumentException("This Role Not Allow To Use!!!");
            }

            var usrIden = await UserManager.FindByIdAsync(emp.AspId);
            if (usrIden.UserName != item.UserName)
            {
                throw new ArgumentException("Change Username Not Allow!!!", "UserName");
            }

            //remove all user's role
            var currentRoles = new List<IdentityUserRole>();
            currentRoles.AddRange(usrIden.Roles);
            foreach (var role in currentRoles)
            {
                await UserManager.RemoveFromRoleAsync(usrIden.Id, role.Role.Name);
            }

            //add new role to user
            foreach (var result in roleAdminModels.Select(role => UserManager.AddToRole(usrIden.Id, role)).Where(result => !result.Succeeded))
            {
                throw new DbUpdateException(result.Errors.First());
            }

            //edit employee
            emp.FullName = item.FullName;
            emp.Phone = item.Phone;
            emp.Gender = item.Gender;
            emp.BirthDate = item.BirthDate;
            _db.Entry(emp).State = EntityState.Modified;
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new DbUpdateConcurrencyException(ex.Message);
            }


            return true;
        }
    }
}