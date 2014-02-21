using System;
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
    public class FixOrderRepository : IFixOrderRepository
    {
        private GtrackDbContext _db { get; set; }

        public FixOrderRepository()
        {
            _db = new GtrackDbContext();
        }

        public IQueryable<FixOrder> GetAll()
        {
            return _db.FixOrders;
        }

        public async Task<FixOrder> Get(int id)
        {
            return await IdExist(id);
        }

        public async Task<FixOrder> Add(FixOrder item)
        {
            if (!item.CreateBy.HasValue)
            {
                throw new ArgumentNullException("CreateBy", "CreateBy is Required");
            }
            if (!item.State.HasValue)
            {
                throw new ArgumentNullException("Status", "Status is Required");
            }

            var fix = new FixOrder
            {
                CreateBy = await EmpExist(item.CreateBy.Value),
                CreateDate = item.CreateDate,
                State = 1
            };

            if (item.CurrentUser != null) fix.CurrentUser = await EmpExist(item.CurrentUser.Value);
            if (item.HeadInstall != null) fix.HeadInstall = await EmpExist(item.HeadInstall.Value);
            if (item.Comment != null) fix.Comment = item.Comment;
            
            fix = _db.FixOrders.Add(fix);
            try
            {
                await _db.SaveChangesAsync();
                return fix;
            }
            catch (DbUpdateConcurrencyException exception)
            {
                throw new DbUpdateConcurrencyException(exception.Message);
            }
        }

        public async Task<bool> Update(FixOrder item)
        {
            if (!item.CreateBy.HasValue)
            {
                throw new ArgumentNullException("CreateBy", "CreateBy is Required");
            }

            if (!item.State.HasValue)
            {
                throw new ArgumentNullException("Status", "Status is Required");
            }

            var fix = await IdExist(item.Id);
            fix.State = await StateExist(item.State.Value);

            if (item.CurrentUser != null) fix.CurrentUser = await EmpExist(item.CurrentUser.Value);
            if (item.HeadInstall != null) fix.HeadInstall = await EmpExist(item.HeadInstall.Value);
            if (item.Comment != null) fix.Comment = item.Comment;

            _db.Entry(fix).State = EntityState.Modified;
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
            var fix = await IdExist(id);

            _db.FixOrders.Remove(fix);
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

        private async Task<FixOrder> IdExist(int id)
        {
            var fix = await _db.FixOrders.FirstOrDefaultAsync(o => o.Id == id);
            if (fix != null) return fix;
            throw new KeyNotFoundException("id");
        }
        private async Task<int> EmpExist(int id)
        {
            var emp = await _db.Employees.FirstOrDefaultAsync(o => o.Id == id);
            if (emp != null) return id;
            throw new ArgumentException("Employee Not Found");
        }
        private async Task<byte> StateExist(byte id)
        {
            var state = await _db.OrderProcessStates.FirstOrDefaultAsync(o => o.Id == id);
            if (state != null) return id;
            throw new ArgumentException("OrderStatus Not Found");
        }
    }
}