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
    /// UnCmTempController - CRUD CmTemp's Status By admin Only.
    /// ** test complete **
    /// </summary>
    [Authorize(Roles = "admin")]
    public class UnCmTempController : ApiController
    {
        private readonly IUnCmTempRepository _repository;

        /// <summary>
        /// Call repository
        /// </summary>
        /// <param name="repository"> The <see cref="IUnCmTempRepository"/>.</param>
        /// <exception cref="ArgumentNullException">repository isNull</exception>
        public UnCmTempController(IUnCmTempRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }
            _repository = repository;
        }

        // GET api/UnCmTemp
        /// <summary>
        /// Gets All UnCmTemps
        /// </summary>
        /// <returns>UnCmTemps</returns>
        [Queryable]
        public IQueryable<UnCmTemp> Get()
        {
            return _repository.GetAll();
        }

        // GET api/UnCmTemp/5
        /// <summary>
        /// Get a UnCmTemp
        /// </summary>
        /// <param name="id">id *char*</param>
        /// <returns>UnCmTemp</returns>
        [HttpGet]
        [ResponseType(typeof(UnCmTemp))]
        public async Task<IHttpActionResult> Get(string id)
        {
            try
            {
                var temp = await _repository.Get(id);
                return Ok(temp);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // POST api/UnCmTemp
        /// <summary>
        /// Post a UnCmTemp
        /// </summary>
        /// <param name="value">The <see cref="UnCmTemp"/>.</param>
        /// <returns>UnCmTemp</returns>
        [HttpPost]
        [ResponseType(typeof(UnCmTemp))]
        public async Task<IHttpActionResult> Post([FromBody]UnCmTemp value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var temp = await _repository.Add(value);
                return Ok(temp);
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

        // PUT api/UnCmTemp/5
        /// <summary>
        /// Put a UnCmTemp
        /// </summary>
        /// <param name="id">id *char*</param>
        /// <param name="value">The <see cref="UnCmTemp"/>.</param>
        /// <returns>HTTP Status</returns>
        [HttpPut]
        public async Task<IHttpActionResult> Put(string id, [FromBody]UnCmTemp value)
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

        // DELETE api/UnCmTemp/5
        /// <summary>
        /// Delete a UnCmTemp
        /// </summary>
        /// <param name="id">id *char*</param>
        /// <returns>HTTP Status</returns>
        [HttpDelete]
        [ResponseType(typeof(UnCmTemp))]
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
