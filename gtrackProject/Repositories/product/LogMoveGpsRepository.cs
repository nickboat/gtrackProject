using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using gtrackProject.Models.dbContext;
using gtrackProject.Models.product;
using gtrackProject.Models.vehicle;
using gtrackProject.Repositories.product.IRepos;

namespace gtrackProject.Repositories.product
{
    public class LogMoveGpsRepository : ILogMoveGpsRepository
    {
        private GtrackDbContext _db { get; set; }

        public LogMoveGpsRepository()
        {
            _db = new GtrackDbContext();
        }

        public IQueryable<LogMoveGps> GetAll()
        {
            return _db.LogMoveGpses;
        }

        public async Task<LogMoveGps> Get(int id)
        {
            return await IdExist(id);
        }

        public async Task<LogMoveGps> Add(LogMoveGps item)
        {
            var gps = await GpsExist(item.GpsId);
            var un = await _db.Universes.FirstOrDefaultAsync(u => u.GpsProductId == item.GpsId);
            if (un == null)
                throw new ArgumentException("Gps not Active", "GpsId");

            if (un.VehicleId != item.VehicleAtFirst)
                throw new ArgumentException("Invalid VehicleAtFirst", "VehicleAtFirst");

            if (un.DisplayStatus.Value == 1)
                throw new ArgumentException("Not Support Ban Product", "GpsId");
            if (un.DisplayStatus.Value == 4)
                throw new ArgumentException("Not Support Expire Product", "GpsId");

            var oldVehicle = await VehicleExist(item.VehicleAtFirst);
            var newVehicle = await VehicleExist(item.VehicleMoveTo);
            var createBy = await EmpExist(item.CreateBy);

            un.DisplayStatus = 2;

            var newLogMoveGps = new LogMoveGps
            {
                CreateBy = createBy,
                CreateDate = DateTime.UtcNow,
                GpsId = gps.Id,
                VehicleAtFirst = oldVehicle.Id,
                VehicleMoveTo = newVehicle.Id,
                Status = 1,
                Comment = item.Comment
            };

            _db.Entry(un).State = EntityState.Modified;
            newLogMoveGps = _db.LogMoveGpses.Add(newLogMoveGps);
            try
            {
                await _db.SaveChangesAsync();
                return newLogMoveGps;
            }
            catch (DbUpdateConcurrencyException exception)
            {
                throw new DbUpdateConcurrencyException(exception.Message);
            }
        }
        public async Task<bool> UpStatus(int id, int status)
        {
            var log = await IdExist(id);
            if (status != 2 || status != 3 || status != 4)
                throw new ArgumentException("Not Support this status", "status");

            log.Status = status;
            _db.Entry(log).State = EntityState.Modified;

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

        private async Task<LogMoveGps> IdExist(int id)
        {
            var status = await _db.LogMoveGpses.FirstOrDefaultAsync(b => b.Id == id);
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
        private async Task<Vehicle> VehicleExist(int id)
        {
            var vehicle = await _db.Vehicles.FirstOrDefaultAsync(v => v.Id == id);
            if (vehicle != null) return vehicle;
            throw new ArgumentNullException("Vehicles");
        }
    }
}