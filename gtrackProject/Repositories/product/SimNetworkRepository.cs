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
    public class SimNetworkRepository : ISimNetworkRepository
    {
        private GtrackDbContext _db { get; set; }

        public SimNetworkRepository()
        {
            _db = new GtrackDbContext();
        }

        public IQueryable<SimNetwork> GetAll()
        {
            return _db.SimStatuses;
        }

        public async Task<SimNetwork> Get(byte id)
        {
            return await IdExist(id);
        }

        public async Task<SimNetwork> Add(SimNetwork item)
        {
            if (await NameExist(item.BrandName)) return null;

            var newBrand = new SimNetwork
            {
                BrandName = item.BrandName
            };

            newBrand = _db.SimStatuses.Add(newBrand);
            try
            {
                await _db.SaveChangesAsync();
                return newBrand;
            }
            catch (DbUpdateConcurrencyException exception)
            {
                throw new DbUpdateConcurrencyException(exception.Message);
            }
        }

        public async Task<bool> Update(SimNetwork item)
        {
            var brand = await IdExist(item.Id);

            if (await NameExist(item.BrandName)) return false;

            brand.BrandName = item.BrandName;

            _db.Entry(brand).State = EntityState.Modified;
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
            var brand = await IdExist(id);

            _db.SimStatuses.Remove(brand);
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

        private async Task<SimNetwork> IdExist(byte id)
        {
            var brand = await _db.SimStatuses.FirstOrDefaultAsync(b => b.Id == id);
            if (brand != null) return brand;
            throw new KeyNotFoundException("id");
        }

        private async Task<bool> NameExist(string name)
        {
            var checkName = await _db.SimStatuses.FirstOrDefaultAsync(b => b.BrandName == name);
            if (checkName == null) return false;
            throw new ArgumentException("This name ( " + name + " ) is already taken");
        }
    }
}