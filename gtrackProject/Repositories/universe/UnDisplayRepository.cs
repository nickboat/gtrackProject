using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using gtrackProject.Models.dbContext;
using gtrackProject.Models.universe;
using gtrackProject.Repositories.universe.IRepos;

namespace gtrackProject.Repositories.universe
{
    public class UnDisplayRepository : IUnDisplayRepository
    {
        private GtrackDbContext _db { get; set; }

        public UnDisplayRepository()
        {
            _db = new GtrackDbContext();
        }

        public IQueryable<UnDisplayStatus> GetAll()
        {
            return _db.UnDisplayStatuss;
        }

        public async Task<UnDisplayStatus> Get(byte id)
        {
            return await IdExist(id);
        }

        public async Task<UnDisplayStatus> Add(UnDisplayStatus item)
        {
            var dis = new UnDisplayStatus
            {
                Name = item.Name
            };
            
            dis = _db.UnDisplayStatuss.Add(dis);
            try
            {
                await _db.SaveChangesAsync();
                return dis;
            }
            catch (DbUpdateConcurrencyException exception)
            {
                throw new DbUpdateConcurrencyException(exception.Message);
            }
        }

        public async Task<bool> Update(UnDisplayStatus item)
        {
            var dis = await IdExist(item.Id);
            dis.Name = item.Name;

            _db.Entry(dis).State = EntityState.Modified;
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
            var dis = await IdExist(id);

            _db.UnDisplayStatuss.Remove(dis);
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

        private async Task<UnDisplayStatus> IdExist(byte id)
        {
            var dis = await _db.UnDisplayStatuss.FirstOrDefaultAsync(o => o.Id == id);
            if (dis != null) return dis;
            throw new KeyNotFoundException("id");
        }
    }
}