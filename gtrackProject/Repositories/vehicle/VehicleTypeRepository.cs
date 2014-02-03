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
    public class VehicleTypeRepository : IVehicleTypeRepository
    {
        private GtrackDbContext _db { get; set; }

        public VehicleTypeRepository()
        {
            _db = new GtrackDbContext();
        }

        public IQueryable<VehicleType> GetAll()
        {
            return _db.VehicleTypes;
        }

        public async Task<VehicleType> Get(byte id)
        {
            return await IdExist(id);
        }

        public async Task<VehicleType> Add(VehicleType item)
        {
            if (item.HeadId == null)
                throw new ArgumentNullException("HeadId");

            var type = new VehicleType
            {
                Name = item.Name,
                HeadId = await HeadExist(item.HeadId.Value)
            };

            type = _db.VehicleTypes.Add(type);
            try
            {
                await _db.SaveChangesAsync();
                return type;
            }
            catch (DbUpdateConcurrencyException exception)
            {
                throw new DbUpdateConcurrencyException(exception.Message);
            }
        }

        public async Task<bool> Update(VehicleType item)
        {
            if (item.HeadId == null)
                throw new ArgumentNullException("HeadId");

            var type = await IdExist(item.Id);

            type.Name = item.Name;
            type.HeadId = await HeadExist(item.HeadId.Value);

            _db.Entry(type).State = EntityState.Modified;
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

        public async Task<bool> Remove(byte id)
        {
            var pv = await IdExist(id);

            _db.VehicleTypes.Remove(pv);
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

        private async Task<VehicleType> IdExist(byte id)
        {
            var type = await _db.VehicleTypes.FirstOrDefaultAsync(o => o.Id == id);
            if (type != null) return type;
            throw new KeyNotFoundException("id");
        }

        private async Task<byte> HeadExist(byte id)
        {
            var type = await _db.VehicleHeadTypes.FirstOrDefaultAsync(h => h.Id == id);
            if (type != null) return id;
            throw new ArgumentException("HeadTypeId");
        }
    }
}