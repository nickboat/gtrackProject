using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using gtrackProject.Models.account;
using gtrackProject.Repositories.account.IRepos;

namespace gtrackProject.Controllers.account
{
    /// <summary>
    /// HdController - CRUD Header By cs,admin.
    /// </summary>
    [Authorize(Roles = "cs, admin")]
    public class HdController : ApiController
    {
        private readonly IHdRepository _repository;

        /// <summary>
        /// Call repository
        /// </summary>
        /// <param name="repository"> The <see cref="IHdRepository"/>.</param>
        /// <exception cref="ArgumentNullException">repository isNull</exception>
        public HdController(IHdRepository repository)
        {
            if (repository == null)
            {
                throw new  ArgumentException("repository");
            }
            _repository = repository;
        }

        // GET api/HD
        /// <summary>
        /// Gets All headers. *Queryable*
        /// </summary>
        /// <returns>Hd</returns>
        [Authorize]
        public IQueryable<Hd> Gethds()
        {
            return _repository.GetAll();
        }

        // GET api/HD/5
        /// <summary>
        /// Gets a header.
        /// </summary>
        /// <param name="id">id *short*</param>
        /// <returns>Hd</returns>
        [Authorize]
        [HttpGet]
        [ResponseType(typeof(Hd))]
        public async Task<IHttpActionResult> Gethd(short id)
        {
            try
            {
                var hd = await _repository.Get(id);
                return Ok(hd);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // PUT api/HD/5
        /// <summary>
        /// Put a header
        /// </summary>
        /// <param name="id">id *short*</param>
        /// <param name="hd">The <see cref="Hd"/>.</param>
        /// <returns>HTTP Status</returns>
        [HttpPut]
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
            
            try
            {
                await _repository.Update(hd);
            }
            catch (DbUpdateConcurrencyException msgDbUpdateConcurrencyException)
            {
                return InternalServerError(msgDbUpdateConcurrencyException);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/HD
        /// <summary>
        /// Post a header
        /// </summary>
        /// <param name="hd">The <see cref="Hd"/>.</param>
        /// <returns>Hd</returns>
        [HttpPost]
        [ResponseType(typeof(Hd))]
        public async Task<IHttpActionResult> Posthd(Hd hd)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var header = await _repository.Add(hd);
                return CreatedAtRoute("DefaultApi", new { id = header.Id }, header);
            }
            catch (DbUpdateException msgDbUpdateException)
            {
                return InternalServerError(msgDbUpdateException);
            }
        }

        // DELETE api/HD/5
        /// <summary>
        /// Delete a header
        /// </summary>
        /// <param name="id">id *short*</param>
        /// <returns>HTTP Status</returns>
        [HttpDelete]
        [ResponseType(typeof(Hd))]
        public async Task<IHttpActionResult> Deletehd(short id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            try
            {
                await _repository.Remove(id);
            }
            catch (DbUpdateConcurrencyException msgDbUpdateConcurrencyException)
            {
                return InternalServerError(msgDbUpdateConcurrencyException);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}