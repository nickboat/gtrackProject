using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using gtrackProject.Models.vehicle;
using gtrackProject.Repositories.vehicle.IRepos;

namespace gtrackProject.Controllers.vehicle
{
    /// <summary>
    /// LpTypeController - CRUD Vehicle's LicensePlate By admin, cs.
    /// ** test complete **
    /// </summary>
    [Authorize(Roles = "admin, cs")]
    public class LpTypeController : ApiController
    {
        private readonly ILpTypeRepository _repository;

        /// <summary>
        /// Call repository
        /// </summary>
        /// <param name="repository"> The <see cref="ILpTypeRepository"/>.</param>
        /// <exception cref="ArgumentNullException">repository isNull</exception>
        public LpTypeController(ILpTypeRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }
            _repository = repository;
        }


        // GET api/lptype
        /// <summary>
        /// Gets All LpTypes
        /// </summary>
        /// <returns>LpTypes</returns>
        [Queryable]
        public IQueryable<LpType> Get()
        {
            return _repository.GetAll();
        }

        // GET api/lptype/5
        /// <summary>
        /// Get a LpType
        /// </summary>
        /// <param name="id">id *byte*</param>
        /// <returns>LpType</returns>
        [HttpGet]
        [ResponseType(typeof(LpType))]
        public async Task<IHttpActionResult> Get(byte id)
        {
            try
            {
                var type = await _repository.Get(id);
                return Ok(type);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // POST api/lptype
        /// <summary>
        /// Post a LpType
        /// </summary>
        /// <param name="value">The <see cref="LpType"/>.</param>
        /// <returns>LpType</returns>
        [HttpPost]
        [ResponseType(typeof(LpType))]
        public async Task<IHttpActionResult> Post([FromBody]LpType value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var type = await _repository.Add(value);
                return Ok(type);
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

        // PUT api/lptype/5
        /// <summary>
        /// Put a LpType
        /// </summary>
        /// <param name="id">id *byte*</param>
        /// <param name="value">The <see cref="LpType"/>.</param>
        /// <returns>HTTP Status</returns>
        [HttpPut]
        public async Task<IHttpActionResult> Put(byte id, [FromBody]LpType value)
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

        // DELETE api/lptype/5
        /// <summary>
        /// Delete a LpType
        /// </summary>
        /// <param name="id">id *byte*</param>
        /// <returns>HTTP Status</returns>
        [HttpDelete]
        [ResponseType(typeof(LpType))]
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
