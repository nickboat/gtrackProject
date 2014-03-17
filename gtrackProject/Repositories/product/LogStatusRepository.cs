using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using gtrackProject.Models.dbContext;
using gtrackProject.Models.product;
using gtrackProject.Repositories.product.IRepos;

namespace gtrackProject.Repositories.product
{
    public class LogStatusRepository : ILogStatusRepository
    {
        private GtrackDbContext _db { get; set; }

        public LogStatusRepository()
        {
            _db = new GtrackDbContext();
        }

        public IQueryable<LogStatus> GetAll()
        {
            return _db.LogStatuses;
        }

        public async Task<LogStatus> Get(int id)
        {
            return await IdExist(id);
        }

        public async Task<LogStatus> Add(LogStatus item)
        {
            if (await NameExist(item.Name)) return null;

            var newstatus = new LogStatus
            {
                Name = item.Name
            };

            newstatus = _db.LogStatuses.Add(newstatus);
            try
            {
                await _db.SaveChangesAsync();
                return newstatus;
            }
            catch (DbUpdateConcurrencyException exception)
            {
                throw new DbUpdateConcurrencyException(exception.Message);
            }
        }

        public async Task<bool> Update(LogStatus item)
        {
            var status = await IdExist(item.Id);

            if (await NameExist(item.Name)) return false;

            status.Name = item.Name;

            _db.Entry(status).State = EntityState.Modified;
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
            var status = await IdExist(id);

            _db.LogStatuses.Remove(status);
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

        private async Task<LogStatus> IdExist(int id)
        {
            var status = await _db.LogStatuses.FirstOrDefaultAsync(b => b.Id == id);
            if (status != null) return status;
            throw new KeyNotFoundException("id");
        }

        private async Task<bool> NameExist(string name)
        {
            var checkName = await _db.LogStatuses.FirstOrDefaultAsync(b => b.Name == name);
            if (checkName == null) return false;
            throw new ArgumentException("This name ( " + name + " ) is already taken");
        }
    }
}