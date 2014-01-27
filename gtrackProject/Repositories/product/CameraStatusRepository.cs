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
    public class CameraStatusRepository : ICameraStatusRepository
    {
        private GtrackDbContext _db { get; set; }
        public CameraStatusRepository()
        {
            _db = new GtrackDbContext();
        }

        public IQueryable<ProductCameraStatus> GetAll()
        {
            return _db.ProductCameraStatuss;
        }

        public async Task<ProductCameraStatus> Get(string id)
        {
            return await IdExist(id);
        }

        public async Task<ProductCameraStatus> Add(ProductCameraStatus item)
        {
            var newStatus = new ProductCameraStatus()
            {
                Name = item.Name
            };

            newStatus = _db.ProductCameraStatuss.Add(newStatus);
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

        public async Task<bool> Update(ProductCameraStatus item)
        {
            var status = await IdExist(item.Id);
            status.Name = item.Name;

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

        public async Task<bool> Remove(string id)
        {
            var status = await IdExist(id);

            _db.ProductCameraStatuss.Remove(status);
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

        private async Task<ProductCameraStatus> IdExist(string id)
        {
            var status = await _db.ProductCameraStatuss.FirstOrDefaultAsync(t => t.Id == id);
            if (status != null) return status;
            throw new KeyNotFoundException("id");
        }
    }
}