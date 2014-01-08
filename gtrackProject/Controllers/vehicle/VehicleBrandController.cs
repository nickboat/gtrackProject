using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using gtrackProject.Models.vehicle;
using gtrackProject.Repositories.vehicle.IRepos;

namespace gtrackProject.Controllers.vehicle
{
    public class VehicleBrandController : ApiController
    {
        private readonly IVehicleBrandRepository _repository;

        public VehicleBrandController(IVehicleBrandRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }
            _repository = repository;
        }

        // GET api/Vehiclebrand
        public IQueryable<VehicleBrand> Get()
        {
            return _repository.GetAll();
        }

        // GET api/Vehiclebrand/5
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
