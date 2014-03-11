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
    public class SimRepository : ISimRepository
    {
        private GtrackDbContext _db { get; set; }

        public SimRepository()
        {
            _db = new GtrackDbContext();
        }

        public IQueryable<Sim> GetAll()
        {
            return _db.Sims;
        }

        public async Task<Sim> Get(int id)
        {
            return await IdExist(id);
        }

        public async Task<Sim> Add(Sim item)
        {
            if (await NumberExist(item.Number)) return null;

            var newSim = new Sim
            {
                Number = item.Number,
                Network = await NetworkExist(item.Network),
                FeeType = await FeeExist(item.FeeType),
                Status = await StatusExist(item.Status)
            };

            newSim = _db.Sims.Add(newSim);
            try
            {
                await _db.SaveChangesAsync();
                return newSim;
            }
            catch (DbUpdateConcurrencyException exception)
            {
                throw new DbUpdateConcurrencyException(exception.Message);
            }
        }

        public async Task<bool> Update(Sim item)
        {
            var sim = await IdExist(item.Id);

            if (await NumberExist(item.Number)) return false;

            sim.Number = item.Number;
            sim.Network = await NetworkExist(item.Network);
            sim.FeeType = await FeeExist(item.FeeType);
            sim.Status = await StatusExist(item.Status);

            _db.Entry(sim).State = EntityState.Modified;
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

        public async Task<bool> Remove(int id)
        {
            var brand = await IdExist(id);

            _db.Sims.Remove(brand);
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

        private async Task<Sim> IdExist(int id)
        {
            var sim = await _db.Sims.FirstOrDefaultAsync(b => b.Id == id);
            if (sim != null) return sim;
            throw new KeyNotFoundException("id");
        }
        private async Task<byte> NetworkExist(byte id)
        {
            var sim = await _db.SimNetworks.FirstOrDefaultAsync(b => b.Id == id);
            if (sim != null) return id;
            throw new KeyNotFoundException("id");
        }
        private async Task<byte> FeeExist(byte id)
        {
            var sim = await _db.SimFeeTypes.FirstOrDefaultAsync(b => b.Id == id);
            if (sim != null) return id;
            throw new KeyNotFoundException("id");
        }
        private async Task<byte> StatusExist(byte id)
        {
            var sim = await _db.SimStatuses.FirstOrDefaultAsync(b => b.Id == id);
            if (sim != null) return id;
            throw new KeyNotFoundException("id");
        }
        private async Task<bool> NumberExist(string num)
        {
            var checkName = await _db.Sims.FirstOrDefaultAsync(b => b.Number == num);
            if (checkName == null) return false;
            throw new ArgumentException("This name ( " + num + " ) is already taken");
        }
    }
}