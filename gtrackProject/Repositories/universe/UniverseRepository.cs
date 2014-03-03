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
    public class UniverseRepository : IUniverseRepository
    {
        private GtrackDbContext _db { get; set; }

        public UniverseRepository()
        {
            _db = new GtrackDbContext();
        }

        public IQueryable<Universe> GetAll()
        {
            return _db.Universes;
        }

        public async Task<Universe> Get(int id)
        {
            return await IdExist(id);
        }

        public async Task<Universe> Add(Universe item)
        {
            var newUn = new Universe
            {
                VehicleId = await VehicleExist(item.VehicleId),
                DisplayStatus = await DisplayExist(item.DisplayStatus)
            };

            if (item.GpsProductId != null) 
                newUn.GpsProductId = await ProductExist(item.GpsProductId.Value);
            if (item.CurrentDataDatetime != null)
                newUn.CurrentDataDatetime = item.CurrentDataDatetime.Value;
            if (item.CorrectDataDatetime != null)
                newUn.CorrectDataDatetime = item.CorrectDataDatetime.Value;
            if (item.CorrectDataId != null)
                newUn.CorrectDataId = item.CorrectDataId.Value;
            if (item.CmCommand != null) 
                newUn.CmCommand = await CommExist(item.CmCommand);
            if (item.CmEngine != null)
                newUn.CmEngine = await EngineExist(item.CmEngine);
            if (item.CmMeter != null)
                newUn.CmMeter = await MeterExist(item.CmMeter);
            if (item.CmBatt != null)
                newUn.CmBatt = await BattExist(item.CmBatt);
            if (item.CmTemp != null)
                newUn.CmTemp = await TempExist(item.CmTemp);
            if (item.CmGps != null)
                newUn.CmGps = await GpsExist(item.CmGps);
            if (item.CmSignalStatus != null)
                newUn.CmSignalStatus = await SignalExist(item.CmSignalStatus);
            if (item.FuelLevel != null)
                newUn.FuelLevel = item.FuelLevel.Value;
            if (item.TempLevel != null)
                newUn.TempLevel = item.TempLevel.Value;
            if (item.Speed != null)
                newUn.Speed = item.Speed.Value;
            if (item.Direction != null)
                newUn.Direction = item.Direction.Value;
            if (item.IpGps != null)
                newUn.IpGps = item.IpGps;
            if (item.Port != null)
                newUn.Port = item.Port.Value;
            if (item.LaGoogle != null)
                newUn.LaGoogle = item.LaGoogle.Value;
            if (item.LongGoogle != null)
                newUn.LongGoogle = item.LongGoogle.Value;
            if (item.DriverId != null) 
                newUn.DriverId = await DriverExist(item.DriverId.Value);
            if (item.OrderId != null) 
                newUn.OrderId = await OrderExist(item.OrderId.Value);
            if (item.FixOrderId != null) 
                newUn.FixOrderId = await FixExist(item.FixOrderId.Value);

            newUn = _db.Universes.Add(newUn);
            try
            {
                await _db.SaveChangesAsync();
                return newUn;
            }
            catch (DbUpdateConcurrencyException exception)
            {
                throw new DbUpdateConcurrencyException(exception.Message);
            }
        }

        public async Task<bool> Update(Universe item)
        {
            var un = await IdExist(item.Id);
            un.VehicleId = await VehicleExist(item.VehicleId);
            un.DisplayStatus = await DisplayExist(item.DisplayStatus);

            if (item.GpsProductId != null)
                un.GpsProductId = await ProductExist(item.GpsProductId.Value);
            if (item.CurrentDataDatetime != null)
                un.CurrentDataDatetime = item.CurrentDataDatetime.Value;
            if (item.CorrectDataDatetime != null)
                un.CorrectDataDatetime = item.CorrectDataDatetime.Value;
            if (item.CorrectDataId != null)
                un.CorrectDataId = item.CorrectDataId.Value;
            if (item.CmCommand != null)
                un.CmCommand = await CommExist(item.CmCommand);
            if (item.CmEngine != null)
                un.CmEngine = await EngineExist(item.CmEngine);
            if (item.CmMeter != null)
                un.CmMeter = await MeterExist(item.CmMeter);
            if (item.CmBatt != null)
                un.CmBatt = await BattExist(item.CmBatt);
            if (item.CmTemp != null)
                un.CmTemp = await TempExist(item.CmTemp);
            if (item.CmGps != null)
                un.CmGps = await GpsExist(item.CmGps);
            if (item.CmSignalStatus != null)
                un.CmSignalStatus = await SignalExist(item.CmSignalStatus);
            if (item.FuelLevel != null)
                un.FuelLevel = item.FuelLevel.Value;
            if (item.TempLevel != null)
                un.TempLevel = item.TempLevel.Value;
            if (item.Speed != null)
                un.Speed = item.Speed.Value;
            if (item.Direction != null)
                un.Direction = item.Direction.Value;
            if (item.IpGps != null)
                un.IpGps = item.IpGps;
            if (item.Port != null)
                un.Port = item.Port.Value;
            if (item.LaGoogle != null)
                un.LaGoogle = item.LaGoogle.Value;
            if (item.LongGoogle != null)
                un.LongGoogle = item.LongGoogle.Value;
            if (item.DriverId != null)
                un.DriverId = await DriverExist(item.DriverId.Value);
            if (item.OrderId != null)
                un.OrderId = await OrderExist(item.OrderId.Value);
            if (item.FixOrderId != null)
                un.FixOrderId = await FixExist(item.FixOrderId.Value);

            _db.Entry(un).State = EntityState.Modified;
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
            var un = await IdExist(id);

            _db.Universes.Remove(un);
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
        
        private async Task<Universe> IdExist(int id)
        {
            var un = await _db.Universes.FirstOrDefaultAsync(v => v.Id == id);
            if (un != null) return un;
            throw new KeyNotFoundException("id");
        }
        private async Task<int> VehicleExist(int id)
        {
            var veh = await _db.Vehicles.FirstOrDefaultAsync(v => v.Id == id);
            if (veh != null) return id;
            throw new ArgumentException("VehicleId Not Found");
        }
        private async Task<int> ProductExist(int id)
        {
            var product = await _db.Gpss.FirstOrDefaultAsync(p => p.Id == id);
            if (product != null) return id;
            throw new ArgumentException("ProductId Not Found");
        }

        private async Task<string> CommExist(string fag)
        {
            var comm = await _db.UnCmComms.FirstOrDefaultAsync(c => c.Id == fag);
            if (comm != null) return fag;
            throw new ArgumentException("CmCommand Not Found");
        }
        private async Task<string> EngineExist(string fag)
        {
            var engine = await _db.UnCmEngines.FirstOrDefaultAsync(e => e.Id == fag);
            if (engine != null) return fag;
            throw new ArgumentException("CmEngine Not Found");
        }
        private async Task<string> MeterExist(string fag)
        {
            var meter = await _db.UnCmMeters.FirstOrDefaultAsync(m => m.Id == fag);
            if (meter != null) return fag;
            throw new ArgumentException("CmMeter Not Found");
        }
        private async Task<string> BattExist(string fag)
        {
            var batt = await _db.UnCmBatts.FirstOrDefaultAsync(b => b.Id == fag);
            if (batt != null) return fag;
            throw new ArgumentException("CmBatt Not Found");
        }
        private async Task<string> TempExist(string fag)
        {
            var temp = await _db.UnCmTemps.FirstOrDefaultAsync(t => t.Id == fag);
            if (temp != null) return fag;
            throw new ArgumentException("CmTemp Not Found");
        }
        private async Task<string> GpsExist(string fag)
        {
            var gps = await _db.UnCmGpss.FirstOrDefaultAsync(g => g.Id == fag);
            if (gps != null) return fag;
            throw new ArgumentException("CmGps Not Found");
        }private async Task<string> SignalExist(string fag)
        {
            var signal = await _db.UnCmSignals.FirstOrDefaultAsync(s => s.Id == fag);
            if (signal != null) return fag;
            throw new ArgumentException("CmSignalStatus Not Found");
        }
        private async Task<byte> DisplayExist(byte id)
        {
            var dis = await _db.UnDisplayStatuss.FirstOrDefaultAsync(d => d.Id == id);
            if (dis != null) return id;
            throw new ArgumentException("DisplayStatus Not Found");
        }
        private async Task<int> DriverExist(int id)
        {
            var driver = await _db.Drivers.FirstOrDefaultAsync(d => d.Id == id);
            if (driver != null) return id;
            throw new ArgumentException("DriverId Not Found");
        }
        private async Task<int> OrderExist(int id)
        {
            var order = await _db.Orders.FirstOrDefaultAsync(o => o.Id == id);
            if (order != null) return id;
            throw new ArgumentException("OrderId Not Found");
        }
        private async Task<int> FixExist(int id)
        {
            var fix = await _db.FixOrders.FirstOrDefaultAsync(f => f.Id == id);
            if (fix != null) return id;
            throw new ArgumentException("FixOrderId Not Found");
        }
    }
}