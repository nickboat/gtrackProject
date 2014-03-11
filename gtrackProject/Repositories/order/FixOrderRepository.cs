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

            var gps = await GpsExist(item.ProblemProduct);
            var uni = await _db.Universes.FirstOrDefaultAsync(u => u.GpsProductId == gps.Id);
            var usrCreate = await EmpExist(item.CreateBy.Value);
            var d = DateTime.UtcNow;

            var fix = new FixOrder
            {
                CreateBy = usrCreate,
                CreateDate = d,
                State = 1,
                ProblemProduct = gps.Id
            };

            //update product.State = bad & universe.DisplayStatus = test
            uni.DisplayStatus = 2;//test
            gps.State = 5;//bad
            gps.BadBy = usrCreate;
            gps.BadDate = d;
            
            if (item.Comment != null)
            {
                fix.Comment = item.Comment;
                gps.BadComment = item.Comment;
            }

            if (item.FromFixOrderId != null) fix.FromFixOrderId = await FixExist(item.FromFixOrderId.Value);
            if (item.FromOrderId != null) fix.FromOrderId = await OrderExist(item.FromOrderId.Value);

            //clear currentUser
            fix.CurrentUser = null;

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

            switch (item.State)
            {
                case 2: //checked (cs)
                    if (fix.ProblemProduct != item.ProblemProduct)
                    {
                        var uniOld = await _db.Universes.FirstOrDefaultAsync(u => u.GpsProductId == fix.ProblemProduct);
                        var gpsOld = fix.ProblemGps;
                        var uniNew = await _db.Universes.FirstOrDefaultAsync(u => u.GpsProductId == item.ProblemProduct);
                        var gpsNew = await GpsExist(item.ProblemProduct);

                        uniNew.DisplayStatus = 2;//test
                        gpsNew.State = 5;//bad
                        gpsNew.BadBy = gpsOld.BadBy;
                        gpsNew.BadDate = gpsOld.BadDate;
                        _db.Entry(uniNew).State = EntityState.Modified;
                        _db.Entry(gpsNew).State = EntityState.Modified;

                        uniOld.DisplayStatus = 3;//normal
                        gpsOld.State = 4;//install
                        gpsOld.BadBy = null;
                        gpsOld.BadDate = null;
                        _db.Entry(uniOld).State = EntityState.Modified;
                        _db.Entry(gpsOld).State = EntityState.Modified;
                    }

                    fix.FromFixOrderId = item.FromFixOrderId;
                    fix.FromOrderId = item.FromOrderId;
                    fix.Comment = item.Comment;
                    fix.CurrentUser = null;
                    fix.State = 2;
                    break;
                /*case 3: //QCworking (Qc) ... update in updateCurrentUser
                        //add SolvedProduct to fixOrder
                    break;*/
                case 4: //QCcomplete (Qc)
                    if (!fix.SolvedProduct.HasValue)
                        throw new ArgumentNullException("SolvedProduct", "SolvedProduct in this FixOrder incomplete!!!");

                    if (fix.ProblemProduct != fix.SolvedProduct)
                    {
                        //update logFee.GpsId from problem_id to solved_id
                        var fee =
                            await _db.LogFees.OrderByDescending(f => f.CreateDate)
                                .FirstOrDefaultAsync(f => f.GpsId == fix.ProblemProduct);

                        if (fee == null)
                            throw new ArgumentNullException("ProblemProduct", "Notfound ProblemProduct in LogFees!!!");

                        fee.GpsId = fix.SolvedProduct.Value;
                        _db.Entry(fee).State = EntityState.Modified;
                    }

                    fix.State = 4;
                    fix.CurrentUser = null;
                    break;
                case 5: //Installation (cs)
                    if (item.HeadInstall == null)
                        throw new ArgumentNullException("HeadInstall", "HeadInstall is Required for this state");
                    
                    fix.HeadInstall = await EmpExist(item.HeadInstall.Value);
                    fix.State = 5;
                    fix.CurrentUser = null;
                    break;
                /*case 6: //Complete (install) ... update in product process
                break;*/
                /*case 8: //Incomplete (install) ... when create fixOrder from this Order
                    break;*/
                default:
                    throw new ArgumentException("Incorrect State", "State");
            }

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

        public async Task<bool> UserActive(int fixId, string aspId, bool isQC)
        {
            var usr = await _db.Employees.FirstOrDefaultAsync(e => e.AspId == aspId);
            if (usr == null) throw new ArgumentException("Invalid User Access!!!");

            var fix = await _db.FixOrders.FirstOrDefaultAsync(f => f.Id == fixId);
            if (fix == null) throw new ArgumentException("FixOrderId Not Found");

            if (fix.CurrentUser != null) throw new ArgumentException("Another User is currently use this fixorder");

            fix.CurrentUser = usr.Id;
            if (isQC) fix.State = 3;

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