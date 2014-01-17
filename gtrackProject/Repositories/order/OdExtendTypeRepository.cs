using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using gtrackProject.Models.dbContext;
using gtrackProject.Models.order;
using gtrackProject.Repositories.order.IRepos;

namespace gtrackProject.Repositories.order
{
    public class OdExtendTypeRepository : IOdExtendTypeRepository
    {
        private GtrackDbContext _db { get; set; }

        public OdExtendTypeRepository()
        {
            _db = new GtrackDbContext();
        }

        public IQueryable<OrderExtendType> GetAll()
        {
            return _db.OrderExtendTypes;
        }

        public async Task<OrderExtendType> Get(byte id)
        {
            return await IdExist(id);
        }

        public async Task<OrderExtendType> Add(OrderExtendType item)
        {
            var ext = new OrderExtendType
            {
                TypeName = item.TypeName,
                Value = item.Value
            };
            
            ext = _db.OrderExtendTypes.Add(ext);
            try
            {
                await _db.SaveChangesAsync();
                return ext;
            }
            catch (DbUpdateConcurrencyException exception)
            {
                throw new DbUpdateConcurrencyException(exception.Message);
            }
        }

        public async Task<bool> Update(OrderExtendType item)
        {
            var ext = await IdExist(item.Id);
            ext.TypeName = item.TypeName;
            ext.Value = item.Value;

            _db.Entry(ext).State = EntityState.Modified;
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
            var ext = await IdExist(id);

            _db.OrderExtendTypes.Remove(ext);
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

        private async Task<OrderExtendType> IdExist(byte id)
        {
            var ext = await _db.OrderExtendTypes.FirstOrDefaultAsync(o => o.Id == id);
            if (ext != null) return ext;
            throw new KeyNotFoundException("id");
        }
    }
}