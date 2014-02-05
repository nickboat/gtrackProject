using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using gtrackProject.Models.account;
using gtrackProject.Models.dbContext;
using gtrackProject.Repositories.account.IRepos;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace gtrackProject.Repositories.account
{
    public class CustomerRepository : ICustomerRepository
    {
        private UserManager<IdentityUser> UserManager { get; set; }
        private RoleManager<IdentityRole> RoleManager { get; set; }
        private IdentityDbContext AspContext { get; set; }
        private GtrackDbContext _db { get; set; }

        public CustomerRepository()
        {
            AspContext = new IdentityDbContext();
            _db = new GtrackDbContext();
            UserManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>());
            RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());
        }

        public IEnumerable<CustomerModel> GetByHd(short hdId)
        {
            var list = new List<CustomerModel>();
            var custs = _db.Customers.Where(c => c.Hd_Id == hdId);
            if (!custs.Any()) return null;

            foreach (var cust in custs)
            {
                var userIden = UserManager.FindById(cust.Asp_Id);

                var custModel = new CustomerModel
                {
                    Id = cust.Id,
                    UserName = userIden.UserName,
                    FullName = cust.FullName,
                    Phone = cust.Phone,
                    CompanyName = cust.CompanyName,
                    Email = cust.Email,
                    Hd_Id = cust.Hd_Id
                };
                list.Add(custModel);
            }

            return list;
        }

        public async Task<CustomerModel> Get(int id)
        {
            var cust = await _db.Customers.FirstOrDefaultAsync(c => c.Id == id);
            if (cust == null) throw new KeyNotFoundException("id");

            var userIden = await UserManager.FindByIdAsync(cust.Asp_Id);

            var custModel = new CustomerModel
            {
                Id = cust.Id,
                UserName = userIden.UserName,
                FullName = cust.FullName,
                Phone = cust.Phone,
                CompanyName = cust.CompanyName,
                Email = cust.Email,
                Hd_Id = cust.Hd_Id
            };

            return custModel;
        }

        public async Task<CustomerModel> Add(CustomerModel item)
        {
            //add to asp.net Identity
            var usrIden = new IdentityUser(item.UserName);
            var usrResult = await UserManager.CreateAsync(usrIden, item.UserName);//pass is same username **by default**
            if (!usrResult.Succeeded)
            {
                throw new DbUpdateException(usrResult.Errors.First());
            }

            var roleResult = await UserManager.AddToRoleAsync(usrIden.Id, "customer");
            if (!roleResult.Succeeded)
            {
                throw new DbUpdateException(roleResult.Errors.First());
            }

            var newCust = new Customer
            {
                Hd_Id = item.Hd_Id,
                Asp_Id = usrIden.Id,
                CompanyName=item.CompanyName,
                Email = item.Email,
                FullName=item.FullName,
                Phone = item.Phone
            };
            _db.Customers.Add(newCust);
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                //remove asp.net identity user
                var usr = AspContext.Users.First(u => u.Id == usrIden.Id);
                //AspContext.Users.Remove(usr);
                //AspContext.SaveChanges();
                if (usr == null) throw new DbUpdateConcurrencyException(ex.Message);
                AspContext.Users.Remove(usr);

                try
                {
                    AspContext.SaveChanges();
                }
                catch (DbUpdateConcurrencyException aspEx)
                {
                    throw new DbUpdateConcurrencyException(aspEx.Message);
                }

                throw new DbUpdateConcurrencyException(ex.Message);
            }
            item.Id = newCust.Id;

            return item;
        }

        public async Task<bool> Update(CustomerModel item)
        {
            var cust = await _db.Customers.FirstOrDefaultAsync(c => c.Id == item.Id);
            if (cust == null)
            {
                throw new KeyNotFoundException("id");
            }

            var header = await _db.Hds.FirstOrDefaultAsync(h => h.Id == item.Hd_Id);
            if (header == null)
            {
                throw new KeyNotFoundException("Hd_Id");
            }

            //edit customer
            cust.Hd_Id = item.Hd_Id;
            cust.CompanyName = item.CompanyName;
            cust.Email = item.Email;
            cust.FullName = item.FullName;
            cust.Phone = item.Phone;
            _db.Entry(cust).State = EntityState.Modified;
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new DbUpdateConcurrencyException(ex.Message);
            }


            return true;
        }

        public async Task<bool> Remove(int id)
        {
            var cust = await _db.Customers.FirstOrDefaultAsync(c => c.Id == id);
            if (cust == null) throw new KeyNotFoundException("id");

            //remove asp.net identity user
            var usr = await AspContext.Users.FirstAsync(u => u.Id == cust.Asp_Id);
            if (usr != null)
            {
                AspContext.Users.Remove(usr);

                try
                {
                    await AspContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    throw new DbUpdateConcurrencyException(ex.Message);
                }
            }

            _db.Customers.Remove(cust);
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new DbUpdateConcurrencyException(ex.Message);
            }

            return true;
        }
    }
}