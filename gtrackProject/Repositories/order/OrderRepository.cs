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

            var order = new Order
            {
                CreateBy = await EmpExist(item.CreateBy.Value),
                CreateDate = DateTime.UtcNow,
                HdId = await HdExist(item.HdId),
                Version = await VersionExist(item.Version),
                Quantity = item.Quantity,
                State = 1,//create
                Deadline = item.Deadline
            };

            if (item.CurrentUser != null) order.CurrentUser = await EmpExist(item.CurrentUser.Value);
            if (item.HeadInstall != null) order.HeadInstall = await EmpExist(item.HeadInstall.Value);
            if (item.Comment != null) order.Comment = item.Comment;
            
            order = _db.Orders.Add(order);
            try
            {
                await _db.SaveChangesAsync();
                await AddVehicle(order.Quantity, order.HdId, order.Id);

                return order;
            }
            catch (DbUpdateConcurrencyException exception)
            {
                throw new DbUpdateConcurrencyException(exception.Message);
            }

        }

        public async Task<bool> Update(Order item)
        {
            if (!item.State.HasValue)
            {
                throw new ArgumentNullException("Status", "Status is Required");
            }

            var order = await IdExist(item.Id);
            if (item.State.Value == 4)
            {
                //order.state = QCcomplete , when all productGPS in this Order active on universe
                var uni = _db.Universes.Where(u => u.OrderId == order.Id && u.GpsProductId == null);
                if (uni.Any())
                    throw new ArgumentNullException("GpsProductId", "one or more ProductGPS in this Order incomplete!!!");
            }

            if (item.State.Value == 6)
            {
                //order.state = Complete , when all productGPS in this Order is complete or fixing in FixOrder
                var uni = _db.Universes.Where(u => u.OrderId == order.Id && u.DisplayStatus != 3);
                foreach (var u in uni)
                {
                    var fix = _db.FixOrders.FirstOrDefault(f => f.FromOrderId.Value == order.Id && f.ProblemProduct == u.GpsProductId.Value);
                    if (fix == null)
                    {throw new ArgumentNullException("GpsProductId",
                            "one or more ProductGPS in this Order incomplete!!!");}
                }
            }
            
            var changeHd = false;
            var changeQuantity = false;

            if (order.HdId != item.HdId) changeHd = true;
            if (order.Quantity != item.Quantity) changeQuantity = true;            

            order.HdId = await HdExist(item.HdId);
            order.Version = await VersionExist(item.Version);
            order.Quantity = item.Quantity;
            order.State = await StatusExist(item.State.Value);
            order.Deadline = item.Deadline;

            if (item.CurrentUser != null) order.CurrentUser = await EmpExist(item.CurrentUser.Value);
            if (item.HeadInstall != null) order.HeadInstall = await EmpExist(item.HeadInstall.Value);
            if (item.Comment != null) order.Comment = item.Comment;

            //changeHD ... move vehicle from ole hd to new hd
            //changeQuantity ... add or delete vehicle to eq New Order Quantity
            if (changeHd || changeQuantity) await ChangeOrder(changeHd, order.HdId, item.HdId, order.Quantity, order.Id);
            
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

            //delete all vehicle in this order
            var unOrders = await _db.Universes.Where(u => u.OrderId == order.Id).OrderByDescending(u => u.VehicleId).ToListAsync();
            foreach (var veh in unOrders.Select(un => _db.Vehicles.FirstOrDefault(v => v.Id == un.VehicleId)))
            {
                _db.Vehicles.Remove(veh);
            }

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
        private async Task<byte> StatusExist(byte id)
        {
            var status = await _db.OrderProcessStates.FirstOrDefaultAsync(o => o.Id == id);
            if (status != null) return id;
            throw new ArgumentException("OrderStatus Not Found");
        }

        private async Task<bool> AddVehicle(int quantity,short hdId,int orderId)
        {
            var car = _db.Vehicles.Where(c => c.HdId == hdId);//.Max(c => c.IdCar);

            //New HD and New Vehicle ... start at "000001"
            var maxIdCar = "0";
            //add New vehicle to current hd ... start at max idcar + 1
            if (car.Any()) maxIdCar = car.Max(c => c.IdCar);

            var max = Convert.ToInt32(maxIdCar);

            for (var i = 0; i < quantity; i++)
            {
                max = max + 1;

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
                        DisplayStatus = 2//test
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

        private async Task<bool> ChangeOrder(bool changeHd, short hdOld, short hdNew, int q, int orderId)
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
                await AddVehicle(q, hdNew, orderId);
                return true;
            }

            // changeHd = false
            var unOrders = await _db.Universes.Where(u => u.OrderId == orderId).OrderByDescending(u => u.VehicleId).ToListAsync();
            if (unOrders == null) throw new KeyNotFoundException("orderId");

            var sub = 0;
            if (unOrders.Count() > q)
            {
                sub = unOrders.Count() - q;
                for (var i = 0; i < sub; i++)
                {
                    var veh = _db.Vehicles.FirstOrDefault(v => v.Id == unOrders[i].VehicleId);
                    _db.Vehicles.Remove(veh);
                }

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

            //unOrders.Count() < q
            sub = q - unOrders.Count();
            await AddVehicle(sub, hdOld, orderId);
            return true;
        }
    }
}