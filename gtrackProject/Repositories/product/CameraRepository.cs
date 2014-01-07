using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using gtrackProject.Models.dbContext;
using gtrackProject.Models.product;
using gtrackProject.Repositories.product.IRepos;

namespace gtrackProject.Repositories.product
{
    public class CameraRepository : ICameraRepository
    {
        private GtrackDbContext _db { get; set; }

        public CameraRepository()
        {
            _db = new GtrackDbContext();
        }

        public IQueryable<ProductCamera> GetAll()
        {
            return _db.ProductCameras;
        }

        public async Task<ProductCamera> Get(int id)
        {
            return await IdExist(id);
        }

        public async Task<ProductCamera> Add(ProductCamera item)
        {
            var newCam = new ProductCamera();
            if (item.ProductId != null)
            {
                if (await ProductExist(item.ProductId.Value))
                {
                    newCam.ProductId = item.ProductId;
                }
            }
            else
            {
                throw new ArgumentNullException("ProductId");
            }

            if (!String.IsNullOrEmpty(item.Serial))
            {
                if (!(await SerialExist(item.Serial)))
                {
                    newCam.Serial = item.Serial;
                }
            }
            else
            {
                throw new ArgumentNullException("Serial");
            }

            newCam = _db.ProductCameras.Add(newCam);
            try
            {
                await _db.SaveChangesAsync();
                return newCam;
            }
            catch (DbUpdateConcurrencyException exception)
            {
                throw new DbUpdateConcurrencyException(exception.Message);
            }
        }

        public async Task<bool> Update(ProductCamera item)
        {
            var cam = await IdExist(item.Id);

            if (item.ProductId != null)
            {
                if (await ProductExist(item.ProductId.Value))
                {
                    cam.ProductId = item.ProductId;
                }
            }
            else
            {
                cam.ProductId = null;
            }

            if (!String.IsNullOrEmpty(item.Serial))
            {
                if (!(await SerialExist(item.Serial)))
                {
                    cam.Serial = item.Serial;
                }
            }
            else
            {
                throw new ArgumentNullException("Serial");
            }

            _db.Entry(cam).State = EntityState.Modified;
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
            var cam = await IdExist(id);

            _db.ProductCameras.Remove(cam);
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

        private async Task<ProductCamera> IdExist(int id)
        {
            var cam = await _db.ProductCameras.FirstOrDefaultAsync(c => c.Id == id);
            if (cam != null) return cam;
            throw new KeyNotFoundException("id");
        }
        private async Task<bool> SerialExist(string serial)
        {
            var cam = await _db.ProductCameras.FirstOrDefaultAsync(c => c.Serial == serial);
            if (cam == null) return false;
            throw new ArgumentException("This Serial ( " + serial + " ) is already taken");
        }
        private async Task<bool> ProductExist(int id)
        {
            var product = await _db.ProductGpss.FirstOrDefaultAsync(p => p.Id == id);
            if (product != null) return true;
            throw new ArgumentException("ProductId Not Found");
        }
    }
}