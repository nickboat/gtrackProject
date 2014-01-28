using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using gtrackProject.Models.dbContext;
using gtrackProject.Models.product;
using gtrackProject.Repositories.product.IRepos;

namespace gtrackProject.Repositories.product
{
    public class GpsMemoryRepository : IGpsMemoryRepository
    {
        private GtrackDbContext _db { get; set; }
        public GpsMemoryRepository()
        {
            _db = new GtrackDbContext();
        }

        public IQueryable<ProductGpsMemoryStatus> GetAll()
        {
            return _db.ProductGpsMemorys;
        }

        public async Task<ProductGpsMemoryStatus> Get(byte id)
        {
            return await IdExist(id);
        }

        public async Task<ProductGpsMemoryStatus> Add(ProductGpsMemoryStatus item)
        {
            var newStatus = new ProductGpsMemoryStatus()
            {
                Name = item.Name,
                Val = item.Val
            };

            newStatus = _db.ProductGpsMemorys.Add(newStatus);
            try
            {
                await _db.SaveChangesAsync();
                return newStatus;
            }
            catch (DbUpdateConcurrencyException exception)
            {
                throw new DbUpdateConcurrencyException(exception.Message);
            }
        }

        public async Task<bool> Update(ProductGpsMemoryStatus item)
        {
            var status = await IdExist(item.Id);
            status.Name = item.Name;
            status.Val = item.Val;

            _db.Entry(status).State = EntityState.Modified;
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
            var status = await IdExist(id);

            _db.ProductGpsMemorys.Remove(status);
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

        private async Task<ProductGpsMemoryStatus> IdExist(byte id)
        {
            var status = await _db.ProductGpsMemorys.FirstOrDefaultAsync(t => t.Id == id);
            if (status != null) return status;
            throw new KeyNotFoundException("id");
        }
    }
}