using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using gtrackProject.Models.universe;
using gtrackProject.Repositories.universe.IRepos;

namespace gtrackProject.Controllers.universe
{
    /// <summary>
    /// UnDisplayController - CRUD Universe's DisplayStatus By admin Only.
    /// ** test complete **
    /// </summary>
    [Authorize(Roles = "admin")]
    public class UnDisplayController : ApiController
    {
        private readonly IUnDisplayRepository _repository;

        /// <summary>
        /// Call repository
        /// </summary>
        /// <param name="repository"> The <see cref="IUnDisplayRepository"/>.</param>
        /// <exception cref="ArgumentNullException">repository isNull</exception>
        public UnDisplayController(IUnDisplayRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }
            _repository = repository;
        }

        // GET api/UnDisplay
        /// <summary>
        /// Gets All UnDisplayStatus
        /// </summary>
        /// <returns>UnDisplayStatus</returns>
        public IQueryable<UnDisplayStatus> Get()
        {
            return _repository.GetAll();
        }

        // GET api/UnDisplay/5
        /// <summary>
        /// Get a UnDisplayStatus
        /// </summary>
        /// <param name="id">id *byte*</param>
        /// <returns>UnDisplayStatus</returns>
        [HttpGet]
        [ResponseType(typeof(UnDisplayStatus))]
        public async Task<IHttpActionResult> Get(byte id)
        {
            try
            {
                var dis = await _repository.Get(id);
                return Ok(dis);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // POST api/UnDisplay
        /// <summary>
        /// Post a UnDisplayStatus
        /// </summary>
        /// <param name="value">The <see cref="UnDisplayStatus"/>.</param>
        /// <returns>UnDisplayStatus</returns>
        [HttpPost]
        [ResponseType(typeof(UnDisplayStatus))]
        public async Task<IHttpActionResult> Post([FromBody]UnDisplayStatus value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var dis = await _repository.Add(value);
                return Ok(dis);
            }
            catch (ArgumentException msgArgumentException)
            {
                return BadRequest(msgArgumentException.Message);
            }
            catch (DbUpdateException msgDbUpdateException)
            {
                return InternalServerError(msgDbUpdateException);
            }
        }

        // PUT api/UnDisplay/5
        /// <summary>
        /// Put a UnDisplayStatus
        /// </summary>
        /// <param name="id">id *byte*</param>
        /// <param name="value">The <see cref="UnDisplayStatus"/>.</param>
        /// <returns>HTTP Status</returns>
        [HttpPut]
        public async Task<IHttpActionResult> Put(byte id, [FromBody]UnDisplayStatus value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != value.Id)
            {
                return BadRequest();
            }

            try
            {
                await _repository.Update(value);
            }
            catch (DbUpdateConcurrencyException msgDbUpdateConcurrencyException)
            {
                return InternalServerError(msgDbUpdateConcurrencyException);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (ArgumentException msgArgumentException)
            {
                return BadRequest(msgArgumentException.Message);
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE api/UnDisplay/5
        /// <summary>
        /// Delete a UnDisplayStatus
        /// </summary>
        /// <param name="id">id *byte*</param>
        /// <returns>HTTP Status</returns>
        [HttpDelete]
        [ResponseType(typeof(UnDisplayStatus))]
        public async Task<IHttpActionResult> Delete(byte id)
        {
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
