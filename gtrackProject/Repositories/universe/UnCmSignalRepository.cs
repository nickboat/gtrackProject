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
    public class UnCmSignalRepository : IUnCmSignalRepository
    {
        private GtrackDbContext _db { get; set; }

        public UnCmSignalRepository()
        {
            _db = new GtrackDbContext();
        }

        public IQueryable<UnCmSignal> GetAll()
        {
            return _db.UnCmSignals;
        }

        public async Task<UnCmSignal> Get(string id)
        {
            return await IdExist(id);
        }

        public async Task<UnCmSignal> Add(UnCmSignal item)
        {
            var signal = new UnCmSignal
            {
                Name = item.Name,
            };

            if (!string.IsNullOrEmpty(item.MsgEn)) signal.MsgEn = item.MsgEn;
            if (!string.IsNullOrEmpty(item.MsgTh)) signal.MsgEn = item.MsgTh;

            signal = _db.UnCmSignals.Add(signal);
            try
            {
                await _db.SaveChangesAsync();
                return signal;
            }
            catch (DbUpdateConcurrencyException exception)
            {
                throw new DbUpdateConcurrencyException(exception.Message);
            }
        }

        public async Task<bool> Update(UnCmSignal item)
        {
            var signal = await IdExist(item.Id);

            signal.Name = item.Name;
            if (!string.IsNullOrEmpty(item.MsgEn)) signal.MsgEn = item.MsgEn;
            if (!string.IsNullOrEmpty(item.MsgTh)) signal.MsgEn = item.MsgTh;

            _db.Entry(signal).State = EntityState.Modified;
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
            var signal = await IdExist(id);

            _db.UnCmSignals.Remove(signal);
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

        private async Task<UnCmSignal> IdExist(string id)
        {
            var signal = await _db.UnCmSignals.FirstOrDefaultAsync(o => o.Id == id);
            if (signal != null) return signal;
            throw new KeyNotFoundException("id");
        }
    }
}