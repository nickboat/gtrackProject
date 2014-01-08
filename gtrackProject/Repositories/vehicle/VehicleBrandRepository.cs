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
    public class VehicleBrandRepository : IVehicleBrandRepository
    {
        private GtrackDbContext _db { get; set; }

        public VehicleBrandRepository()
        {
            _db = new GtrackDbContext();
        }

        public IQueryable<VehicleBrand> GetAll()
        {
            return _db.VehicleBrands;
        }

        public async Task<VehicleBrand> Get(byte id)
        {
            return await IdExist(id);
        }

        public async Task<VehicleBrand> Add(VehicleBrand item)
        {
            var brand = new VehicleBrand
            {
                Name = item.Name,
            };

            brand = _db.VehicleBrands.Add(brand);
            try
            {
                await _db.SaveChangesAsync();
                return brand;
            }
            catch (DbUpdateConcurrencyException exception)
            {
                throw new DbUpdateConcurrencyException(exception.Message);
            }
        }

        public async Task<bool> Update(VehicleBrand item)
        {
            var brand = await IdExist(item.Id);

            brand.Name = item.Name;

            _db.Entry(brand).State = EntityState.Modified;
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

            _db.VehicleBrands.Remove(pv);
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

        private async Task<VehicleBrand> IdExist(byte id)
        {
            var brand = await _db.VehicleBrands.FirstOrDefaultAsync(o => o.Id == id);
            if (brand != null) return brand;
            throw new KeyNotFoundException("id");
        }
    }
}