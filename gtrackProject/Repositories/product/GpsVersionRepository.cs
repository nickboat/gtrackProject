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
    public class GpsVersionRepository : IGpsVersionRepository
    {
        private GtrackDbContext _db { get; set; }
        public GpsVersionRepository()
        {
            _db = new GtrackDbContext();
        }

        public IQueryable<ProductGpsVersion> GetAll()
        {
            return _db.ProductGpsVersions;
        }

        public async Task<ProductGpsVersion> Get(byte id)
        {
            return await IdExist(id);
        }

        public async Task<ProductGpsVersion> Add(ProductGpsVersion item)
        {
            if (await NameExist(item.Name)) return null;

            var newVer = new ProductGpsVersion()
            {
                Name = item.Name
            };

            newVer = _db.ProductGpsVersions.Add(newVer);
            try
            {
                await _db.SaveChangesAsync();
                return newVer;
            }
            catch (DbUpdateConcurrencyException exception)
            {
                throw new DbUpdateConcurrencyException(exception.Message);
            }
        }

        public async Task<bool> Update(ProductGpsVersion item)
        {
            var ver = await IdExist(item.Id);

            if (await NameExist(item.Name)) return false;

            ver.Name = item.Name;

            _db.Entry(ver).State = EntityState.Modified;
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
            var ver = await IdExist(id);

            _db.ProductGpsVersions.Remove(ver);
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

        private async Task<ProductGpsVersion> IdExist(byte id)
        {
            var ver = await _db.ProductGpsVersions.FirstOrDefaultAsync(v => v.Id == id);
            if (ver != null) return ver;
            throw new KeyNotFoundException("id");
        }

        private async Task<bool> NameExist(string name)
        {
            var ver = await _db.ProductGpsVersions.FirstOrDefaultAsync(v => v.Name == name);
            if (ver == null) return false;
            throw new ArgumentException("This name ( " + name + " ) is already taken");
        }
    }
}