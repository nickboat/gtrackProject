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
    public class OdStateRepository : IOdStateRepository
    {
        private GtrackDbContext _db { get; set; }

        public OdStateRepository()
        {
            _db = new GtrackDbContext();
        }

        public IQueryable<OrderState> GetAll()
        {
            return _db.OrderProcessStates;
        }

        public async Task<OrderState> Get(byte id)
        {
            return await IdExist(id);
        }

        public async Task<OrderState> Add(OrderState item)
        {
            var state = new OrderState
            {
                StatusEn = item.StatusEn,
                StatusTh = item.StatusTh
            };
            
            state = _db.OrderProcessStates.Add(state);
            try
            {
                await _db.SaveChangesAsync();
                return state;
            }
            catch (DbUpdateConcurrencyException exception)
            {
                throw new DbUpdateConcurrencyException(exception.Message);
            }
        }

        public async Task<bool> Update(OrderState item)
        {
            var state = await IdExist(item.Id);
            state.StatusTh = item.StatusTh;
            state.StatusEn = item.StatusEn;

            _db.Entry(state).State = EntityState.Modified;
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
            var state = await IdExist(id);

            _db.OrderProcessStates.Remove(state);
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

        private async Task<OrderState> IdExist(byte id)
        {
            var state = await _db.OrderProcessStates.FirstOrDefaultAsync(o => o.Id == id);
            if (state != null) return state;
            throw new KeyNotFoundException("id");
        }
    }
}