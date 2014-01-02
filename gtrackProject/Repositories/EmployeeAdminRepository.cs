﻿using System.Configuration;
using System.Data.Entity.Infrastructure;
using System.IO;
using gtrackProject.Models;
using gtrackProject.Models.account;
using Microsoft.Ajax.Utilities;
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

        public EmployeeAdminModel Get(int id)
        {
            var emp = _db.Employees.FirstOrDefault(e => e.Id == id);
            if (emp == null)
            {
                throw new KeyNotFoundException("id");
            }

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

            return employeeAdmin;
        }

        public EmployeeAdminModel Add(EmployeeAdminModel item)
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
            var usrResult = UserManager.Create(usrIden, item.UserName);//pass is same username **by default**
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
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                //remove asp.net identity user
                var usr = AspContext.Users.First(u => u.Id == usrIden.Id);
                AspContext.Users.Remove(usr);
                AspContext.SaveChanges();

                throw new DbUpdateException(ex.Message);
            }
            

            return item;
        }

        public bool Remove(int id)
        {
            var emp = _db.Employees.FirstOrDefault(e => e.Id == id);
            if (emp == null)
            {
                throw new KeyNotFoundException("id");
            }

            //remove asp.net identity user
            var usr = AspContext.Users.First(u => u.Id == emp.AspId);
            AspContext.Users.Remove(usr);
            
            try
            {
                AspContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new DbUpdateException(ex.Message);
            }

            _db.Employees.Remove(emp);
            try
            {
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new DbUpdateException(ex.Message);
            }

            return true;
        }

        public bool Update(EmployeeAdminModel item)
        {
            var roleAdminModels = item.Roles;
            var usrIden = UserManager.FindById(item.AspId);

            if (!roleAdminModels.Any())
            {
                //return BadRequest("User much have a role or more!!!");
                throw new ArgumentNullException("item", "User much have a role or more!!!");
            }

            if (roleAdminModels.Any(role => !RoleManager.RoleExists(role)))
            {
                //return BadRequest("Invalid Role!!!");
                throw new ArgumentException("Invalid Role!!!");
            }

            if (roleAdminModels.Any(role => role == "admin" || role == "customer"))
            {
                //return BadRequest("This Role Not Allow!!!");
                throw new ArgumentException("This Role Not Allow To Use!!!");
            }

            if (usrIden.UserName != item.UserName)
            {
                //return BadRequest("Change Username Not Allow!!!");
                throw new ArgumentException("Change Username Not Allow!!!","item");
            }

            //remove all user's role
            var currentRoles = new List<IdentityUserRole>();
            currentRoles.AddRange(usrIden.Roles);
            foreach (var role in currentRoles)
            {
                UserManager.RemoveFromRole(usrIden.Id, role.Role.Name);
            }

            //add new role to user
            foreach (var result in roleAdminModels.Select(role => UserManager.AddToRole(usrIden.Id, role)).Where(result => !result.Succeeded))
            {
                throw new DbUpdateException(result.Errors.First());
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
            try
            {
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new DbUpdateException(ex.Message);
            }


            return true;
        }
    }
}