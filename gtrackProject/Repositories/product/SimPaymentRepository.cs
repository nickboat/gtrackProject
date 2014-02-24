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
    public class SimPaymentRepository :ISimPaymentRepository
    {
        private GtrackDbContext _db { get; set; }
        public SimPaymentRepository()
        {
            _db = new GtrackDbContext();
        }

        public IQueryable<SimFeeType> GetAll()
        {
            return _db.SimPaymentTypes;
        }

        public async Task<SimFeeType> Get(byte id)
        {
            return await IdExist(id);
        }

        public async Task<SimFeeType> Add(SimFeeType item)
        {
            if (await NameExist(item.PaymentName)) return null;

            var newPayment = new SimFeeType()
            {
                PaymentName = item.PaymentName
            };

            newPayment = _db.SimPaymentTypes.Add(newPayment);
            try
            {
                await _db.SaveChangesAsync();
                return newPayment;
            }
            catch (DbUpdateConcurrencyException exception)
            {
                throw new DbUpdateConcurrencyException(exception.Message);
            }
        }

        public async Task<bool> Update(SimFeeType item)
        {
            var payment = await IdExist(item.Id);

            if (await NameExist(item.PaymentName)) return false;

            payment.PaymentName = item.PaymentName;

            _db.Entry(payment).State = EntityState.Modified;
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

            _db.SimPaymentTypes.Remove(payment);
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

        private async Task<SimFeeType> IdExist(byte id)
        {
            var brand = await _db.SimPaymentTypes.FirstOrDefaultAsync(p => p.Id == id);
            if (brand != null) return brand;
            throw new KeyNotFoundException("id");
        }

        private async Task<bool> NameExist(string name)
        {
            var checkName = await _db.SimPaymentTypes.FirstOrDefaultAsync(p => p.PaymentName == name);
            if (checkName == null) return false;
            throw new ArgumentException("This name ( " + name + " ) is already taken");
        }
    }
}