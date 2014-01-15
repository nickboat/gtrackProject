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
    public class UnCmSignalController : ApiController
    {
        private readonly IUnCmSignalRepository _repository;

        public UnCmSignalController(IUnCmSignalRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }
            _repository = repository;
        }

        // GET api/UnCmSignal
        public IQueryable<UnCmSignal> Get()
        {
            return _repository.GetAll();
        }

        // GET api/UnCmSignal/5
        [HttpGet]
        [ResponseType(typeof(UnCmSignal))]
        public async Task<IHttpActionResult> Get(string id)
        {
            try
            {
                var signal = await _repository.Get(id);
                return Ok(signal);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // POST api/UnCmSignal
        [HttpPost]
        [ResponseType(typeof(UnCmSignal))]
        public async Task<IHttpActionResult> Post([FromBody]UnCmSignal value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var signal = await _repository.Add(value);
                return Ok(signal);
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

        // PUT api/UnCmSignal/5
        [HttpPut]
        public async Task<IHttpActionResult> Put(string id, [FromBody]UnCmSignal value)
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

        // DELETE api/UnCmSignal/5
        [HttpDelete]
        [ResponseType(typeof(UnCmSignal))]
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
