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
    /// VehicleOgnController - CRUD Vehicle's Oganize By admin, cs.
    /// </summary>
    [Authorize(Roles = "admin, cs")]
    public class VehicleOgnController : ApiController
    {
        private readonly IVehicleOganizeRepository _repository;

        /// <summary>
        /// Call repository
        /// </summary>
        /// <param name="repository"> The <see cref="IVehicleOganizeRepository"/>.</param>
        /// <exception cref="ArgumentNullException">repository isNull</exception>
        public VehicleOgnController(IVehicleOganizeRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }
            _repository = repository;
        }

        // GET api/VehicleOgn
        /// <summary>
        /// Gets All VehicleOganizes
        /// </summary>
        /// <returns>VehicleOganize</returns>
        public IQueryable<VehicleOganize> Get()
        {
            return _repository.GetAll();
        }

        // GET api/VehicleOgn/5
        /// <summary>
        /// Get a VehicleOganize
        /// </summary>
        /// <param name="id">id *byte*</param>
        /// <returns>VehicleOganize</returns>
        [HttpGet]
        [ResponseType(typeof(VehicleOganize))]
        public async Task<IHttpActionResult> Get(byte id)
        {
            try
            {
                var ogn = await _repository.Get(id);
                return Ok(ogn);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // POST api/VehicleOgn
        /// <summary>
        /// Post a VehicleOganize
        /// </summary>
        /// <param name="value">The <see cref="VehicleOganize"/>.</param>
        /// <returns>VehicleOganize</returns>
        [HttpPost]
        [ResponseType(typeof(VehicleOganize))]
        public async Task<IHttpActionResult> Post([FromBody]VehicleOganize value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var ogn = await _repository.Add(value);
                return Ok(ogn);
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

        // PUT api/VehicleOgn/5
        /// <summary>
        /// Put a VehicleOganize
        /// </summary>
        /// <param name="id">id *byte*</param>
        /// <param name="value">The <see cref="VehicleOganize"/>.</param>
        /// <returns>HTTP Status</returns>
        [HttpPut]
        public async Task<IHttpActionResult> Put(byte id, [FromBody]VehicleOganize value)
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

        // DELETE api/VehicleOgn/5
        /// <summary>
        /// Delete a VehicleOganize
        /// </summary>
        /// <param name="id">id *byte*</param>
        /// <returns>HTTP STatus</returns>
        [HttpDelete]
        [ResponseType(typeof(VehicleOganize))]
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
