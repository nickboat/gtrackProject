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
    public class VehicleHeadTypeRepository : IVehicleHeadTypeRepository
    {
        private GtrackDbContext _db { get; set; }

        public VehicleHeadTypeRepository()
        {
            _db = new GtrackDbContext();
        }

        public IQueryable<VehicleHeadType> GetAll()
        {
            return _db.VehicleHeadTypes;
        }

        public async Task<VehicleHeadType> Get(byte id)
        {
            return await IdExist(id);
        }

        public async Task<VehicleHeadType> Add(VehicleHeadType item)
        {
            var ht = new VehicleHeadType
            {
                Name = item.Name,
            };

            ht = _db.VehicleHeadTypes.Add(ht);
            try
            {
                await _db.SaveChangesAsync();
                return ht;
            }
            catch (DbUpdateConcurrencyException exception)
            {
                throw new DbUpdateConcurrencyException(exception.Message);
            }
        }

        public async Task<bool> Update(VehicleHeadType item)
        {
            var ht = await IdExist(item.Id);

            ht.Name = item.Name;

            _db.Entry(ht).State = EntityState.Modified;
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
            var ht = await IdExist(id);

            _db.VehicleHeadTypes.Remove(ht);
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

        private async Task<VehicleHeadType> IdExist(byte id)
        {
            var ht = await _db.VehicleHeadTypes.FirstOrDefaultAsync(h => h.Id == id);
            if (ht != null) return ht;
            throw new KeyNotFoundException("id");
        }
    }
}