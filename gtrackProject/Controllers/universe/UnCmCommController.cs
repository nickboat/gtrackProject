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
    /// UnCmCommController - CRUD CmComm's Status By admin Only.
    /// ** test complete **
    /// </summary>
    [Authorize(Roles = "admin")]
    public class UnCmCommController : ApiController
    {
        private readonly IUnCmCommRepository _repository;

        /// <summary>
        /// Call repository
        /// </summary>
        /// <param name="repository"> The <see cref="IUnCmCommRepository"/>.</param>
        /// <exception cref="ArgumentNullException">repository isNull</exception>
        public UnCmCommController(IUnCmCommRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }
            _repository = repository;
        }

        // GET api/UnCmComm
        /// <summary>
        /// Gets All UnCmComms
        /// </summary>
        /// <returns>UnCmComms</returns>
        [Queryable]
        public IQueryable<UnCmComm> Get()
        {
            return _repository.GetAll();
        }

        // GET api/UnCmComm/5
        /// <summary>
        /// Get a UnCmComm
        /// </summary>
        /// <param name="id">id *char*</param>
        /// <returns>UnCmComm</returns>
        [HttpGet]
        [ResponseType(typeof(UnCmComm))]
        public async Task<IHttpActionResult> Get(string id)
        {
            try
            {
                var comm = await _repository.Get(id);
                return Ok(comm);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // POST api/UnCmComm
        /// <summary>
        /// Post a UnCmComm
        /// </summary>
        /// <param name="value">The <see cref="UnCmComm"/>.</param>
        /// <returns>UnCmComm</returns>
        [HttpPost]
        [ResponseType(typeof(UnCmComm))]
        public async Task<IHttpActionResult> Post([FromBody]UnCmComm value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var comm = await _repository.Add(value);
                return Ok(comm);
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

        // PUT api/UnCmComm/5
        /// <summary>
        /// Put a UnCmComm
        /// </summary>
        /// <param name="id">id *char*</param>
        /// <param name="value">The <see cref="UnCmComm"/>.</param>
        /// <returns>HTTP Status</returns>
        [HttpPut]
        public async Task<IHttpActionResult> Put(string id, [FromBody]UnCmComm value)
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

        // DELETE api/UnCmComm/5
        /// <summary>
        /// Delete a UnCmComm
        /// </summary>
        /// <param name="id">id *char*</param>
        /// <returns>HTTP Status</returns>
        [HttpDelete]
        [ResponseType(typeof(UnCmComm))]
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
