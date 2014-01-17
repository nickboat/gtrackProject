﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using gtrackProject.Models.dbContext;
using gtrackProject.Models.order;
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

        public IQueryable<FixOrders> GetAll()
        {
            return _db.FixOrders;
        }

        public async Task<FixOrders> Get(int id)
        {
            return await IdExist(id);
        }

        public async Task<FixOrders> Add(FixOrders item)
        {
            var fix = new FixOrders
            {
                CreateBy = await EmpExist(item.CreateBy),
                CreateDate = item.CreateDate,
                Status = 1
            };

            if (item.CurrentUser != null) fix.CurrentUser = await EmpExist(item.CurrentUser.Value);
            if (item.HeadInstall != null) fix.HeadInstall = await EmpExist(item.HeadInstall.Value);
            if (item.Comment != null) fix.Comment = item.Comment;
            
            fix = _db.FixOrders.Add(fix);
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

        public async Task<bool> Update(FixOrders item)
        {
            var fix = await IdExist(item.Id);
            fix.Status = await StatusExist(item.Status);

            if (item.CurrentUser != null) fix.CurrentUser = await EmpExist(item.CurrentUser.Value);
            if (item.HeadInstall != null) fix.HeadInstall = await EmpExist(item.HeadInstall.Value);
            if (item.Comment != null) fix.Comment = item.Comment;

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

        private async Task<FixOrders> IdExist(int id)
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
        private async Task<byte> StatusExist(byte id)
        {
            var status = await _db.OrderStatuss.FirstOrDefaultAsync(o => o.Id == id);
            if (status != null) return id;
            throw new ArgumentException("OrderStatus Not Found");
        }
    }
}