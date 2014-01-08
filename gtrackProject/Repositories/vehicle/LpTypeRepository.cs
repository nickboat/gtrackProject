using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using gtrackProject.Models.dbContext;
using gtrackProject.Models.vehicle;
using gtrackProject.Repositories.vehicle.IRepos;

namespace gtrackProject.Repositories.vehicle
{
    public class LpTypeRepository : ILpTypeRepository
    {
        private GtrackDbContext _db { get; set; }

        public LpTypeRepository()
        {
            _db = new GtrackDbContext();
        }

        public IQueryable<LpType> GetAll()
        {
            return _db.LpTypes;
        }

        public async Task<LpType> Get(byte id)
        {
            return await IdExist(id);
        }

        public async Task<LpType> Add(LpType item)
        {
            var newType = new LpType
            {
                Name = item.Name,
                Meaning = item.Meaning
            };

            newType = _db.LpTypes.Add(newType);
            try
            {
                await _db.SaveChangesAsync();
                return newType;
            }
            catch (DbUpdateConcurrencyException exception)
            {
                throw new DbUpdateConcurrencyException(exception.Message);
            }
        }

        public async Task<bool> Update(LpType item)
        {
            var type =await IdExist(item.Id);

            type.Name = item.Name;
            type.Meaning = item.Meaning;

            _db.Entry(type).State = EntityState.Modified;
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
            var type = await IdExist(id);

            _db.LpTypes.Remove(type);
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

        private async Task<LpType> IdExist(byte id)
        {
            var lp = await _db.LpTypes.FirstOrDefaultAsync(l => l.Id == id);
            if (lp != null) return lp;
            throw new KeyNotFoundException("id");
        }
    }
}