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
    public class OdStatusRepository : IOdStatusRepository
    {
        private GtrackDbContext _db { get; set; }

        public OdStatusRepository()
        {
            _db = new GtrackDbContext();
        }

        public IQueryable<OrderStatus> GetAll()
        {
            return _db.OrderStatuss;
        }

        public async Task<OrderStatus> Get(byte id)
        {
            return await IdExist(id);
        }

        public async Task<OrderStatus> Add(OrderStatus item)
        {
            var status = new OrderStatus
            {
                StatusEn = item.StatusEn,
                StatusTh = item.StatusTh
            };
            
            status = _db.OrderStatuss.Add(status);
            try
            {
                await _db.SaveChangesAsync();
                return status;
            }
            catch (DbUpdateConcurrencyException exception)
            {
                throw new DbUpdateConcurrencyException(exception.Message);
            }
        }

        public async Task<bool> Update(OrderStatus item)
        {
            var status = await IdExist(item.Id);
            status.StatusTh = item.StatusTh;
            status.StatusEn = item.StatusEn;

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

            _db.OrderStatuss.Remove(status);
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

        private async Task<OrderStatus> IdExist(byte id)
        {
            var status = await _db.OrderStatuss.FirstOrDefaultAsync(o => o.Id == id);
            if (status != null) return status;
            throw new KeyNotFoundException("id");
        }
    }
}