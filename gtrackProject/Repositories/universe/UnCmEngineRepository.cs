using System;
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
    public class UnCmEngineRepository : IUnCmEngineRepository
    {
        private GtrackDbContext _db { get; set; }

        public UnCmEngineRepository()
        {
            _db = new GtrackDbContext();
        }

        public IQueryable<UnCmEngine> GetAll()
        {
            return _db.UnCmEngines;
        }

        public async Task<UnCmEngine> Get(string id)
        {
            return await IdExist(id);
        }

        public async Task<UnCmEngine> Add(UnCmEngine item)
        {
            var engine = new UnCmEngine
            {
                Name = item.Name,
                Id = await UsedIdName(item.Id)
            };

            if (!string.IsNullOrEmpty(item.MsgEn)) engine.MsgEn = item.MsgEn;
            if (!string.IsNullOrEmpty(item.MsgTh)) engine.MsgTh = item.MsgTh;

            engine = _db.UnCmEngines.Add(engine);
            try
            {
                await _db.SaveChangesAsync();
                return engine;
            }
            catch (DbUpdateConcurrencyException exception)
            {
                throw new DbUpdateConcurrencyException(exception.Message);
            }
        }

        public async Task<bool> Update(UnCmEngine item)
        {
            var engine = await IdExist(item.Id);

            engine.Name = item.Name;
            if (!string.IsNullOrEmpty(item.MsgEn)) engine.MsgEn = item.MsgEn;
            if (!string.IsNullOrEmpty(item.MsgTh)) engine.MsgTh = item.MsgTh;

            _db.Entry(engine).State = EntityState.Modified;
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

        public async Task<bool> Remove(string id)
        {
            var engine = await IdExist(id);

            _db.UnCmEngines.Remove(engine);
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

        private async Task<UnCmEngine> IdExist(string id)
        {
            var engine = await _db.UnCmEngines.FirstOrDefaultAsync(o => o.Id == id);
            if (engine != null) return engine;
            throw new KeyNotFoundException("id");
        }
        private async Task<string> UsedIdName(string id)
        {
            var engine = await _db.UnCmEngines.FirstOrDefaultAsync(o => o.Id == id);
            if (engine == null) return id;
            throw new ArgumentException("( " + id + " ) used", "id");
        }
    }
}