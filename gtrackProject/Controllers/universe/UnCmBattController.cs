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
    /// UnCmBattController - CRUD CmBatt's Status By admin Only.
    /// </summary>
    [Authorize(Roles = "admin")]
    public class UnCmBattController : ApiController
    {
        private readonly IUnCmBattRepository _repository;

        /// <summary>
        /// Call repository
        /// </summary>
        /// <param name="repository"> The <see cref="IUnCmBattRepository"/>.</param>
        /// <exception cref="ArgumentNullException">repository isNull</exception>
        public UnCmBattController(IUnCmBattRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }
            _repository = repository;
        }

        // GET api/UnCmBatt
        /// <summary>
        /// Gets All UnCmBatts
        /// </summary>
        /// <returns>UnCmBatts</returns>
        public IQueryable<UnCmBatt> Get()
        {
            return _repository.GetAll();
        }

        // GET api/UnCmBatt/5
        /// <summary>
        /// Get a UnCmBatt
        /// </summary>
        /// <param name="id">id *char*</param>
        /// <returns>UnCmBatt</returns>
        [HttpGet]
        [ResponseType(typeof(UnCmBatt))]
        public async Task<IHttpActionResult> Get(string id)
        {
            try
            {
                var batt = await _repository.Get(id);
                return Ok(batt);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // POST api/UnCmBatt
        /// <summary>
        /// Post a UnCmBatt
        /// </summary>
        /// <param name="value">The <see cref="UnCmBatt"/>.</param>
        /// <returns>UnCmBatt</returns>
        [HttpPost]
        [ResponseType(typeof(UnCmBatt))]
        public async Task<IHttpActionResult> Post([FromBody]UnCmBatt value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var batt = await _repository.Add(value);
                return Ok(batt);
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

        // PUT api/UnCmBatt/5
        /// <summary>
        /// Put a UnCmBatt
        /// </summary>
        /// <param name="id">id *char*</param>
        /// <param name="value">The <see cref="UnCmBatt"/>.</param>
        /// <returns>UnCmBatt</returns>
        [HttpPut]
        public async Task<IHttpActionResult> Put(string id, [FromBody]UnCmBatt value)
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

        // DELETE api/UnCmBatt/5
        /// <summary>
        /// Delete a UnCmBatt
        /// </summary>
        /// <param name="id">id *char*</param>
        /// <returns>HTTP Status</returns>
        [HttpDelete]
        [ResponseType(typeof(UnCmBatt))]
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
