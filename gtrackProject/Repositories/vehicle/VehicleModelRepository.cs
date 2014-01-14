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
    public class VehicleModelRepository : IVehicleModelRepository
    {
        private GtrackDbContext _db { get; set; }

        public VehicleModelRepository()
        {
            _db = new GtrackDbContext();
        }

        public IQueryable<VehicleModel> GetAll()
        {
            return _db.VehicleModels;
        }

        public async Task<VehicleModel> Get(short id)
        {
            return await IdExist(id);
        }

        public async Task<VehicleModel> Add(VehicleModel item)
        {
            var model = new VehicleModel
            {
                Name = item.Name,
                BrandId = await BrandExist(item.BrandId),
                TypeId = await TypeExist(item.TypeId),
            };

            if (item.Year != null) model.Year = item.Year;

            model = _db.VehicleModels.Add(model);
            try
            {
                await _db.SaveChangesAsync();
                return model;
            }
            catch (DbUpdateConcurrencyException exception)
            {
                throw new DbUpdateConcurrencyException(exception.Message);
            }
        }

        public async Task<bool> Update(VehicleModel item)
        {
            var model = await IdExist(item.Id);

            model.Name = item.Name;
            model.BrandId = await BrandExist(item.BrandId);
            model.TypeId = await TypeExist(item.TypeId);

            if (item.Year != null) model.Year = item.Year;

            _db.Entry(model).State = EntityState.Modified;
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

        public async Task<bool> Remove(short id)
        {
            var model = await IdExist(id);

            _db.VehicleModels.Remove(model);
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

        private async Task<VehicleModel> IdExist(short id)
        {
            var model = await _db.VehicleModels.FirstOrDefaultAsync(o => o.Id == id);
            if (model != null) return model;
            throw new KeyNotFoundException("id");
        }

        private async Task<byte> BrandExist(byte id)
        {
            var brand = await _db.VehicleBrands.FirstOrDefaultAsync(b => b.Id == id);
            if (brand != null) return id;
            throw new KeyNotFoundException("id");
        }

        private async Task<byte> TypeExist(byte id)
        {
            var type = await _db.VehicleTypes.FirstOrDefaultAsync(t => t.Id == id);
            if (type != null) return id;
            throw new KeyNotFoundException("id");
        }
    }
}