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
    public class VehicleOgnController : ApiController
    {
        private readonly IVehicleOganizeRepository _repository;

        public VehicleOgnController(IVehicleOganizeRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }
            _repository = repository;
        }

        // GET api/VehicleOgn
        public IQueryable<VehicleOganize> Get()
        {
            return _repository.GetAll();
        }

        // GET api/VehicleOgn/5
        [HttpGet]
        [ResponseType(typeof(VehicleOganize))]
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                var pv = await _repository.Get(id);
                return Ok(pv);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // POST api/VehicleOgn
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
                var pv = await _repository.Add(value);
                return Ok(pv);
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
        [HttpPut]
        public async Task<IHttpActionResult> Put(int id, [FromBody]VehicleOganize value)
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
