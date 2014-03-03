using System;
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
    public class GpsVersionRepository : IGpsVersionRepository
    {
        private GtrackDbContext _db { get; set; }
        public GpsVersionRepository()
        {
            _db = new GtrackDbContext();
        }

        public IQueryable<GpsVersion> GetAll()
        {
            return _db.GpsVersions;
        }

        public async Task<GpsVersion> Get(byte id)
        {
            return await IdExist(id);
        }

        public async Task<GpsVersion> Add(GpsVersion item)
        {
            if (await NameExist(item.Name)) return null;

            var newVer = new GpsVersion()
            {
                Name = item.Name
            };

            newVer = _db.GpsVersions.Add(newVer);
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

        public async Task<bool> Update(GpsVersion item)
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

            _db.GpsVersions.Remove(ver);
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

        private async Task<GpsVersion> IdExist(byte id)
        {
            var ver = await _db.GpsVersions.FirstOrDefaultAsync(v => v.Id == id);
            if (ver != null) return ver;
            throw new KeyNotFoundException("id");
        }

        private async Task<bool> NameExist(string name)
        {
            var ver = await _db.GpsVersions.FirstOrDefaultAsync(v => v.Name == name);
            if (ver == null) return false;
            throw new ArgumentException("This name ( " + name + " ) is already taken");
        }
    }
}