using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using gtrackProject.Models.account;
using gtrackProject.Models.dbContext;
using gtrackProject.Repositories.account.IRepos;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace gtrackProject.Repositories.account
{
    public class HdReository : IHdRepository
    {
        private GtrackDbContext _db { get; set; }
        private IdentityDbContext AspContext { get; set; }

        public HdReository()
        {
            _db = new GtrackDbContext();
        }

        public IQueryable<Hd> GetAll()
        {
            return _db.Hds;
        }

        public async Task<Hd> Get(int id)
        {
            return await IdExist(id);
        }

        public async Task<Hd> Add(Hd item)
        {
            /*{
                "Name":"??",
                "Code":"??",
                "HdIdUpline" : "??"
            }*/
            if (item.HdIdUpline != null && item.HdIdUpline != 0)
            {
                var up = await IdExist(item.HdIdUpline.Value);
            }

            var randResult = await RandHd();//random hd.value 3 char before add
            var newHd = new Hd
            {
                Name = item.Name,
                Code = item.Code,
                HdIdUpline = item.HdIdUpline,
                Value = randResult,
                TableName = "_" + randResult//use hd.value to create tableName "_xxx"
            };
            
            //create table backup in mysql ***
            //createBackUp(newHd.TableName);

            newHd = _db.Hds.Add(newHd);
            try
            {
                await _db.SaveChangesAsync();
                return newHd;
            }
            catch (DbUpdateConcurrencyException exception)
            {
                throw new DbUpdateConcurrencyException(exception.Message);
            }
            
        }

        public async Task<bool> Update(Hd item)
        {
            /*{
                "Name":"??",
                "Code":"??",
                "HdIdUpline" : "??"
            }*/

            //check IdExists
            var header = await IdExist(item.Id);
            
            //check Upline invalid
            if (item.HdIdUpline != null && item.HdIdUpline != 0)
            {
                var up = await IdExist(item.HdIdUpline.Value);
            }

            header.Name = item.Name;
            header.Code = item.Code;
            header.HdIdUpline = item.HdIdUpline;

            _db.Entry(header).State = EntityState.Modified;
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
            var header = await IdExist(id);
            var customers = _db.Customers.Where(c => c.Hd_Id == id);

            if (customers.Count() != 0)
            {
                //delete this hd's customers
                foreach (var customer in customers)
                {
                    var c = customer;
                    var usr = await AspContext.Users.FirstAsync(u => u.Id == c.Asp_Id);
                    AspContext.Users.Remove(usr);
                }

                try
                {
                    await AspContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    throw new DbUpdateConcurrencyException(ex.Message);
                }
            }

            //delete this hd
            _db.Hds.Remove(header);
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

        private async Task<Hd> IdExist(int id)
        {
            var header = await _db.Hds.FirstOrDefaultAsync(h => h.Id == id);
            if (header != null) return header;
            throw new KeyNotFoundException("id");
        }

        private async Task<string> RandHd()
        {
            var random = new Random();
            while (true)
            {
                const string chars = "abcdefghijklmnopgrstuvwxyz123456789";//without Zero
                //var random = new Random();
                var result = new string(Enumerable.Repeat(chars, 3).Select(s => s[random.Next(s.Length)]).ToArray());

                var header = await _db.Hds.FirstOrDefaultAsync(h => h.Value == result);
                if (header == null)
                {
                    return result;
                }
            }
        }
    }
}