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
    public class UnCmCommRepository : IUnCmCommRepository
    {
        private GtrackDbContext _db { get; set; }

        public UnCmCommRepository()
        {
            _db = new GtrackDbContext();
        }

        public IQueryable<UnCmComm> GetAll()
        {
            return _db.UnCmComms;
        }

        public async Task<UnCmComm> Get(string id)
        {
            return await IdExist(id);
        }

        public async Task<UnCmComm> Add(UnCmComm item)
        {
            var comm = new UnCmComm
            {
                Name = item.Name,
            };

            if (!string.IsNullOrEmpty(item.MsgEn)) comm.MsgEn = item.MsgEn;
            if (!string.IsNullOrEmpty(item.MsgTh)) comm.MsgEn = item.MsgTh;

            comm = _db.UnCmComms.Add(comm);
            try
            {
                await _db.SaveChangesAsync();
                return comm;
            }
            catch (DbUpdateConcurrencyException exception)
            {
                throw new DbUpdateConcurrencyException(exception.Message);
            }
        }

        public async Task<bool> Update(UnCmComm item)
        {
            var comm = await IdExist(item.Id);

            comm.Name = item.Name;
            if (!string.IsNullOrEmpty(item.MsgEn)) comm.MsgEn = item.MsgEn;
            if (!string.IsNullOrEmpty(item.MsgTh)) comm.MsgEn = item.MsgTh;

            _db.Entry(comm).State = EntityState.Modified;
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
            var pv = await IdExist(id);

            _db.UnCmComms.Remove(pv);
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

        private async Task<UnCmComm> IdExist(string id)
        {
            var comm = await _db.UnCmComms.FirstOrDefaultAsync(o => o.Id == id);
            if (comm != null) return comm;
            throw new KeyNotFoundException("id");
        }
    }
}