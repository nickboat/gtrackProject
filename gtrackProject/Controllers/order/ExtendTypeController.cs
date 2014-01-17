using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using gtrackProject.Models.order;
using gtrackProject.Repositories.order.IRepos;

namespace gtrackProject.Controllers.order
{
    public class ExtendTypeController : ApiController
    {
        private readonly IOdExtendTypeRepository _repository;

        public ExtendTypeController(IOdExtendTypeRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }
            _repository = repository;
        }

        // GET api/ExtendType
        public IQueryable<OrderExtendType> Get()
        {
            return _repository.GetAll();
        }

        // GET api/ExtendType/5
        [HttpGet]
        [ResponseType(typeof(OrderExtendType))]
        public async Task<IHttpActionResult> Get(byte id)
        {
            try
            {
                var ext = await _repository.Get(id);
                return Ok(ext);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // POST api/ExtendType
        [HttpPost]
        [ResponseType(typeof(OrderExtendType))]
        public async Task<IHttpActionResult> Post([FromBody]OrderExtendType value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var ext = await _repository.Add(value);
                return Ok(ext);
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

        // PUT api/ExtendType/5
        [HttpPut]
        public async Task<IHttpActionResult> Put(byte id, [FromBody]OrderExtendType value)
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

        // DELETE api/ExtendType/5
        [HttpDelete]
        [ResponseType(typeof(OrderExtendType))]
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
