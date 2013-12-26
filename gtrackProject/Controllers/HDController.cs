using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using gtrackProject.Models;

namespace gtrackProject.Controllers
{
    public class HdController : ApiController
    {
        private readonly GtrackDbContext _db = new GtrackDbContext();

        // GET api/HD
        public IQueryable<Hd> Gethds()
        {
            return _db.Hds;
        }

        // GET api/HD/5
        [ResponseType(typeof(Hd))]
        public async Task<IHttpActionResult> Gethd(short id)
        {
            var hd = await _db.Hds.FindAsync(id);
            if (hd == null)
            {
                return NotFound();
            }

            return Ok(hd);
        }

        // PUT api/HD/5
        public async Task<IHttpActionResult> Puthd(short id, Hd hd)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hd.Id)
            {
                return BadRequest();
            }

            _db.Entry(hd).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HdExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/HD
        [ResponseType(typeof(Hd))]
        public async Task<IHttpActionResult> Posthd(Hd hd)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.Hds.Add(hd);
            await _db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = hd.Id }, hd);
        }

        // DELETE api/HD/5
        [ResponseType(typeof(Hd))]
        public async Task<IHttpActionResult> Deletehd(short id)
        {
            var hd = await _db.Hds.FindAsync(id);
            if (hd == null)
            {
                return NotFound();
            }

            _db.Hds.Remove(hd);
            await _db.SaveChangesAsync();

            return Ok(hd);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HdExists(short id)
        {
            return _db.Hds.Count(e => e.Id == id) > 0;
        }
    }
}