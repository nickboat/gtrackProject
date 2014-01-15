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
    public class UnCmGpsController : ApiController
    {
        private readonly IUnCmGpsRepository _repository;

        public UnCmGpsController(IUnCmGpsRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }
            _repository = repository;
        }

        // GET api/UnCmGps
        public IQueryable<UnCmGps> Get()
        {
            return _repository.GetAll();
        }

        // GET api/UnCmGps/5
        [HttpGet]
        [ResponseType(typeof(UnCmGps))]
        public async Task<IHttpActionResult> Get(string id)
        {
            try
            {
                var gps = await _repository.Get(id);
                return Ok(gps);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // POST api/UnCmGps
        [HttpPost]
        [ResponseType(typeof(UnCmGps))]
        public async Task<IHttpActionResult> Post([FromBody]UnCmGps value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var gps = await _repository.Add(value);
                return Ok(gps);
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

        // PUT api/UnCmGps/5
        [HttpPut]
        public async Task<IHttpActionResult> Put(string id, [FromBody]UnCmGps value)
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

        // DELETE api/UnCmGps/5
        [HttpDelete]
        [ResponseType(typeof(UnCmGps))]
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
