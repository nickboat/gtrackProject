using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using gtrackProject.Models.dbContext;
using gtrackProject.Models.vehicle;
using gtrackProject.Repositories.vehicle.IRepos;

namespace gtrackProject.Repositories.vehicle
{
    public class LogMoveVehicleRepository : ILogMoveVehicleRepository
    {
        private GtrackDbContext _db { get; set; }

        public LogMoveVehicleRepository()
        {
            _db = new GtrackDbContext();
        }

        public IQueryable<LogMoveVehicle> GetAll()
        {
            return _db.LogMoveVehicles;
        }

        public async Task<LogMoveVehicle> Get(int id)
        {
            return await IdExist(id);
        }

        public async Task<LogMoveVehicle> Add(LogMoveVehicle item)
        {
            var veh = await VehicleExist(item.VehicleId);
            var oldHd = await HdExist(item.HdAtFirst);
            var newHd = await HdExist(item.HdMoveTo);
            var createBy = await EmpExist(item.CreateBy);

            if (veh.HdId != oldHd && veh.IdCar != item.IdCarAtFirst)
                throw new ArgumentException("Invalid HD,IdCar AtFirst", "VehicleAtFirst");

            var newCar =
                await _db.Vehicles.FirstOrDefaultAsync(v => v.HdId == item.HdMoveTo && v.IdCar == item.IdCarMoveTo);
            if (newCar != null)
                throw new ArgumentException("HD,IdCar MoveTo is used", "VehicleMoveTo");
            
            veh.HdId = newHd;
            veh.IdCar = item.IdCarMoveTo;

            var newLogMoveVehicle = new LogMoveVehicle
            {
                CreateBy = createBy,
                CreateDate = DateTime.UtcNow,
                VehicleId = veh.Id,
                HdAtFirst = oldHd,
                IdCarAtFirst = item.IdCarAtFirst,
                HdMoveTo = newHd,
                IdCarMoveTo = item.IdCarMoveTo,
                Comment = item.Comment
            };

            _db.Entry(veh).State = EntityState.Modified;
            newLogMoveVehicle = _db.LogMoveVehicles.Add(newLogMoveVehicle);
            try
            {
                await _db.SaveChangesAsync();
                return newLogMoveVehicle;
            }
            catch (DbUpdateConcurrencyException exception)
            {
                throw new DbUpdateConcurrencyException(exception.Message);
            }
        }

        private async Task<LogMoveVehicle> IdExist(int id)
        {
            var log = await _db.LogMoveVehicles.FirstOrDefaultAsync(l => l.Id == id);
            if (log != null) return log;
            throw new KeyNotFoundException("id");
        }
        private async Task<short> HdExist(short id)
        {
            var hd = await _db.Hds.FirstOrDefaultAsync(h => h.Id == id);
            if (hd != null) return id;
            throw new ArgumentNullException("HDId");
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