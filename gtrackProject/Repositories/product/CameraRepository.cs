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

            if (item.State != null)
                newCam.State = await StateExist(item.State.Value);
            else
                throw new ArgumentNullException("State");

            if (item.ProductId != null)
                newCam.ProductId = await ProductExist(item.ProductId.Value);

            newCam.Serial = await SerialExist(item.Serial);

            if (item.Status != null)
                newCam.Status = await StatusExist(item.Status.Value);

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

            if (item.State != null)
                cam.State = await StateExist(item.State.Value);
            else
                throw new ArgumentNullException("State");

            if (item.ProductId != null)
            {
                cam.ProductId = await ProductExist(item.ProductId.Value);
            }
            else
            {
                cam.ProductId = null;
            }

            cam.Serial = await SerialExist(item.Serial);

            if (item.Status != null)
            {
                cam.Status = await StatusExist(item.Status.Value);
            }
            else
            {
                cam.Status = null;
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
        private async Task<string> SerialExist(string serial)
        {
            var cam = await _db.ProductCameras.FirstOrDefaultAsync(c => c.Serial == serial);
            if (cam == null) return serial;
            throw new ArgumentException("This Serial ( " + serial + " ) is already taken");
        }
        private async Task<int> ProductExist(int id)
        {
            var product = await _db.ProductGpss.FirstOrDefaultAsync(p => p.Id == id);
            if (product != null) return id;
            throw new ArgumentException("ProductId Not Found");
        }
        private async Task<byte> StatusExist(byte id)
        {
            var status = await _db.ProductCameraStatuss.FirstOrDefaultAsync(p => p.Id == id);
            if (status != null) return id;
            throw new ArgumentException("ProductId Not Found");
        }
        private async Task<byte> StateExist(byte id)
        {
            var state = await _db.ProductCameraStatuss.FirstOrDefaultAsync(p => p.Id == id);
            if (state != null) return id;
            throw new ArgumentException("ProductId Not Found");
        }
    }
}