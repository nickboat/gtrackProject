using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using gtrackProject.Models.dbContext;
using gtrackProject.Models.order;
using gtrackProject.Models.product;
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

            var fix = new FixOrder
            {
                CreateBy = await EmpExist(item.CreateBy.Value),
                CreateDate = DateTime.UtcNow,
                State = 1
            };

            //update product.State = bad & universe.DisplayStatus = test
            var gps = await GpsExist(item.ProblemProduct);
            var uni = await _db.Universes.FirstOrDefaultAsync(u => u.GpsProductId == gps.Id);
            uni.DisplayStatus = 2;//test
            gps.State = 5;//bad

            fix.ProblemProduct = gps.Id;
            if (item.CurrentUser != null)
            {
                var usr = await EmpExist(item.CurrentUser.Value);
                fix.CurrentUser = usr;
                gps.BadBy = usr;
            }
            if (item.HeadInstall != null)
                fix.HeadInstall = await EmpExist(item.HeadInstall.Value);
            if (item.Comment != null)
            {
                fix.Comment = item.Comment;
                gps.BadComment = item.Comment;
            }
            if (item.FromFixOrderId != null) fix.FromFixOrderId = await FixExist(item.FromFixOrderId.Value);
            if (item.FromOrderId != null) fix.FromOrderId = await OrderExist(item.FromOrderId.Value);
            
            fix = _db.FixOrders.Add(fix);
            _db.Entry(gps).State = EntityState.Modified;
            _db.Entry(uni).State = EntityState.Modified;
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
            var fix = await IdExist(item.Id);
            fix.State = await StateExist(item.State);
            if (item.State == 4)
            {
                if (item.SolvedProduct != null)
                {
                    var gps = await GpsExist(item.ProblemProduct);
                    fix.SolvedProduct = gps.Id;
                }
                else
                    throw new ArgumentNullException("SolvedProduct", "SolvedProduct is Required");
            }

            if (item.State == 5)
            {
                if (item.HeadInstall != null) 
                    fix.HeadInstall = await EmpExist(item.HeadInstall.Value);
                else
                    throw new ArgumentNullException("HeadInstall", "HeadInstall is Required");
            }

            if (item.CurrentUser != null) fix.CurrentUser = await EmpExist(item.CurrentUser.Value);
            if (item.Comment != null) fix.Comment = item.Comment;
            if (item.FromFixOrderId != null) fix.FromFixOrderId = await FixExist(item.FromFixOrderId.Value);
            if (item.FromOrderId != null) fix.FromOrderId = await OrderExist(item.FromOrderId.Value);

            if (item.State == 6)
                fix.CurrentUser = null;

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
            var state = await _db.OrderStates.FirstOrDefaultAsync(o => o.Id == id);
            if (state != null) return id;
            throw new ArgumentException("OrderStatus Not Found");
        }
        private async Task<Gps> GpsExist(int id)
        {
            var gps = await _db.Gpss.FirstOrDefaultAsync(o => o.Id == id);
            if (gps != null) return gps;
            throw new ArgumentException("Product_Id Not Found");
        }
        private async Task<int> FixExist(int id)
        {
            var fix = await _db.FixOrders.FirstOrDefaultAsync(o => o.Id == id);
            if (fix != null) return id;
            throw new ArgumentException("FixOrder Not Found");
        }
        private async Task<int> OrderExist(int id)
        {
            var order = await _db.Orders.FirstOrDefaultAsync(o => o.Id == id);
            if (order != null) return id;
            throw new ArgumentException("Order Not Found");
        }
    }
}