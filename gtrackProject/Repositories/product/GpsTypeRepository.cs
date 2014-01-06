using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using gtrackProject.Models.dbContext;
using gtrackProject.Models.product;

namespace gtrackProject.Repositories.product
{
    public class GpsTypeRepository : IGpsTypeRepository
    {
        private GtrackDbContext _db { get; set; }
        public GpsTypeRepository()
        {
            _db = new GtrackDbContext();
        }

        public IQueryable<ProductGpsType> GetAll()
        {
            return _db.ProductGpsTypes;
        }

        public async Task<ProductGpsType> Get(byte id)
        {
            return await IdExist(id);
        }

        public async Task<ProductGpsType> Add(ProductGpsType item)
        {
            if (await NameExist(item.StatusNameTh,item.StatusNameEn)) return null;

            var newType = new ProductGpsType()
            {
                StatusNameTh = item.StatusNameTh,
                StatusNameEn = item.StatusNameEn
            };

            newType = _db.ProductGpsTypes.Add(newType);
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

        public async Task<bool> Update(ProductGpsType item)
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

            _db.ProductGpsTypes.Remove(payment);
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

        private async Task<ProductGpsType> IdExist(byte id)
        {
            var type = await _db.ProductGpsTypes.FirstOrDefaultAsync(t => t.Id == id);
            if (type != null) return type;
            throw new KeyNotFoundException("id");
        }

        private async Task<bool> NameExist(string th,string en)
        {
            var type = await _db.ProductGpsTypes.FirstOrDefaultAsync(p => p.StatusNameTh == th || p.StatusNameEn == en);
            if (type == null) return false;
            throw new ArgumentException("This name is already taken");
        }
    }
}