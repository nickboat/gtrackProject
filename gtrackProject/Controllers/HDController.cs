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
    public class HDController : ApiController
    {
        private gtrackDbContext db = new gtrackDbContext();

        // GET api/HD
        public IQueryable<hd> Gethds()
        {
            return db.hds;
        }

        // GET api/HD/5
        [ResponseType(typeof(hd))]
        public async Task<IHttpActionResult> Gethd(short id)
        {
            hd hd = await db.hds.FindAsync(id);
            if (hd == null)
            {
                return NotFound();
            }

            return Ok(hd);
        }

        // PUT api/HD/5
        public async Task<IHttpActionResult> Puthd(short id, hd hd)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hd.Id)
            {
                return BadRequest();
            }

            db.Entry(hd).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!hdExists(id))
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
        [ResponseType(typeof(hd))]
        public async Task<IHttpActionResult> Posthd(hd hd)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.hds.Add(hd);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = hd.Id }, hd);
        }

        // DELETE api/HD/5
        [ResponseType(typeof(hd))]
        public async Task<IHttpActionResult> Deletehd(short id)
        {
            hd hd = await db.hds.FindAsync(id);
            if (hd == null)
            {
                return NotFound();
            }

            db.hds.Remove(hd);
            await db.SaveChangesAsync();

            return Ok(hd);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool hdExists(short id)
        {
            return db.hds.Count(e => e.Id == id) > 0;
        }
    }
}