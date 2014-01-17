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
    public class OrderRepository : IOrderRepository
    {
        private GtrackDbContext _db { get; set; }

        public OrderRepository()
        {
            _db = new GtrackDbContext();
        }

        public IQueryable<Order> GetAll()
        {
            return _db.Orders;
        }

        public async Task<Order> Get(int id)
        {
            return await IdExist(id);
        }

        public async Task<Order> Add(Order item)
        {
            var order = new Order
            {
                CreateBy = await EmpExist(item.CreateBy),
                CreateDate = item.CreateDate,
                HdId = await HdExist(item.HdId),
                Version = await VersionExist(item.Version),
                Quantity = item.Quantity,
                PricePerUnit = item.PricePerUnit,
                FeePerYear = item.FeePerYear,
                Status = 1,
                Deadline = item.Deadline
            };

            if (item.CurrentUser != null) order.CurrentUser = await EmpExist(item.CurrentUser.Value);
            if (item.HeadInstall != null) order.HeadInstall = await EmpExist(item.HeadInstall.Value);
            if (item.Comment != null) order.Comment = item.Comment;
            if (item.ExtendTypeId != null) order.ExtendTypeId = await ExtendExist(item.ExtendTypeId.Value);
            
            order = _db.Orders.Add(order);
            try
            {
                await _db.SaveChangesAsync();
                return order;
            }
            catch (DbUpdateConcurrencyException exception)
            {
                throw new DbUpdateConcurrencyException(exception.Message);
            }
        }

        public async Task<bool> Update(Order item)
        {
            var order = await IdExist(item.Id);
            order.CreateBy = await EmpExist(item.CreateBy);
            order.CreateDate = item.CreateDate;
            order.HdId = await HdExist(item.HdId);
            order.Version = await VersionExist(item.Version);
            order.Quantity = item.Quantity;
            order.PricePerUnit = item.PricePerUnit;
            order.FeePerYear = item.FeePerYear;
            order.Status = await StatusExist(item.Status);
            order.Deadline = item.Deadline;

            if (item.CurrentUser != null) order.CurrentUser = await EmpExist(item.CurrentUser.Value);
            if (item.HeadInstall != null) order.HeadInstall = await EmpExist(item.HeadInstall.Value);
            if (item.Comment != null) order.Comment = item.Comment;
            if (item.ExtendTypeId != null) order.ExtendTypeId = await ExtendExist(item.ExtendTypeId.Value);

            _db.Entry(order).State = EntityState.Modified;
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
            var order = await IdExist(id);

            _db.Orders.Remove(order);
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

        private async Task<Order> IdExist(int id)
        {
            var order = await _db.Orders.FirstOrDefaultAsync(o => o.Id == id);
            if (order != null) return order;
            throw new KeyNotFoundException("id");
        }
        private async Task<int> EmpExist(int id)
        {
            var emp = await _db.Employees.FirstOrDefaultAsync(o => o.Id == id);
            if (emp != null) return id;
            throw new ArgumentException("Employee Not Found");
        }
        private async Task<short> HdExist(short id)
        {
            var hd = await _db.Hds.FirstOrDefaultAsync(o => o.Id == id);
            if (hd != null) return id;
            throw new ArgumentException("Header Not Found");
        }
        private async Task<byte> VersionExist(byte id)
        {
            var ver = await _db.ProductGpsVersions.FirstOrDefaultAsync(o => o.Id == id);
            if (ver != null) return id;
            throw new ArgumentException("ProductGpsVersion Not Found");
        }
        private async Task<byte> ExtendExist(byte id)
        {
            var ext = await _db.OrderExtendTypes.FirstOrDefaultAsync(o => o.Id == id);
            if (ext != null) return id;
            throw new ArgumentException("OrderExtendType Not Found");
        }
        private async Task<byte> StatusExist(byte id)
        {
            var status = await _db.OrderStatuss.FirstOrDefaultAsync(o => o.Id == id);
            if (status != null) return id;
            throw new ArgumentException("OrderStatus Not Found");
        }
    }
}