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
            return _db.ProductGpss;
        }

        public async Task<Gps> Get(int id)
        {
            return await IdExist(id);
        }

        public async Task<Gps> Add(Gps item)
        {
            if (!item.CreateBy.HasValue)
            {
                throw new ArgumentNullException("CreateBy", "CreateBy is Required");
            }

            var newProduct = new Gps();

            var ver = await VersionExist(item.Version);
            newProduct.Version = ver.Id;

            newProduct.Serial = await createSerial(ver.Name);

            //status
            newProduct.CreateDate = DateTime.UtcNow;
            newProduct.CreateBy = await empExist(item.CreateBy.Value);
            newProduct.State = 1;

            newProduct = _db.ProductGpss.Add(newProduct);
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

            //config
            if (item.SimNum != null)
                product.SimNum = await PhoneNumExist(item.SimNum);

            if (item.SimBrandId != null)
                product.SimBrandId = await SimBrandExist(item.SimBrandId.Value);

            if (item.SimPaymentTypeId != null)
                product.SimPaymentTypeId = await SimPaymentExist(item.SimPaymentTypeId.Value);
            
            product.Serial = await SerialExist(item.Serial);
            var ver = await VersionExist(item.Version);
            product.Version = ver.Id;

            if (item.ExpireDate != null)//client use datetime UTC format
                product.ExpireDate = item.ExpireDate.Value;

            if (item.LastExtendDate != null)//client use datetime UTC format
                product.LastExtendDate = item.LastExtendDate.Value;

            //status
            if (item.StockBy != null)
            {
                product.StockBy = await empExist(item.StockBy.Value);
                product.StockDate = DateTime.UtcNow;
                product.State = 2;
            }
            else if (item.QcBy != null)
            {
                product.QcBy = await empExist(item.QcBy.Value);
                product.QcDate = DateTime.UtcNow;
                product.State = 3;
            }
            else if (item.InstallBy != null)
            {
                product.InstallBy = await empExist(item.InstallBy.Value);
                product.InstallDate = DateTime.UtcNow;
                product.State = 4;
                product.ErrProductComment = item.ErrProductComment;
            }
            else if (item.BadBy != null)
            {
                product.BadBy = await empExist(item.BadBy.Value);
                product.BadDate = DateTime.UtcNow;
                product.BadComment = item.BadComment;
                product.State = 5;
            }
            else if (item.UnuseableBy != null)
            {
                product.UnuseableBy = await empExist(item.UnuseableBy.Value);
                product.UnuseableDate = DateTime.UtcNow;
                product.UnuseableComment = item.UnuseableComment;
                product.State = 6;
            }
            else if (item.State == 8)
            {
                product.State = 8;
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

            _db.ProductGpss.Remove(product);
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
            var product = await _db.ProductGpss.FirstOrDefaultAsync(p => p.Id == id);
            if (product != null) return product;
            throw new KeyNotFoundException("id");
        }
        private async Task<string> PhoneNumExist(string num)
        {
            var product = await _db.ProductGpss.FirstOrDefaultAsync(p => p.SimNum == num);
            if (product == null) return num;
            throw new ArgumentException("This Phone Number ( " + num + " ) is already taken");
        }
        private async Task<byte> SimBrandExist(byte id)
        {
            var brand = await _db.SimBrands.FirstOrDefaultAsync(b => b.Id == id);
            if (brand != null) return id;
            throw new ArgumentException("SimBrandId Not Found");
        }
        private async Task<byte> SimPaymentExist(byte id)
        {
            var pay = await _db.SimPaymentTypes.FirstOrDefaultAsync(p => p.Id == id);
            if (pay != null) return id;
            throw new ArgumentException("SimPaymentTypesId Not Found");
        }
        private async Task<string> SerialExist(string serial)
        {
            var product = await _db.ProductGpss.FirstOrDefaultAsync(p => p.Serial == serial);
            if (product == null) return serial;
            throw new ArgumentException("This Serial ( " + serial + " ) is already taken");
        }
        private async Task<GpsVersion> VersionExist(byte id)
        {
            var ver = await _db.ProductGpsVersions.FirstOrDefaultAsync(v => v.Id == id);
            if (ver != null) return ver;
            throw new ArgumentException("GpsVersionsId Not Found");
        }
        private async Task<int> empExist(int id)
        {
            var emp = await _db.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (emp != null) return id;
            throw new ArgumentException("EmployeeId Not Found");
        }

        private async Task<string> createSerial(string verName)
        {
            while (true)
            {
                var r = new Random();
                var n = r.Next(1000000);
                var sr = verName + DateTime.Now.ToString("yy") + n.ToString("D6");
                if (await SerialExist(sr) == null)
                    return sr;
            }
        }
    }
}