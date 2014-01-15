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
    public class UnCmMeterRepository : IUnCmMeterRepository
    {
        private GtrackDbContext _db { get; set; }

        public UnCmMeterRepository()
        {
            _db = new GtrackDbContext();
        }

        public IQueryable<UnCmMeter> GetAll()
        {
            return _db.UnCmMeters;
        }

        public async Task<UnCmMeter> Get(string id)
        {
            return await IdExist(id);
        }

        public async Task<UnCmMeter> Add(UnCmMeter item)
        {
            var meter = new UnCmMeter
            {
                Name = item.Name,
            };

            if (!string.IsNullOrEmpty(item.MsgEn)) meter.MsgEn = item.MsgEn;
            if (!string.IsNullOrEmpty(item.MsgTh)) meter.MsgEn = item.MsgTh;

            meter = _db.UnCmMeters.Add(meter);
            try
            {
                await _db.SaveChangesAsync();
                return meter;
            }
            catch (DbUpdateConcurrencyException exception)
            {
                throw new DbUpdateConcurrencyException(exception.Message);
            }
        }

        public async Task<bool> Update(UnCmMeter item)
        {
            var meter = await IdExist(item.Id);

            meter.Name = item.Name;
            if (!string.IsNullOrEmpty(item.MsgEn)) meter.MsgEn = item.MsgEn;
            if (!string.IsNullOrEmpty(item.MsgTh)) meter.MsgEn = item.MsgTh;

            _db.Entry(meter).State = EntityState.Modified;
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
            var meter = await IdExist(id);

            _db.UnCmMeters.Remove(meter);
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

        private async Task<UnCmMeter> IdExist(string id)
        {
            var meter = await _db.UnCmMeters.FirstOrDefaultAsync(o => o.Id == id);
            if (meter != null) return meter;
            throw new KeyNotFoundException("id");
        }
    }
}