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
    public class VehicleRepository : IVehicleRepository
    {
        private GtrackDbContext _db { get; set; }

        public VehicleRepository()
        {
            _db = new GtrackDbContext();
        }

        public IQueryable<Vehicle> GetAll()
        {
            return _db.Vehicles;
        }

        public async Task<Vehicle> Get(int id)
        {
            return await IdExist(id);
        }

        public async Task<Vehicle> Add(Vehicle item)
        {
            var newVeh = new Vehicle
            {
                IdCar = await IdCarExist(item.IdCar),
                HdId = await HdExist(item.HdId)
            };

            if (item.LicensePlate != null) 
                newVeh.LicensePlate = await LpExist(item.LicensePlate);
            if (item.LicensePlateType != null) 
                newVeh.LicensePlateType = await LpTypeExist(item.LicensePlateType.Value);
            if (item.LicensePlateAt != null) 
                newVeh.LicensePlateAt = await LpAtExist(item.LicensePlateAt.Value);
            if (item.ModelCarId != null) 
                newVeh.ModelCarId = await ModelExist(item.ModelCarId.Value);
            if (item.ColorCarId != null) 
                newVeh.ColorCarId = await ColorExist(item.ColorCarId.Value);
            if (item.OganizeCarId != null) 
                newVeh.OganizeCarId = await OgnExist(item.OganizeCarId.Value);
            if (item.BodyNo != null) 
                newVeh.BodyNo = await BodyNoExist(item.BodyNo);

            newVeh = _db.Vehicles.Add(newVeh);
            try
            {
                await _db.SaveChangesAsync();
                return newVeh;
            }
            catch (DbUpdateConcurrencyException exception)
            {
                throw new DbUpdateConcurrencyException(exception.Message);
            }
        }

        public async Task<bool> Update(Vehicle item)
        {
            var veh = await IdExist(item.Id);

            veh.IdCar = await IdCarExist(item.IdCar);
            veh.HdId = await HdExist(item.HdId);

            if (item.LicensePlate != null)
                veh.LicensePlate = await LpExist(item.LicensePlate);
            if (item.LicensePlateType != null)
                veh.LicensePlateType = await LpTypeExist(item.LicensePlateType.Value);
            if (item.LicensePlateAt != null)
                veh.LicensePlateAt = await LpAtExist(item.LicensePlateAt.Value);
            if (item.ModelCarId != null)
                veh.ModelCarId = await ModelExist(item.ModelCarId.Value);
            if (item.ColorCarId != null)
                veh.ColorCarId = await ColorExist(item.ColorCarId.Value);
            if (item.OganizeCarId != null)
                veh.OganizeCarId = await OgnExist(item.OganizeCarId.Value);
            if (item.BodyNo != null)
                veh.BodyNo = await BodyNoExist(item.BodyNo);

            _db.Entry(veh).State = EntityState.Modified;
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

        public async Task<bool> Remove(int id)
        {
            var veh = await IdExist(id);

            _db.Vehicles.Remove(veh);
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
        
        private async Task<Vehicle> IdExist(int id)
        {
            var veh = await _db.Vehicles.FirstOrDefaultAsync(v => v.Id == id);
            if (veh != null) return veh;
            throw new KeyNotFoundException("id");
        }
        private async Task<string> IdCarExist(string idCar)
        {
            var veh = await _db.Vehicles.FirstOrDefaultAsync(v => v.IdCar == idCar);
            if (veh == null) return idCar;
            throw new ArgumentException("This IdCar ( " + idCar + " ) is already taken");
        }
        private async Task<short> HdExist(short id)
        {
            var header = await _db.Hds.FirstOrDefaultAsync(h => h.Id == id);
            if (header != null) return id;
            throw new ArgumentException("Header Not Found");
        }

        private async Task<string> LpExist(string lp)
        {
            var veh = await _db.Vehicles.FirstOrDefaultAsync(v => v.LicensePlate == lp);
            if (veh == null) return lp;
            throw new ArgumentException("This LicensePlate ( " + lp + " ) is already taken");
        }

        private async Task<byte> LpTypeExist(byte id)
        {
            var type = await _db.LpTypes.FirstOrDefaultAsync(t => t.Id == id);
            if (type != null) return id;
            throw new ArgumentException("LicensePlateType Not Found");
        }
        private async Task<byte> LpAtExist(byte id)
        {
            var province = await _db.Provincess.FirstOrDefaultAsync(p => p.Id == id);
            if (province != null) return id;
            throw new ArgumentException("Province Not Found");
        }
        private async Task<short> ModelExist(short id)
        {
            var model = await _db.VehicleModels.FirstOrDefaultAsync(m => m.Id == id);
            if (model != null) return id;
            throw new ArgumentException("VehicleModels Not Found");
        }
        private async Task<byte> ColorExist(byte id)
        {
            var color = await _db.VehicleColors.FirstOrDefaultAsync(c => c.Id == id);
            if (color != null) return id;
            throw new ArgumentException("VehicleColors Not Found");
        }
        private async Task<byte> OgnExist(byte id)
        {
            var ogn = await _db.VehicleOganizes.FirstOrDefaultAsync(o => o.Id == id);
            if (ogn != null) return id;
            throw new ArgumentException("VehicleOganizes Not Found");
        }
        private async Task<string> BodyNoExist(string bodyNo)
        {
            var veh = await _db.Vehicles.FirstOrDefaultAsync(v => v.BodyNo == bodyNo);
            if (veh == null) return bodyNo;
            throw new ArgumentException("This BodyNo ( " + bodyNo + " ) is already taken");
        }
    }
}