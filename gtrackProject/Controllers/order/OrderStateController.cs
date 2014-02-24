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
    /// OrderStateController - CRUD Order's State By admin Only.
    /// ** test complete **
    /// </summary>
    [Authorize(Roles = "admin")]
    public class OrderStateController : ApiController
    {
        private readonly IOdStateRepository _repository;

        /// <summary>
        /// Call repository
        /// </summary>
        /// <param name="repository"> The <see cref="IOdStateRepository"/>.</param>
        /// <exception cref="ArgumentNullException">repository isNull</exception>
        public OrderStateController(IOdStateRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }
            _repository = repository;
        }

        // GET api/OrderState
        /// <summary>
        /// Gets All OrderState
        /// </summary>
        /// <returns>OrderState</returns>
        [Queryable]
        public IQueryable<OrderState> Get()
        {
            return _repository.GetAll();
        }

        // GET api/OrderState/5
        /// <summary>
        /// Get a OrderState
        /// </summary>
        /// <param name="id">id *byte*</param>
        /// <returns>OrderState</returns>
        [HttpGet]
        [ResponseType(typeof(OrderState))]
        public async Task<IHttpActionResult> Get(byte id)
        {
            try
            {
                var state = await _repository.Get(id);
                return Ok(state);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // POST api/OrderState
        /// <summary>
        /// Post a OrderState
        /// </summary>
        /// <param name="value">The <see cref="OrderState"/>.</param>
        /// <returns>OrderState</returns>
        [HttpPost]
        [ResponseType(typeof(OrderState))]
        public async Task<IHttpActionResult> Post([FromBody]OrderState value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var state = await _repository.Add(value);
                return Ok(state);
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

        // PUT api/OrderState/5
        /// <summary>
        /// Put a OrderState.
        /// </summary>
        /// <param name="id">id *byte*</param>
        /// <param name="value">The <see cref="OrderState"/>.</param>
        /// <returns>HTTP State</returns>
        [HttpPut]
        public async Task<IHttpActionResult> Put(byte id, [FromBody]OrderState value)
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

        // DELETE api/OrderState/5
        /// <summary>
        /// Delete a OrderState
        /// </summary>
        /// <param name="id">id *byte*</param>
        /// <returns>HTTP State</returns>
        [HttpDelete]
        [ResponseType(typeof(OrderState))]
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
