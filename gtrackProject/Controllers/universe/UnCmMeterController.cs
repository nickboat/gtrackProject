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
    public class UnCmMeterController : ApiController
    {
        private readonly IUnCmMeterRepository _repository;

        public UnCmMeterController(IUnCmMeterRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }
            _repository = repository;
        }

        // GET api/UnCmMeter
        public IQueryable<UnCmMeter> Get()
        {
            return _repository.GetAll();
        }

        // GET api/UnCmMeter/5
        [HttpGet]
        [ResponseType(typeof(UnCmMeter))]
        public async Task<IHttpActionResult> Get(string id)
        {
            try
            {
                var meter = await _repository.Get(id);
                return Ok(meter);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // POST api/UnCmMeter
        [HttpPost]
        [ResponseType(typeof(UnCmMeter))]
        public async Task<IHttpActionResult> Post([FromBody]UnCmMeter value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var meter = await _repository.Add(value);
                return Ok(meter);
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

        // PUT api/UnCmMeter/5
        [HttpPut]
        public async Task<IHttpActionResult> Put(string id, [FromBody]UnCmMeter value)
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

        // DELETE api/UnCmMeter/5
        [HttpDelete]
        [ResponseType(typeof(UnCmMeter))]
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
