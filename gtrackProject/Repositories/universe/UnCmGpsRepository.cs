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
    public class UnCmGpsRepository : IUnCmGpsRepository
    {
        private GtrackDbContext _db { get; set; }

        public UnCmGpsRepository()
        {
            _db = new GtrackDbContext();
        }

        public IQueryable<UnCmGps> GetAll()
        {
            return _db.UnCmGpss;
        }

        public async Task<UnCmGps> Get(string id)
        {
            return await IdExist(id);
        }

        public async Task<UnCmGps> Add(UnCmGps item)
        {
            var gps = new UnCmGps
            {
                Name = item.Name,
            };

            if (!string.IsNullOrEmpty(item.MsgEn)) gps.MsgEn = item.MsgEn;
            if (!string.IsNullOrEmpty(item.MsgTh)) gps.MsgEn = item.MsgTh;

            gps = _db.UnCmGpss.Add(gps);
            try
            {
                await _db.SaveChangesAsync();
                return gps;
            }
            catch (DbUpdateConcurrencyException exception)
            {
                throw new DbUpdateConcurrencyException(exception.Message);
            }
        }

        public async Task<bool> Update(UnCmGps item)
        {
            var gps = await IdExist(item.Id);

            gps.Name = item.Name;
            if (!string.IsNullOrEmpty(item.MsgEn)) gps.MsgEn = item.MsgEn;
            if (!string.IsNullOrEmpty(item.MsgTh)) gps.MsgEn = item.MsgTh;

            _db.Entry(gps).State = EntityState.Modified;
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
            var gps = await IdExist(id);

            _db.UnCmGpss.Remove(gps);
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

        private async Task<UnCmGps> IdExist(string id)
        {
            var gps = await _db.UnCmGpss.FirstOrDefaultAsync(o => o.Id == id);
            if (gps != null) return gps;
            throw new KeyNotFoundException("id");
        }
    }
}