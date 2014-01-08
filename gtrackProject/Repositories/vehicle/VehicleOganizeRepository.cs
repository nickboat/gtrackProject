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
    public class VehicleOganizeRepository : IVehicleOganizeRepository
    {
        private GtrackDbContext _db { get; set; }

        public VehicleOganizeRepository()
        {
            _db = new GtrackDbContext();
        }

        public IQueryable<VehicleOganize> GetAll()
        {
            return _db.VehicleOganizes;
        }

        public async Task<VehicleOganize> Get(int id)
        {
            return await IdExist(id);
        }

        public async Task<VehicleOganize> Add(VehicleOganize item)
        {
            var ogn = new VehicleOganize
            {
                Name = item.Name,
            };

            ogn = _db.VehicleOganizes.Add(ogn);
            try
            {
                await _db.SaveChangesAsync();
                return ogn;
            }
            catch (DbUpdateConcurrencyException exception)
            {
                throw new DbUpdateConcurrencyException(exception.Message);
            }
        }

        public async Task<bool> Update(VehicleOganize item)
        {
            var ogn = await IdExist(item.Id);

            ogn.Name = item.Name;

            _db.Entry(ogn).State = EntityState.Modified;
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
            var pv = await IdExist(id);

            _db.VehicleOganizes.Remove(pv);
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

        private async Task<VehicleOganize> IdExist(int id)
        {
            var ogn = await _db.VehicleOganizes.FirstOrDefaultAsync(o => o.Id == id);
            if (ogn != null) return ogn;
            throw new KeyNotFoundException("id");
        }
    }
}