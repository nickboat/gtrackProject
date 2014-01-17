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
    public class FixOrderController : ApiController
    {
        private readonly IFixOrderRepository _repository;

        public FixOrderController(IFixOrderRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }
            _repository = repository;
        }

        // GET api/FixOrder
        public IQueryable<FixOrders> Get()
        {
            return _repository.GetAll();
        }

        // GET api/FixOrder/5
        [HttpGet]
        [ResponseType(typeof(FixOrders))]
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                var fix = await _repository.Get(id);
                return Ok(fix);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // POST api/FixOrder
        [HttpPost]
        [ResponseType(typeof(FixOrders))]
        public async Task<IHttpActionResult> Post([FromBody]FixOrders value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var fix = await _repository.Add(value);
                return Ok(fix);
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

        // PUT api/FixOrder/5
        [HttpPut]
        public async Task<IHttpActionResult> Put(int id, [FromBody]FixOrders value)
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

        // DELETE api/FixOrder/5
        [HttpDelete]
        [ResponseType(typeof(FixOrders))]
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
