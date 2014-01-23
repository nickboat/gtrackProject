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
    /// UniverseController - RU Universe.
    /// </summary>
    [Authorize]
    public class UniverseController : ApiController
    {
        private readonly IUniverseRepository _repository;

        /// <summary>
        /// Call repository
        /// </summary>
        /// <param name="repository"> The <see cref="IUniverseRepository"/>.</param>
        /// <exception cref="ArgumentNullException">repository isNull</exception>
        public UniverseController(IUniverseRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }
            _repository = repository;
        }

        // GET api/Universe
        /// <summary>
        /// Gets All Universes
        /// </summary>
        /// <returns>Universes</returns>
        public IQueryable<Universe> Get()
        {
            return _repository.GetAll();
        }

        // GET api/Universe/5
        /// <summary>
        /// Get a Universe
        /// </summary>
        /// <param name="id">id *int*</param>
        /// <returns>Universe</returns>
        [HttpGet]
        [ResponseType(typeof(Universe))]
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                var un = await _repository.Get(id);
                return Ok(un);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // POST api/Universe
        /*[HttpPost] //move add universe function to add vehicle function
        [ResponseType(typeof(Universe))] 
        public async Task<IHttpActionResult> Post([FromBody]Universe value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var un = await _repository.Add(value);
                return Ok(un);
            }
            catch (ArgumentException msgArgumentException)
            {
                return BadRequest(msgArgumentException.Message);
            }
            catch (DbUpdateException msgDbUpdateException)
            {
                return InternalServerError(msgDbUpdateException);
            }
        }*/

        // PUT api/Universe/5
        /// <summary>
        /// Put a Universe
        /// </summary>
        /// <param name="id">id *int*</param>
        /// <param name="value">The <see cref="Universe"/>.</param>
        /// <returns></returns>
        /// todo put universe fromurl - possible???
        [Authorize(Roles = "admin, cs, install, server")]
        [HttpPut]
        public async Task<IHttpActionResult> Put(int id, [FromBody]Universe value)
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

        // DELETE api/Universe/5
        /*[HttpDelete] //becuz fk_vehicle : cascade
        [ResponseType(typeof(Universe))]
        public async Task<IHttpActionResult> Delete(int id)
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
        }*/
    }
}
