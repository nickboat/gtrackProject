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
    /// VehicleBrandController - CRUD Vehicle's Brand By admin, cs.
    /// ** test complete **
    /// </summary>
    [Authorize(Roles = "admin, cs")]
    public class VehicleBrandController : ApiController
    {
        private readonly IVehicleBrandRepository _repository;

        /// <summary>
        /// Call repository
        /// </summary>
        /// <param name="repository"> The <see cref="IVehicleBrandRepository"/>.</param>
        /// <exception cref="ArgumentNullException">repository isNull</exception>
        public VehicleBrandController(IVehicleBrandRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }
            _repository = repository;
        }

        // GET api/Vehiclebrand
        /// <summary>
        /// Gets All VehicleBrands
        /// </summary>
        /// <returns>VehicleBrands</returns>
        public IQueryable<VehicleBrand> Get()
        {
            return _repository.GetAll();
        }

        // GET api/Vehiclebrand/5
        /// <summary>
        /// Get a VehicleBrand
        /// </summary>
        /// <param name="id">id *byte*</param>
        /// <returns>VehicleBrand</returns>
        [HttpGet]
        [ResponseType(typeof(VehicleBrand))]
        public async Task<IHttpActionResult> Get(byte id)
        {
            try
            {
                var brand = await _repository.Get(id);
                return Ok(brand);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // POST api/Vehiclebrand
        /// <summary>
        /// Post a VehicleBrand
        /// </summary>
        /// <param name="value">The <see cref="VehicleBrand"/>.</param>
        /// <returns>VehicleBrand</returns>
        [HttpPost]
        [ResponseType(typeof(VehicleBrand))]
        public async Task<IHttpActionResult> Post([FromBody]VehicleBrand value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var brand = await _repository.Add(value);
                return Ok(brand);
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

        // PUT api/Vehiclebrand/5
        /// <summary>
        /// Put a VehicleBrand
        /// </summary>
        /// <param name="id">id *byte*</param>
        /// <param name="value">The <see cref="VehicleBrand"/>.</param>
        /// <returns>HTTP Stauts</returns>
        [HttpPut]
        public async Task<IHttpActionResult> Put(byte id, [FromBody]VehicleBrand value)
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

        // DELETE api/Vehiclebrand/5
        /// <summary>
        /// Delete a VehicleBrand
        /// </summary>
        /// <param name="id">id *byte*</param>
        /// <returns>HTTP Stauts</returns>
        [HttpDelete]
        [ResponseType(typeof(VehicleBrand))]
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
