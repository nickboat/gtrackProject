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
    public class ProductStateRepository : IProductStateRepository
    {
        private GtrackDbContext _db { get; set; }
        public ProductStateRepository()
        {
            _db = new GtrackDbContext();
        }

        public IQueryable<GpsState> GetAll()
        {
            return _db.ProductProcessStates;
        }

        public async Task<GpsState> Get(byte id)
        {
            return await IdExist(id);
        }

        public async Task<GpsState> Add(GpsState item)
        {
            if (await NameExist(item.StatusNameTh,item.StatusNameEn)) return null;

            var newType = new GpsState()
            {
                StatusNameTh = item.StatusNameTh,
                StatusNameEn = item.StatusNameEn
            };

            newType = _db.ProductProcessStates.Add(newType);
            try
            {
                await _db.SaveChangesAsync();
                return newType;
            }
            catch (DbUpdateConcurrencyException exception)
            {
                throw new DbUpdateConcurrencyException(exception.Message);
            }
        }

        public async Task<bool> Update(GpsState item)
        {
            var type = await IdExist(item.Id);

            if (await NameExist(item.StatusNameTh,item.StatusNameEn)) return false;

            type.StatusNameTh = item.StatusNameTh;
            type.StatusNameEn = item.StatusNameEn;

            _db.Entry(type).State = EntityState.Modified;
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
            var payment = await IdExist(id);

            _db.ProductProcessStates.Remove(payment);
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

        private async Task<GpsState> IdExist(byte id)
        {
            var type = await _db.ProductProcessStates.FirstOrDefaultAsync(t => t.Id == id);
            if (type != null) return type;
            throw new KeyNotFoundException("id");
        }

        private async Task<bool> NameExist(string th,string en)
        {
            var type = await _db.ProductProcessStates.FirstOrDefaultAsync(p => p.StatusNameTh == th || p.StatusNameEn == en);
            if (type == null) return false;
            throw new ArgumentException("This name is already taken");
        }
    }
}