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
    /// <summary>
    /// OrderStatusController - CRUD Order's status By admin Only.
    /// </summary>
    [Authorize(Roles = "admin")]
    public class OrderStatusController : ApiController
    {
        private readonly IOdStatusRepository _repository;

        /// <summary>
        /// Call repository
        /// </summary>
        /// <param name="repository"> The <see cref="IOdStatusRepository"/>.</param>
        /// <exception cref="ArgumentNullException">repository isNull</exception>
        public OrderStatusController(IOdStatusRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }
            _repository = repository;
        }

        // GET api/OrderStatus
        /// <summary>
        /// Gets All OrderStatus
        /// </summary>
        /// <returns>OrderStatus</returns>
        public IQueryable<OrderStatus> Get()
        {
            return _repository.GetAll();
        }

        // GET api/OrderStatus/5
        /// <summary>
        /// Get a OrderStatus
        /// </summary>
        /// <param name="id">id *byte*</param>
        /// <returns>OrderStatus</returns>
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
        /// <summary>
        /// Post a OrderStatus
        /// </summary>
        /// <param name="value">The <see cref="OrderStatus"/>.</param>
        /// <returns>OrderStatus</returns>
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
        /// <summary>
        /// Put a OrderStatus.
        /// </summary>
        /// <param name="id">id *byte*</param>
        /// <param name="value">The <see cref="OrderStatus"/>.</param>
        /// <returns>HTTP Status</returns>
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
        /// <summary>
        /// Delete a OrderStatus
        /// </summary>
        /// <param name="id">id *byte*</param>
        /// <returns>HTTP Status</returns>
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
