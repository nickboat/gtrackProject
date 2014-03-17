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
    public class LogSimRepository : ILogSimRepository
    {
        private GtrackDbContext _db { get; set; }

        public LogSimRepository()
        {
            _db = new GtrackDbContext();
        }

        public IQueryable<LogSim> GetAll()
        {
            return _db.LogSims;
        }

        public async Task<LogSim> Get(int id)
        {
            return await IdExist(id);
        }

        public async Task<LogSim> Add(LogSim item)
        {
            var gps = await GpsExist(item.GpsId);
            if (gps.SimId != item.SimAtFirst)
                throw new ArgumentException("Invalid SimAtFirst", "SimAtFirst");

            var oldSim = await SimExist(item.SimAtFirst);
            var newSim = await SimExist(item.SimNew);
            var createBy = await EmpExist(item.CreateBy);

            oldSim.Status = 1;

            if (newSim.Status != 2) newSim.Status = 2;
            else throw new ArgumentException("SimNew.Status is currently Active", "SimNew");

            gps.SimId = newSim.Id;

            var newLogSim = new LogSim
            {
                CreateBy = createBy,
                CreateDate = DateTime.UtcNow,
                GpsId = gps.Id,
                SimAtFirst = oldSim.Id,
                SimNew = newSim.Id
            };

            _db.Entry(oldSim).State = EntityState.Modified;
            _db.Entry(newSim).State = EntityState.Modified;
            _db.Entry(gps).State = EntityState.Modified;
            newLogSim = _db.LogSims.Add(newLogSim);
            try
            {
                await _db.SaveChangesAsync();
                return newLogSim;
            }
            catch (DbUpdateConcurrencyException exception)
            {
                throw new DbUpdateConcurrencyException(exception.Message);
            }
        }

        private async Task<LogSim> IdExist(int id)
        {
            var status = await _db.LogSims.FirstOrDefaultAsync(b => b.Id == id);
            if (status != null) return status;
            throw new KeyNotFoundException("id");
        }
        private async Task<Gps> GpsExist(int id)
        {
            var gps = await _db.Gpss.FirstOrDefaultAsync(g => g.Id == id);
            if (gps != null) return gps;
            throw new ArgumentNullException("GpsId");
        }
        private async Task<int> EmpExist(int id)
        {
            var emp = await _db.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (emp != null) return id;
            throw new ArgumentNullException("CreateBy");
        }
        private async Task<Sim> SimExist(int id)
        {
            var sim = await _db.Sims.FirstOrDefaultAsync(s => s.Id == id);
            if (sim != null) return sim;
            throw new ArgumentNullException("Sims");
        }
    }
}