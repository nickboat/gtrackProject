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
    public class SimStatusRepository : ISimStatusRepository
    {
        private GtrackDbContext _db { get; set; }

        public SimStatusRepository()
        {
            _db = new GtrackDbContext();
        }

        public IQueryable<SimStatus> GetAll()
        {
            return _db.SimStatuses;
        }

        public async Task<SimStatus> Get(byte id)
        {
            return await IdExist(id);
        }

        public async Task<SimStatus> Add(SimStatus item)
        {
            if (await NameExist(item.Name)) return null;

            var newstatus = new SimStatus
            {
                Name = item.Name
            };

            newstatus = _db.SimStatuses.Add(newstatus);
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

        public async Task<bool> Update(SimStatus item)
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

        public async Task<bool> Remove(byte id)
        {
            var status = await IdExist(id);

            _db.SimStatuses.Remove(status);
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

        private async Task<SimStatus> IdExist(byte id)
        {
            var status = await _db.SimStatuses.FirstOrDefaultAsync(b => b.Id == id);
            if (status != null) return status;
            throw new KeyNotFoundException("id");
        }

        private async Task<bool> NameExist(string name)
        {
            var checkName = await _db.SimStatuses.FirstOrDefaultAsync(b => b.Name == name);
            if (checkName == null) return false;
            throw new ArgumentException("This name ( " + name + " ) is already taken");
        }
    }
}