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
            if (item.State == 1) throw new ArgumentException("Incorrect State", "State");

            var product = await IdExist(item.Id);

            //status
            switch (item.State)
            {
                case 2://stock
                    if (item.StockBy == null)
                        throw new ArgumentNullException("StockBy", "StockBy is Required for this state");
                    product.StockBy = await EmpExist(item.StockBy.Value);
                    product.StockDate = DateTime.UtcNow;
                    product.State = 2;
                    break;
                case 3://QC
                    if (item.QcBy == null)
                        throw new ArgumentNullException("QcBy", "QcBy is Required for this state");

                    if (item.SimId == null)
                        throw new ArgumentNullException("SimId", "SimId is Required for this state");

                    var by = await EmpExist(item.QcBy.Value);
                    var d = DateTime.UtcNow;
                    var newSim = await SimExist(item.SimId.Value);

                    if (product.SimId != null)//changeSim
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

                    //todo: check all product in this order ... update order status = 4 (QCcomplete)
                    break;
                case 4://Install
                    if (item.InstallBy == null)
                        throw new ArgumentNullException("InstallBy", "InstallBy is Required for this state");
                    product.InstallBy = await EmpExist(item.InstallBy.Value);
                    product.InstallDate = DateTime.UtcNow;
                    product.State = 4;
                    break;
                case 5://Bad
                    if (item.BadBy == null)
                        throw new ArgumentNullException("BadBy", "BadBy is Required for this state");
                    product.BadBy = await EmpExist(item.BadBy.Value);
                    product.BadDate = DateTime.UtcNow;
                    product.BadComment = item.BadComment;
                    product.State = 5;
                    break;
                case 6://Unuseable
                    if (item.UnuseableBy == null)
                        throw new ArgumentNullException("UnuseableBy", "UnuseableBy is Required for this state");
                    product.UnuseableBy = await EmpExist(item.UnuseableBy.Value);
                    product.UnuseableDate = DateTime.UtcNow;
                    product.UnuseableComment = item.UnuseableComment;
                    product.State = 6;
                    break;
                case 8://Complete
                    product.InstallDate = DateTime.UtcNow;
                    product.State = 8;

                    //todo: check all product in this order ... update order status = 6 (Complete)
                    //todo: check fixorder of fixorder of ... of order and update all order status
                    //todo: if logfee notfound ... insert startDate & expireDate 
                    break;
                case 10://Fixed
                    if (item.StockBy == null)
                        throw new ArgumentNullException("StockBy", "StockBy is Required for this state");
                    product.StockBy = await EmpExist(item.StockBy.Value);
                    product.StockDate = DateTime.UtcNow;
                    product.State = 2;
                    break;
                case 11://Problem
                    if (item.ProblemBy == null)
                        throw new ArgumentNullException("ProblemBy", "ProblemBy is Required for this state");
                    product.ProblemBy = await EmpExist(item.ProblemBy.Value);
                    product.ProblemDate = DateTime.UtcNow;
                    product.ProblemComment = item.ProblemComment;
                    product.State = 11;
                    break;
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
    }
}