using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using gtrackProject.Models.dbContext;
using gtrackProject.Models.driver;
using gtrackProject.Repositories.driver.IRepos;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace gtrackProject.Repositories.driver
{
    public class DriverRepository : IDriverRepository
    {
        private GtrackDbContext _db { get; set; }
        private UserManager<IdentityUser> UserManager { get; set; }
        private IdentityDbContext AspContext { get; set; }
        public DriverRepository()
        {
            _db = new GtrackDbContext();
            AspContext = new IdentityDbContext();
            UserManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>());
        }

        public IQueryable<Driver> GetAll()
        {
            return _db.Drivers;
        }

        public async Task<Driver> Get(int id)
        {
            return await IdExist(id);
        }

        public async Task<Driver> Add(Driver item)
        {
            var driver = new Driver
            {
                IdCard = item.IdCard,
                FirstName = item.FirstName,
                LastNmae = item.LastNmae,
                BirthDate = item.BirthDate,
                Gender = item.Gender,
                DriverIdCard = item.DriverIdCard,
                ZipCode = item.ZipCode,
                CategoryId = await CateExist(item.CategoryId)
            };

            if (item.ExpireCard != null) driver.ExpireCard = item.ExpireCard.Value;
            if (item.TitleName != null) driver.TitleName = item.TitleName;
            if (item.AspId != null) driver.AspId = await AspExist(item.AspId);

            driver = _db.Drivers.Add(driver);
            try
            {
                await _db.SaveChangesAsync();
                return driver;
            }
            catch (DbUpdateConcurrencyException exception)
            {
                throw new DbUpdateConcurrencyException(exception.Message);
            }
        }

        public async Task<bool> Update(Driver item)
        {
            var driver = await IdExist(item.Id);
            driver.IdCard = item.IdCard;
            driver.FirstName = item.FirstName;
            driver.LastNmae = item.LastNmae;
            driver.BirthDate = item.BirthDate;
            driver.Gender = item.Gender;
            driver.DriverIdCard = item.DriverIdCard;
            driver.ZipCode = item.ZipCode;
            driver.CategoryId = await CateExist(item.CategoryId);

            if (item.ExpireCard != null) driver.ExpireCard = item.ExpireCard.Value;
            if (item.TitleName != null) driver.TitleName = item.TitleName;
            if (item.AspId != null) driver.AspId = await AspExist(item.AspId);

            _db.Entry(driver).State = EntityState.Modified;
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException exception)
            {
                throw new DbUpdateConcurrencyException(exception.Message);
            }

            return true;
        }

        public async Task<bool> Remove(int id)
        {
            var driver = await IdExist(id);
            var usr = await AspContext.Users.FirstAsync(u => u.Id == driver.AspId);
            AspContext.Users.Remove(usr);

            try
            {
                await AspContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new DbUpdateConcurrencyException(ex.Message);
            }

            _db.Drivers.Remove(driver);
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException exception)
            {
                throw new DbUpdateConcurrencyException(exception.Message);
            }

            return true;
        }

        private async Task<Driver> IdExist(int id)
        {
            var driver = await _db.Drivers.FirstOrDefaultAsync(o => o.Id == id);
            if (driver != null) return driver;
            throw new KeyNotFoundException("id");
        }
        private async Task<byte> CateExist(byte id)
        {
            var cate = await _db.DriverCategory.FirstOrDefaultAsync(o => o.Id == id);
            if (cate != null) return id;
            throw new ArgumentException("Employee Not Found");
        }
        private async Task<string> AspExist(string id)
        {
            var userIden = await UserManager.FindByIdAsync(id);
            if (userIden != null) return id;
            throw new ArgumentException("UserId Not Found");
        }
    }
}