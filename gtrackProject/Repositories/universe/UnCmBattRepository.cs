using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using gtrackProject.Models.dbContext;
using gtrackProject.Models.universe;
using gtrackProject.Repositories.universe.IRepos;

namespace gtrackProject.Repositories.universe
{
    public class UnCmBattRepository : IUnCmBattRepository
    {
        private GtrackDbContext _db { get; set; }

        public UnCmBattRepository()
        {
            _db = new GtrackDbContext();
        }

        public IQueryable<UnCmBatt> GetAll()
        {
            return _db.UnCmBatts;
        }

        public async Task<UnCmBatt> Get(string id)
        {
            return await IdExist(id);
        }

        public async Task<UnCmBatt> Add(UnCmBatt item)
        {
            var batt = new UnCmBatt
            {
                Name = item.Name,
                Id = await UsedIdName(item.Id)
            };

            if (!string.IsNullOrEmpty(item.MsgEn)) batt.MsgEn = item.MsgEn;
            if (!string.IsNullOrEmpty(item.MsgTh)) batt.MsgTh = item.MsgTh;

            batt = _db.UnCmBatts.Add(batt);
            try
            {
                await _db.SaveChangesAsync();
                return batt;
            }
            catch (DbUpdateConcurrencyException exception)
            {
                throw new DbUpdateConcurrencyException(exception.Message);
            }
        }

        public async Task<bool> Update(UnCmBatt item)
        {
            var batt = await IdExist(item.Id);

            batt.Name = item.Name;
            if (!string.IsNullOrEmpty(item.MsgEn)) batt.MsgEn = item.MsgEn;
            if (!string.IsNullOrEmpty(item.MsgTh)) batt.MsgTh = item.MsgTh;

            _db.Entry(batt).State = EntityState.Modified;
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

        public async Task<bool> Remove(string id)
        {
            var batt = await IdExist(id);

            _db.UnCmBatts.Remove(batt);
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

        private async Task<UnCmBatt> IdExist(string id)
        {
            var batt = await _db.UnCmBatts.FirstOrDefaultAsync(o => o.Id == id);
            if (batt != null) return batt;
            throw new KeyNotFoundException("id");
        }
        private async Task<string> UsedIdName(string id)
        {
            var batt = await _db.UnCmBatts.FirstOrDefaultAsync(o => o.Id == id);
            if (batt == null) return id;
            throw new ArgumentException("( " + id + " ) used", "id");
        }
    }
}