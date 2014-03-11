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
        
        public async Task<bool> UpdateGps(Universe item)
        {
            if (item.GpsProductId == null) throw new ArgumentNullException("GpsProductId", "GpsProductId is Required for this state");

            var un = await IdExist(item.Id);
            un.GpsProductId = await ProductExist(item.GpsProductId.Value);
            if (item.OrderId != null) un.OrderId = await OrderExist(item.OrderId.Value);
            if (item.FixOrderId != null) un.FixOrderId = await FixExist(item.FixOrderId.Value);

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
        public async Task<bool> UpdateData(Universe item)
        {
            if (item.CorrectDataId == null) throw new ArgumentNullException("CorrectDataId", "CorrectDataId is Required for this state");
            if (item.FuelLevel == null) throw new ArgumentNullException("FuelLevel", "FuelLevel is Required for this state");
            if (item.TempLevel == null) throw new ArgumentNullException("TempLevel", "TempLevel is Required for this state");
            if (item.Speed == null) throw new ArgumentNullException("Speed", "Speed is Required for this state");
            if (item.Direction == null) throw new ArgumentNullException("Speed", "Speed is Required for this state");
            if (string.IsNullOrEmpty(item.IpGps)) throw new ArgumentNullException("IpGps", "IpGps is Required for this state");
            if (item.Port == null) throw new ArgumentNullException("Port", "Port is Required for this state");
            if (item.LongGoogle == null) throw new ArgumentNullException("LongGoogle", "LongGoogle is Required for this state");
            if (item.LaGoogle == null) throw new ArgumentNullException("LaGoogle", "LaGoogle is Required for this state");

            var un = await IdExist(item.Id);
            un.CurrentDataDatetime = DateTime.UtcNow;
            un.CorrectDataId = item.CorrectDataId.Value;
            un.CorrectDataDatetime = item.CorrectDataDatetime;

            un.CmCommand = await CommExist(item.CmCommand);
            un.CmEngine = await EngineExist(item.CmEngine);
            un.CmMeter = await MeterExist(item.CmMeter);
            un.CmBatt = await BattExist(item.CmBatt);
            un.FuelLevel = item.FuelLevel.Value;
            un.CmTemp = await TempExist(item.CmTemp);
            un.TempLevel = item.TempLevel.Value;
            un.CmGps = await GpsExist(item.CmGps);
            un.Speed = item.Speed.Value;
            un.Direction = item.Direction.Value;
            un.LongGoogle = item.LongGoogle.Value;
            un.LaGoogle = item.LaGoogle.Value;
            //fag
            un.CmSignalStatus = await SignalExist(item.CmSignalStatus);

            un.IpGps = item.IpGps;
            un.Port = item.Port.Value;

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
        //public async Task<bool> UpdateDataString(string item)
        //{
        //    var data = item.Split(',');
        //    var hd = data[0];
        //    var idcar = data[1];

        //    var un = await _db.Universes.FirstOrDefaultAsync(u => u.Vehicle.Hd.Value == hd && u.Vehicle.IdCar == idcar);

        //    un.CurrentDataDatetime = DateTime.UtcNow;
        //    un.CorrectDataId = item.CorrectDataId.Value;
        //    un.CorrectDataDatetime = item.CorrectDataDatetime;

        //    un.CmCommand = await CommExist(data[2]);
        //    un.CmEngine = await EngineExist(data[4]);
        //    un.CmMeter = await MeterExist(data[3]);
        //    un.CmBatt = await BattExist(data[5]);
        //    try
        //    {
        //        var fuel = Convert.ToDecimal(data[6]);
        //        un.FuelLevel = fuel;
        //    }
        //    catch (Exception exception)
        //    {
        //        throw new Exception(exception.Message);
        //    }

        //    try
        //    {
        //        var tempLvl = Convert.ToByte();
        //        un.TempLevel = tempLvl;
        //    }
        //    catch (Exception)
        //    {
        //        un.CmTemp = await TempExist(item.CmTemp);
        //        throw;
        //    }
            
        //    un.CmTemp = await TempExist(item.CmTemp);
        //    un.TempLevel = item.TempLevel.Value;
        //    un.CmGps = await GpsExist(item.CmGps);
        //    un.Speed = item.Speed.Value;
        //    un.Direction = item.Direction.Value;
        //    un.LongGoogle = item.LongGoogle.Value;
        //    un.LaGoogle = item.LaGoogle.Value;
        //    //fag
        //    un.CmSignalStatus = await SignalExist(item.CmSignalStatus);

        //    un.IpGps = item.IpGps;
        //    un.Port = item.Port.Value;

        //    _db.Entry(un).State = EntityState.Modified;

        //    _db.Entry(un).State = EntityState.Modified;
        //    try
        //    {
        //        await _db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException exception)
        //    {
        //        throw new DbUpdateConcurrencyException(exception.Message);
        //    }

        //    return true;
        //}
        public async Task<bool> GetOffGps(int id)
        {
            var un = await IdExist(id);
            if (un.GpsProductId == null) throw new ArgumentNullException("GpsProductId", "GpsProductId is currently Null");

            var gps = un.ProductGps;
            gps.Sim = null;
            _db.Entry(gps).State = EntityState.Modified;
            try
            {
                await _db.SaveChangesAsync();

                un.GpsProductId = null;
                _db.Entry(un).State = EntityState.Modified;
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