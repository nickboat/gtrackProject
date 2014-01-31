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
    public class UnCmTempRepository : IUnCmTempRepository
    {
        private GtrackDbContext _db { get; set; }

        public UnCmTempRepository()
        {
            _db = new GtrackDbContext();
        }

        public IQueryable<UnCmTemp> GetAll()
        {
            return _db.UnCmTemps;
        }

        public async Task<UnCmTemp> Get(string id)
        {
            return await IdExist(id);
        }

        public async Task<UnCmTemp> Add(UnCmTemp item)
        {
            var temp = new UnCmTemp
            {
                Name = item.Name,
                Id = await UsedIdName(item.Id)
            };

            if (!string.IsNullOrEmpty(item.MsgEn)) temp.MsgEn = item.MsgEn;
            if (!string.IsNullOrEmpty(item.MsgTh)) temp.MsgTh = item.MsgTh;

            temp = _db.UnCmTemps.Add(temp);
            try
            {
                await _db.SaveChangesAsync();
                return temp;
            }
            catch (DbUpdateConcurrencyException exception)
            {
                throw new DbUpdateConcurrencyException(exception.Message);
            }
        }

        public async Task<bool> Update(UnCmTemp item)
        {
            var temp = await IdExist(item.Id);

            temp.Name = item.Name;
            if (!string.IsNullOrEmpty(item.MsgEn)) temp.MsgEn = item.MsgEn;
            if (!string.IsNullOrEmpty(item.MsgTh)) temp.MsgTh = item.MsgTh;

            _db.Entry(temp).State = EntityState.Modified;
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
            var temp = await IdExist(id);

            _db.UnCmTemps.Remove(temp);
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

        private async Task<UnCmTemp> IdExist(string id)
        {
            var temp = await _db.UnCmTemps.FirstOrDefaultAsync(o => o.Id == id);
            if (temp != null) return temp;
            throw new KeyNotFoundException("id");
        }
        private async Task<string> UsedIdName(string id)
        {
            var temp = await _db.UnCmTemps.FirstOrDefaultAsync(o => o.Id == id);
            if (temp == null) return id;
            throw new ArgumentException("( " + id + " ) used", "id");
        }
    }
}