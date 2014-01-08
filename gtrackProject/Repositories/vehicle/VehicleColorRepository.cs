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
    public class VehicleColorRepository : IVehicleColorRepository
    {
        private GtrackDbContext _db { get; set; }

        public VehicleColorRepository()
        {
            _db = new GtrackDbContext();
        }

        public IQueryable<VehicleColor> GetAll()
        {
            return _db.VehicleColors;
        }

        public async Task<VehicleColor> Get(byte id)
        {
            return await IdExist(id);
        }

        public async Task<VehicleColor> Add(VehicleColor item)
        {
            var newColor = new VehicleColor
            {
                Name = item.Name
            };

            newColor = _db.VehicleColors.Add(newColor);
            try
            {
                await _db.SaveChangesAsync();
                return newColor;
            }
            catch (DbUpdateConcurrencyException exception)
            {
                throw new DbUpdateConcurrencyException(exception.Message);
            }
        }

        public async Task<bool> Update(VehicleColor item)
        {
            var color = await IdExist(item.Id);

            color.Name = item.Name;

            _db.Entry(color).State = EntityState.Modified;
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
            var color = await IdExist(id);

            _db.VehicleColors.Remove(color);
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

        private async Task<VehicleColor> IdExist(byte id)
        {
            var color = await _db.VehicleColors.FirstOrDefaultAsync(c => c.Id == id);
            if (color != null) return color;
            throw new KeyNotFoundException("id");
        }
    }
}