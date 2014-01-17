using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using gtrackProject.Models.dbContext;
using gtrackProject.Models.driver;
using gtrackProject.Repositories.driver.IRepos;

namespace gtrackProject.Repositories.driver
{
    public class DriverCateRepository : IDriverCateRepository
    {
        private GtrackDbContext _db { get; set; }

        public DriverCateRepository()
        {
            _db = new GtrackDbContext();
        }

        public IQueryable<DriverCategory> GetAll()
        {
            return _db.DriverCategory;
        }

        public async Task<DriverCategory> Get(byte id)
        {
            return await IdExist(id);
        }

        public async Task<DriverCategory> Add(DriverCategory item)
        {
            var cate = new DriverCategory
            {
                Name = item.Name,
                Value = item.Value
            };

            cate = _db.DriverCategory.Add(cate);
            try
            {
                await _db.SaveChangesAsync();
                return cate;
            }
            catch (DbUpdateConcurrencyException exception)
            {
                throw new DbUpdateConcurrencyException(exception.Message);
            }
        }

        public async Task<bool> Update(DriverCategory item)
        {
            var cate = await IdExist(item.Id);
            cate.Name = item.Name;
            cate.Value = item.Value;

            _db.Entry(cate).State = EntityState.Modified;
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
            var cate = await IdExist(id);

            _db.DriverCategory.Remove(cate);
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

        private async Task<DriverCategory> IdExist(byte id)
        {
            var cate = await _db.DriverCategory.FirstOrDefaultAsync(o => o.Id == id);
            if (cate != null) return cate;
            throw new KeyNotFoundException("id");
        }
    }
}