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
    public class ProvinceRepository : IProvinceRepository
    {
        private GtrackDbContext _db { get; set; }

        public ProvinceRepository()
        {
            _db = new GtrackDbContext();
        }

        public IQueryable<Province> GetAll()
        {
            return _db.Provincess;
        }

        public async Task<Province> Get(int id)
        {
            return await IdExist(id);
        }

        public async Task<Province> Add(Province item)
        {
            var pv = new Province
            {
                Name = item.Name,
                ShortName = item.ShortName,
                ShortNameEn = item.ShortNameEn
            };

            pv = _db.Provincess.Add(pv);
            try
            {
                await _db.SaveChangesAsync();
                return pv;
            }
            catch (DbUpdateConcurrencyException exception)
            {
                throw new DbUpdateConcurrencyException(exception.Message);
            }
        }

        public async Task<bool> Update(Province item)
        {
            var pv = await IdExist(item.Id);

            pv.Name = item.Name;
            pv.ShortName = item.ShortName;
            pv.ShortNameEn = item.ShortNameEn;

            _db.Entry(pv).State = EntityState.Modified;
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

            _db.Provincess.Remove(pv);
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

        private async Task<Province> IdExist(int id)
        {
            var cam = await _db.Provincess.FirstOrDefaultAsync(c => c.Id == id);
            if (cam != null) return cam;
            throw new KeyNotFoundException("id");
        }
    }
}