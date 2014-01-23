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
    /// VehicleController - CRUD Vehicle By admin, cs.
    /// </summary>
    [Authorize(Roles = "admin, cs")]
    //todo manage role for this class
    public class VehicleController : ApiController
    {
        private readonly IVehicleRepository _repository;

        /// <summary>
        /// Call repository
        /// </summary>
        /// <param name="repository"> The <see cref="IVehicleRepository"/>.</param>
        /// <exception cref="ArgumentNullException">repository isNull</exception>
        public VehicleController(IVehicleRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }
            _repository = repository;
        }

        // GET api/Vehicle
        /// <summary>
        /// Gets All Vehicles
        /// </summary>
        /// <returns>Vehicles</returns>
        public IQueryable<Vehicle> Get()
        {
            return _repository.GetAll();
        }

        // GET api/Vehicle/5
        /// <summary>
        /// Get a Vehicle
        /// </summary>
        /// <param name="id">Vehicle</param>
        /// <returns>Vehicle</returns>
        [HttpGet]
        [ResponseType(typeof(Vehicle))]
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                var veh = await _repository.Get(id);
                return Ok(veh);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // POST api/Vehicle
        /// <summary>
        /// Post a Vehicle
        /// </summary>
        /// <param name="value">The <see cref="Vehicle"/>.</param>
        /// <returns>Vehicle</returns>
        [HttpPost]
        [ResponseType(typeof(Vehicle))]
        public async Task<IHttpActionResult> Post([FromBody]Vehicle value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var veh = await _repository.Add(value);
                return Ok(veh);
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

        // POST api/Vehicle?hdId=0&q=0
        /// <summary>
        /// Post a Vehicle *FromUrl*
        /// </summary>
        /// <param name="hdId">id *short*</param>
        /// <param name="q">quantity *int*</param>
        /// <returns>HTTP Status</returns>
        /// <exception cref="ArgumentNullException">maxIdCar isNull</exception>
        [HttpPost]
        public async Task<IHttpActionResult> Post([FromUri]short hdId, [FromUri]int q) //q = quantity
        {
            if (hdId <= 0 || q <= 0)
                return BadRequest();

            try
            {
                var maxIdCar = _repository.GetAll().Where(c => c.HdId == hdId).Max(c => c.IdCar);
                if (maxIdCar == null) throw new ArgumentNullException("maxIdCar");

                for (var i = 0; i < q; i++)
                {
                    var max = Convert.ToInt32(maxIdCar) + 1;

                    var newVeh = new Vehicle
                    {
                        IdCar = max.ToString("D6"),
                        HdId = hdId
                    };

                    await _repository.Add(newVeh);
                }

                return Ok();
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

        // PUT api/Vehicle/5
        /// <summary>
        /// Put a Vehicle
        /// </summary>
        /// <param name="id">id *int*</param>
        /// <param name="value">The <see cref="Vehicle"/>.</param>
        /// <returns>HTTP Status</returns>
        [HttpPut]
        public async Task<IHttpActionResult> Put(int id, [FromBody]Vehicle value)
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

        // DELETE api/Vehicle/5
        /// <summary>
        /// Delete a Vehicle
        /// </summary>
        /// <param name="id">id *int*</param>
        /// <returns>HTTP Status</returns>
        [HttpDelete]
        [ResponseType(typeof(Vehicle))]
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
        }
    }
}
