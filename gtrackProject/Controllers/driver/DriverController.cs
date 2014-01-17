using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using gtrackProject.Models.driver;
using gtrackProject.Repositories.driver.IRepos;

namespace gtrackProject.Controllers.driver
{
    public class DriverController : ApiController
    {
        private readonly IDriverRepository _repository;

        public DriverController(IDriverRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }
            _repository = repository;
        }

        // GET api/Driver
        public IQueryable<Driver> Get()
        {
            return _repository.GetAll();
        }

        // GET api/Driver/5
        [HttpGet]
        [ResponseType(typeof(Driver))]
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                var driver = await _repository.Get(id);
                return Ok(driver);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // POST api/Driver
        [HttpPost]
        [ResponseType(typeof(Driver))]
        public async Task<IHttpActionResult> Post([FromBody]Driver value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var driver = await _repository.Add(value);
                return Ok(driver);
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

        // PUT api/Driver/5
        [HttpPut]
        public async Task<IHttpActionResult> Put(int id, [FromBody]Driver value)
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

        // DELETE api/Driver/5
        [HttpDelete]
        [ResponseType(typeof(Driver))]
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
