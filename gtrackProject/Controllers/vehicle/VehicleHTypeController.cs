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
    public class VehicleHTypeController : ApiController
    {
        private readonly IVehicleHeadTypeRepository _repository;

        public VehicleHTypeController(IVehicleHeadTypeRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }
            _repository = repository;
        }

        // GET api/VehicleHType
        public IQueryable<VehicleHeadType> Get()
        {
            return _repository.GetAll();
        }

        // GET api/VehicleHType/5
        [HttpGet]
        [ResponseType(typeof(VehicleHeadType))]
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                var ht = await _repository.Get(id);
                return Ok(ht);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // POST api/VehicleHType
        [HttpPost]
        [ResponseType(typeof(VehicleHeadType))]
        public async Task<IHttpActionResult> Post([FromBody]VehicleHeadType value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var ht = await _repository.Add(value);
                return Ok(ht);
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

        // PUT api/VehicleHType/5
        [HttpPut]
        public async Task<IHttpActionResult> Put(int id, [FromBody]VehicleHeadType value)
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

        // DELETE api/VehicleHType/5
        [HttpDelete]
        [ResponseType(typeof(VehicleHeadType))]
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
