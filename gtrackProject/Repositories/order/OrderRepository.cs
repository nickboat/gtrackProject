using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using gtrackProject.Models.dbContext;
using gtrackProject.Models.order;
using gtrackProject.Models.universe;
using gtrackProject.Models.vehicle;
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
            if (!item.CreateBy.HasValue)
            {
                throw new ArgumentNullException("CreateBy", "CreateBy is Required");
            }
            if (!item.Version.HasValue)
            {
                throw new ArgumentNullException("Version", "Version is Required");
            }

            var order = new Order
            {
                CreateBy = await EmpExist(item.CreateBy.Value),
                CreateDate = item.CreateDate,
                HdId = await HdExist(item.HdId),
                Version = await VersionExist(item.Version.Value),
                Quantity = item.Quantity,
                PricePerUnit = item.PricePerUnit,
                FeePerYear = item.FeePerYear,
                State = 1,
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
                await addVehicle(order.Quantity, order.HdId, order.Id);

                return order;
            }
            catch (DbUpdateConcurrencyException exception)
            {
                throw new DbUpdateConcurrencyException(exception.Message);
            }

        }

        public async Task<bool> Update(Order item)
        {
            if (!item.CreateBy.HasValue)
            {
                throw new ArgumentNullException("CreateBy", "CreateBy is Required");
            }
            if (!item.Version.HasValue)
            {
                throw new ArgumentNullException("Version", "Version is Required");
            }
            if (!item.State.HasValue)
            {
                throw new ArgumentNullException("Status", "Status is Required");
            }

            var order = await IdExist(item.Id);
            var changeHd = false;
            var changeQuantity = false;

            if (order.HdId != item.HdId) changeHd = true;
            if (order.Quantity != item.Quantity) changeQuantity = true;            

            order.CreateBy = await EmpExist(item.CreateBy.Value);
            order.CreateDate = item.CreateDate;
            order.HdId = await HdExist(item.HdId);
            order.Version = await VersionExist(item.Version.Value);
            order.Quantity = item.Quantity;
            order.PricePerUnit = item.PricePerUnit;
            order.FeePerYear = item.FeePerYear;
            order.State = await StatusExist(item.State.Value);
            order.Deadline = item.Deadline;            

            if (item.CurrentUser != null) order.CurrentUser = await EmpExist(item.CurrentUser.Value);
            if (item.HeadInstall != null) order.HeadInstall = await EmpExist(item.HeadInstall.Value);
            if (item.Comment != null) order.Comment = item.Comment;
            if (item.ExtendTypeId != null) order.ExtendTypeId = await ExtendExist(item.ExtendTypeId.Value);

            if (changeHd || changeQuantity) await changeOrder(changeHd, changeQuantity, order.HdId, item.HdId, order.Quantity, order.Id);

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

            await removeVehicle(id);

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
            var status = await _db.OrderProcessStates.FirstOrDefaultAsync(o => o.Id == id);
            if (status != null) return id;
            throw new ArgumentException("OrderStatus Not Found");
        }

        private async Task<bool> addVehicle(int quantity,short hdId,int orderId)
        {
            var maxIdCar = _db.Vehicles.Where(c => c.HdId == hdId).Max(c => c.IdCar);
            if (maxIdCar == null) throw new ArgumentNullException("maxIdCar");

            for (int i = 0; i < quantity; i++)
            {
                var max = Convert.ToInt32(maxIdCar) + 1;

                var newVeh = new Vehicle
                {
                    IdCar = max.ToString("D6"),
                    HdId = hdId
                };

                newVeh = _db.Vehicles.Add(newVeh);
                try
                {
                    await _db.SaveChangesAsync();

                    //add universe
                    var newUn = new Universe
                    {
                        VehicleId = newVeh.Id,
                        OrderId = orderId,
                        DisplayStatus = 2
                    };
                    _db.Universes.Add(newUn);
                    try
                    {
                        await _db.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException exception)
                    {
                        throw new DbUpdateConcurrencyException(exception.Message);
                    }
                }
                catch (DbUpdateConcurrencyException exception)
                {
                    throw new DbUpdateConcurrencyException(exception.Message);
                }
            }

            return true;
        }

        private async Task<bool> changeOrder(bool changeHd, bool changeQ, short hdOld, short hdNew, int q, int orderId)
        {
            if (changeHd)
            {
                var vehs = await _db.Vehicles.Where(v => v.HdId == hdOld).ToListAsync();
                foreach (var veh in vehs)
                {
                    _db.Vehicles.Remove(veh);
                    try
                    {
                        await _db.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException exception)
                    {
                        throw new DbUpdateConcurrencyException(exception.Message);
                    }                    
                }
                await addVehicle(q, hdNew, orderId);
                return true;
            }
            else // changeHd = false , changeQ = true
            {
                var unOrders = await _db.Universes.Where(u => u.OrderId == orderId).OrderByDescending(u => u.VehicleId).ToListAsync();
                
                if (unOrders != null)
                {
                    int sub = 0;
                    if (unOrders.Count() > q)
                    {
                        sub = unOrders.Count() - q;
                        for (int i = 0; i < sub; i++)
                        {
                            var veh = _db.Vehicles.FirstOrDefault(v => v.Id == unOrders[i].VehicleId);

                            _db.Vehicles.Remove(veh);
                            try
                            {
                                await _db.SaveChangesAsync();
                            }
                            catch (DbUpdateConcurrencyException exception)
                            {
                                throw new DbUpdateConcurrencyException(exception.Message);
                            }
                        }
                        return true;
                    }
                    else
                    {
                        sub = q - unOrders.Count();
                        await addVehicle(sub, hdOld, orderId);
                        return true;
                    }                    
                }
                else
                {
                    throw new KeyNotFoundException("orderId");
                }
            }
        }

        private async Task<bool> removeVehicle(int orderId)
        {
            var unOrders = await _db.Universes.Where(u => u.OrderId == orderId).OrderByDescending(u => u.VehicleId).ToListAsync();
            foreach (var un in unOrders)
            {
                var veh = _db.Vehicles.FirstOrDefault(v => v.Id == un.VehicleId);

                _db.Vehicles.Remove(veh);
                try
                {
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException exception)
                {
                    throw new DbUpdateConcurrencyException(exception.Message);
                }
            }
            return true;
        }
    }
}