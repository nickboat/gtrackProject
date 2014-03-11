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
    public class ProductGpsRepository : IProductGpsRepository
    {
        private GtrackDbContext _db { get; set; }

        public ProductGpsRepository()
        {
            _db = new GtrackDbContext();
        }

        public IQueryable<Gps> GetAll()
        {
            return _db.Gpss;
        }

        public async Task<Gps> Get(int id)
        {
            return await IdExist(id);
        }

        public async Task<Gps> Add(Gps item)
        {
            //todo: Manu role only ... check on ctrl
            if (item.State != 1) throw new ArgumentException("Incorrect State", "State");

            if (!item.CreateBy.HasValue)
            {
                throw new ArgumentNullException("CreateBy", "CreateBy is Required");
            }

            var newProduct = new Gps();

            var ver = await VersionExist(item.Version);
            newProduct.Version = ver.Id;

            newProduct.Serial = await CreateSerial(ver.Name);

            //status
            newProduct.CreateDate = DateTime.UtcNow;
            newProduct.CreateBy = await EmpExist(item.CreateBy.Value);
            newProduct.State = 1;

            newProduct = _db.Gpss.Add(newProduct);
            try
            {
                await _db.SaveChangesAsync();
                return newProduct;
            }
            catch (DbUpdateConcurrencyException exception)
            {
                throw new DbUpdateConcurrencyException(exception.Message);
            }
        }

        public async Task<bool> Update(Gps item)
        {
            var product = await IdExist(item.Id);

            switch (item.State)
            {
                case 2: //stock (Manu)
                    if (item.StockBy == null)
                        throw new ArgumentNullException("StockBy", "StockBy is Required for this state");
                    product.StockBy = await EmpExist(item.StockBy.Value);
                    product.StockDate = DateTime.UtcNow;
                    product.State = 2;
                    break;
                case 3: //QC (Qc)
                    if (item.QcBy == null)
                        throw new ArgumentNullException("QcBy", "QcBy is Required for this state");

                    if (item.SimId == null)
                        throw new ArgumentNullException("SimId", "SimId is Required for this state");

                    if (item.OrderId == null)
                        throw new ArgumentNullException("OrderId", "OrderId is Required for this state");

                    var by = await EmpExist(item.QcBy.Value);
                    var d = DateTime.UtcNow;
                    var newSim = await SimExist(item.SimId.Value);

                    if (product.SimId != null) //changeSim
                    {
                        var log = new LogSim
                        {
                            CreateBy = by,
                            CreateDate = d,
                            GpsId = product.Id,
                            SimAtFirst = product.SimId.Value,
                            SimNew = newSim
                        };
                        _db.LogSims.Add(log);
                        try
                        {
                            await _db.SaveChangesAsync();
                        }
                        catch (DbUpdateConcurrencyException exception)
                        {
                            throw new DbUpdateConcurrencyException(exception.Message);
                        }
                    }

                    product.SimId = newSim;
                    product.QcBy = by;
                    product.QcDate = d;
                    product.State = 3;
                    product.OrderId = item.OrderId.Value;

                    //todo: check all product.state = 3 in this order
        //          //todo: update order status = 4 (QCcomplete)

                    break;
                /*case 4: //Install (installation) ... update in order process
                    if (item.InstallBy == null)
                        throw new ArgumentNullException("InstallBy", "InstallBy is Required for this state");
                    product.InstallBy = await EmpExist(item.InstallBy.Value);
                    product.State = 4;
                    break;*/
                case 5: //Bad (Qc)
                    if (item.BadBy == null)
                        throw new ArgumentNullException("BadBy", "BadBy is Required for this state");
                    product.BadBy = await EmpExist(item.BadBy.Value);
                    product.BadDate = DateTime.UtcNow;
                    product.BadComment = item.BadComment;
                    product.State = 5;
                    break;
                case 6: //Unuseable (Manu)
                    if (item.UnuseableBy == null)
                        throw new ArgumentNullException("UnuseableBy", "UnuseableBy is Required for this state");
                    product.UnuseableBy = await EmpExist(item.UnuseableBy.Value);
                    product.UnuseableDate = DateTime.UtcNow;
                    product.UnuseableComment = item.UnuseableComment;
                    product.State = 6;
                    break;
                case 8: //Complete (installation)
                    if (item.OrderId == null)
                        throw new ArgumentNullException("OrderId", "OrderId is Required for this state");

                    //update universe.Display = 3
                    var uni = await _db.Universes.FirstOrDefaultAsync(u => u.GpsProductId == item.Id);
                    if (uni == null) throw new ArgumentNullException("Universes", "ProductId is NotFound in Universe");
                    uni.DisplayStatus = 3;
                    _db.Entry(uni).State = EntityState.Modified;

                    //check this product == SolvedProduct in FixOrder
                    var fix =
                        await _db.FixOrders.Where(f => f.SolvedProduct == item.Id)
                            .OrderByDescending(f => f.CreateDate).FirstOrDefaultAsync();
                    //if found -> update FixOrder status = 6 (Complete) and check fixorder of fixorder of ... of order and update all order status
                    if (fix != null)
                    {
                        fix.State = 6;
                        var loop = fix.FromFixOrderId.HasValue;
                        if (loop)
                        {
                            var fromFix = fix.FromFixOrder;
                            while (loop)
                            {
                                fromFix.State = 6;
                                _db.Entry(fromFix).State = EntityState.Modified;

                                if (fromFix.FromFixOrderId.HasValue)
                                    fromFix = fromFix.FromFixOrder;
                                else
                                {
                                    //call fn() check all product in order
                                    await updateOrder(item.OrderId.Value, item.Id);
                                    loop = false;
                                }
                            }
                        }
                        else
                        {
                            //call fn() check all product in order
                            await updateOrder(item.OrderId.Value, item.Id);
                        }
                    }
                    else
                    {
                        //update logFee startDate&expireDate
                        var fee =
                            await _db.LogFees.OrderByDescending(f => f.CreateDate)
                                .FirstOrDefaultAsync(f => f.GpsId == item.Id);
                        fee.StartDate = DateTime.UtcNow;
                        fee.ExpireDate = DateTime.UtcNow.AddYears(1);
                        _db.Entry(fee).State = EntityState.Modified;

                        //call fn() check all product in order
                        await updateOrder(item.OrderId.Value, item.Id);
                    }

                    product.InstallDate = DateTime.UtcNow;
                    product.State = 8;
                    break;
                case 10: //Fixed (Manu,Qc)
                    if (item.StockBy == null)
                        throw new ArgumentNullException("StockBy", "StockBy is Required for this state");
                    product.StockBy = await EmpExist(item.StockBy.Value);
                    product.StockDate = DateTime.UtcNow;
                    product.State = 2;
                    break;
                    /*case 11://Problem state ... create when FixOrder created
                    if (item.ProblemBy == null)
                        throw new ArgumentNullException("ProblemBy", "ProblemBy is Required for this state");
                    product.ProblemBy = await EmpExist(item.ProblemBy.Value);
                    product.ProblemDate = DateTime.UtcNow;
                    product.ProblemComment = item.ProblemComment;
                    product.State = 11;
                    break;*/
                default:
                    throw new ArgumentException("Incorrect State", "State");
            }

            _db.Entry(product).State = EntityState.Modified;
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
            var product = await IdExist(id);

            _db.Gpss.Remove(product);
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


        private async Task<Gps> IdExist(int id)
        {
            var product = await _db.Gpss.FirstOrDefaultAsync(p => p.Id == id);
            if (product != null) return product;
            throw new KeyNotFoundException("id");
        }
        private async Task<int> SimExist(int id)
        {
            var sim = await _db.Sims.FirstOrDefaultAsync(s => s.Id == id);
            if (sim != null) return id;
            throw new ArgumentException("SimId Not Found");
        }
        private async Task<GpsVersion> VersionExist(byte id)
        {
            var ver = await _db.GpsVersions.FirstOrDefaultAsync(v => v.Id == id);
            if (ver != null) return ver;
            throw new ArgumentException("GpsVersionsId Not Found");
        }
        private async Task<int> EmpExist(int id)
        {
            var emp = await _db.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (emp != null) return id;
            throw new ArgumentException("EmployeeId Not Found");
        }

        private async Task<string> CreateSerial(string verName)
        {
            while (true)
            {
                var r = new Random();
                var n = r.Next(1000000);
                var sr = verName + DateTime.Now.ToString("yy") + n.ToString("D6");

                var product = await _db.Gpss.FirstOrDefaultAsync(p => p.Serial == sr);
                if (product == null) return sr;
            }
        }

        private async Task<bool> updateOrder(int orderId, int itemId)
        {
            var pd = _db.Gpss.Where(g => g.OrderId == orderId && g.State != 8);
            //has One and eq this product
            if (pd.Count() != 1) return false;

            var yes = await pd.FirstOrDefaultAsync(p => p.Id == itemId);
            //if yes -> update Order status = 6 (Complete)
            if (yes != null)
            {
                var order = yes.Order;
                order.State = 6;
                _db.Entry(order).State = EntityState.Modified;
            }
            else
                throw new ArgumentNullException("Order", "ProductId is NotFound in This Order");

            return true;
        }
    }
}