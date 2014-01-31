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

        public IQueryable<ProductGps> GetAll()
        {
            return _db.ProductGpss;
        }

        public async Task<ProductGps> Get(int id)
        {
            return await IdExist(id);
        }

        public async Task<ProductGps> Add(ProductGps item)
        {
            if (!item.CreateBy.HasValue)
            {
                throw new ArgumentNullException("CreateBy", "CreateBy is Required");
            }

            var newProduct = new ProductGps();

            //config
            newProduct.SimNum = await PhoneNumExist(item.SimNum);

            if (item.SimBrandId != null) 
                newProduct.SimBrandId = await SimBrandExist(item.SimBrandId.Value);

            if (item.SimPaymentTypeId != null)
                newProduct.SimPaymentTypeId = await SimPaymentExist(item.SimPaymentTypeId.Value);

            if (item.MemoryId != null)
                newProduct.MemoryId = await MemoryExist(item.MemoryId.Value);

            newProduct.Serial = await SerialExist(item.Serial);
            newProduct.Version = await VersionExist(item.Version);

            //status
            newProduct.CreateDate = item.CreateDate;
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

        public async Task<bool> Update(ProductGps item)
        {
            var product = await IdExist(item.Id);

            //config
            product.SimNum = await PhoneNumExist(item.SimNum);

            if (item.SimBrandId != null)
                product.SimBrandId = await SimBrandExist(item.SimBrandId.Value);

            if (item.SimPaymentTypeId != null)
                product.SimPaymentTypeId = await SimPaymentExist(item.SimPaymentTypeId.Value);

            if (item.MemoryId != null)
                product.MemoryId = await MemoryExist(item.MemoryId.Value);

            product.Serial = await SerialExist(item.Serial);
            product.Version = await VersionExist(item.Version);

            if (item.ExpireDate != null)
                product.ExpireDate = item.ExpireDate.Value;

            if (item.LastExtendDate != null)
                product.LastExtendDate = item.LastExtendDate.Value;

            //status
            if (item.StockBy != null)
            {
                product.StockBy = await empExist(item.StockBy.Value);
                product.StockDate = item.StockDate;
                product.State = 2;
            }
            else if (item.QcBy != null)
            {
                product.QcBy = await empExist(item.QcBy.Value);
                product.QcDate = item.QcDate;
                product.State = 3;
            }
            else if (item.InstallBy != null)
            {
                product.InstallBy = await empExist(item.InstallBy.Value);
                product.InstallDate = item.InstallDate;
                product.State = 4;
                product.ErrProductComment = item.ErrProductComment;
            }
            else if (item.BadBy != null)
            {
                product.BadBy = await empExist(item.BadBy.Value);
                product.BadDate = item.BadDate;
                product.BadComment = item.BadComment;
                product.State = 5;
            }
            else if (item.UnuseableBy != null)
            {
                product.UnuseableBy = await empExist(item.UnuseableBy.Value);
                product.UnuseableDate = item.UnuseableDate;
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


        private async Task<ProductGps> IdExist(int id)
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
        private async Task<byte> VersionExist(byte id)
        {
            var ver = await _db.ProductGpsVersions.FirstOrDefaultAsync(v => v.Id == id);
            if (ver != null) return id;
            throw new ArgumentException("GpsVersionsId Not Found");
        }
        private async Task<int> empExist(int id)
        {
            var emp = await _db.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (emp != null) return id;
            throw new ArgumentException("EmployeeId Not Found");
        }
        private async Task<byte> MemoryExist(byte id)
        {
            var status = await _db.ProductGpsMemorys.FirstOrDefaultAsync(s => s.Id == id);
            if (status != null) return id;
            throw new ArgumentException("ProductGpsMemoryId Not Found");
        }
    }
}