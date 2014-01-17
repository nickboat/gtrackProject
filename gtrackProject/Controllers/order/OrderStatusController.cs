using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using gtrackProject.Models.order;
using gtrackProject.Repositories.order.IRepos;

namespace gtrackProject.Controllers.order
{
    public class OrderStatusController : ApiController
    {
        private readonly IOdStatusRepository _repository;

        public OrderStatusController(IOdStatusRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }
            _repository = repository;
        }

        // GET api/OrderStatus
        public IQueryable<OrderStatus> Get()
        {
            return _repository.GetAll();
        }

        // GET api/OrderStatus/5
        [HttpGet]
        [ResponseType(typeof(OrderStatus))]
        public async Task<IHttpActionResult> Get(byte id)
        {
            try
            {
                var status = await _repository.Get(id);
                return Ok(status);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // POST api/OrderStatus
        [HttpPost]
        [ResponseType(typeof(OrderStatus))]
        public async Task<IHttpActionResult> Post([FromBody]OrderStatus value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var status = await _repository.Add(value);
                return Ok(status);
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

        // PUT api/OrderStatus/5
        [HttpPut]
        public async Task<IHttpActionResult> Put(byte id, [FromBody]OrderStatus value)
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

        // DELETE api/OrderStatus/5
        [HttpDelete]
        [ResponseType(typeof(OrderStatus))]
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
