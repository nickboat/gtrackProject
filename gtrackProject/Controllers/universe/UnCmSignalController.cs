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
    /// UnCmSignalController - CRUD CmSignal's Status By admin Only.
    /// ** test complete **
    /// </summary>
    [Authorize(Roles = "admin")]
    public class UnCmSignalController : ApiController
    {
        private readonly IUnCmSignalRepository _repository;

        /// <summary>
        /// Call repository
        /// </summary>
        /// <param name="repository"> The <see cref="IUnCmSignalRepository"/>.</param>
        /// <exception cref="ArgumentNullException">repository isNull</exception>
        public UnCmSignalController(IUnCmSignalRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }
            _repository = repository;
        }

        // GET api/UnCmSignal
        /// <summary>
        /// Gets All UnCmSignal
        /// </summary>
        /// <returns>UnCmSignal</returns>
        [Queryable]
        public IQueryable<UnCmSignal> Get()
        {
            return _repository.GetAll();
        }

        // GET api/UnCmSignal/5
        /// <summary>
        /// Get a UnCmSignal
        /// </summary>
        /// <param name="id">id *char*</param>
        /// <returns>UnCmSignal</returns>
        [HttpGet]
        [ResponseType(typeof(UnCmSignal))]
        public async Task<IHttpActionResult> Get(string id)
        {
            try
            {
                var signal = await _repository.Get(id);
                return Ok(signal);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // POST api/UnCmSignal
        /// <summary>
        /// Post a UnCmSignal
        /// </summary>
        /// <param name="value">The <see cref="UnCmSignal"/>.</param>
        /// <returns>UnCmSignal</returns>
        [HttpPost]
        [ResponseType(typeof(UnCmSignal))]
        public async Task<IHttpActionResult> Post([FromBody]UnCmSignal value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var signal = await _repository.Add(value);
                return Ok(signal);
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

        // PUT api/UnCmSignal/5
        /// <summary>
        /// Put a UnCmSignal
        /// </summary>
        /// <param name="id">id *char*</param>
        /// <param name="value">The <see cref="UnCmSignal"/>.</param>
        /// <returns>HTTP Stauts</returns>
        [HttpPut]
        public async Task<IHttpActionResult> Put(string id, [FromBody]UnCmSignal value)
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

        // DELETE api/UnCmSignal/5
        /// <summary>
        /// Delete a UnCmSignal
        /// </summary>
        /// <param name="id">id *char*</param>
        /// <returns>HTTP Stauts</returns>
        [HttpDelete]
        [ResponseType(typeof(UnCmSignal))]
        public async Task<IHttpActionResult> Delete(string id)
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
