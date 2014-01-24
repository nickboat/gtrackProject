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
    /// OrderController - CRUD Order By admin, cs.
    /// </summary>
    [Authorize(Roles = "admin, cs")]
    public class OrderController : ApiController
    {
        private readonly IOrderRepository _repository;

        /// <summary>
        /// Call repository
        /// </summary>
        /// <param name="repository"> The <see cref="IOrderRepository"/>.</param>
        /// <exception cref="ArgumentNullException">repository isNull</exception>
        public OrderController(IOrderRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }
            _repository = repository;
        }

        // GET api/Order
        /// <summary>
        /// Gets All Orders.
        /// </summary>
        /// <returns>Orders</returns>
        [Authorize]
        public IQueryable<Order> Get()
        {
            return _repository.GetAll();
        }

        // GET api/Order/5
        /// <summary>
        /// Get a Order.
        /// </summary>
        /// <param name="id">id *int*</param>
        /// <returns>Order</returns>
        [Authorize]
        [HttpGet]
        [ResponseType(typeof(Order))]
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

        // POST api/Order
        /// <summary>
        /// Post a Order
        /// </summary>
        /// <param name="value">The <see cref="Order"/>.</param>
        /// <returns>Order</returns>
        [HttpPost]
        [ResponseType(typeof(Order))]
        public async Task<IHttpActionResult> Post([FromBody]Order value)
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

        // PUT api/Order/5
        /// <summary>
        /// Put a Order
        /// </summary>
        /// <param name="id">id *int*</param>
        /// <param name="value">The <see cref="Order"/>.</param>
        /// <returns>HTTP Status</returns>
        [HttpPut]
        public async Task<IHttpActionResult> Put(int id, [FromBody]Order value)
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

        // DELETE api/Order/5
        /// <summary>
        /// Delete a Order
        /// </summary>
        /// <param name="id">id *int*</param>
        /// <returns>HTTP Status</returns>
        [HttpDelete]
        [ResponseType(typeof(Order))]
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
