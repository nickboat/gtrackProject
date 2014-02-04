using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using gtrackProject.Models.product;
using gtrackProject.Repositories.product.IRepos;

namespace gtrackProject.Controllers.product
{
    /// <summary>
    /// SimPaymentController - CRUD Sim's Payment By admin, cs.
    /// ** test complete **
    /// </summary>
    [Authorize(Roles = "admin, cs")]
    public class SimPaymentController : ApiController
    {
        private readonly ISimPaymentRepository _repository;

        /// <summary>
        /// Call repository
        /// </summary>
        /// <param name="repository"> The <see cref="ISimPaymentRepository"/>.</param>
        /// <exception cref="ArgumentNullException">repository isNull</exception>
        public SimPaymentController(ISimPaymentRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }
            _repository = repository;
        }

        // GET api/simpayment
        /// <summary>
        /// Gets All PaymentTypes
        /// </summary>
        /// <returns>SimPaymentType</returns>
        [Queryable]
        public IQueryable<SimPaymentType> Get()
        {
            return _repository.GetAll();
        }

        // GET api/simpayment/5
        /// <summary>
        /// Get a PaymentType
        /// </summary>
        /// <param name="id">id *byte*</param>
        /// <returns>SimPaymentType</returns>
        [HttpGet]
        [ResponseType(typeof(SimPaymentType))]
        public async Task<IHttpActionResult> Get(byte id)
        {
            try
            {
                var payment = await _repository.Get(id);
                return Ok(payment);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // POST api/simpayment
        /// <summary>
        /// Post a PaymentType
        /// </summary>
        /// <param name="value">The <see cref="SimPaymentType"/>.</param>
        /// <returns>SimPaymentType</returns>
        [HttpPost]
        [ResponseType(typeof(SimPaymentType))]
        public async Task<IHttpActionResult> Post([FromBody]SimPaymentType value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var payment = await _repository.Add(value);
                return Ok(payment);
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

        // PUT api/simpayment/5
        /// <summary>
        /// Put a PaymentType
        /// </summary>
        /// <param name="id">id *byte*</param>
        /// <param name="value">The <see cref="SimPaymentType"/>.</param>
        /// <returns>HTTP Status</returns>
        [HttpPut]
        public async Task<IHttpActionResult> Put(byte id, [FromBody]SimPaymentType value)
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

        // DELETE api/simpayment/5
        /// <summary>
        /// Delete a PaymentType
        /// </summary>
        /// <param name="id">id *byte*</param>
        /// <returns>HTTP Status</returns>
        [HttpDelete]
        [ResponseType(typeof(SimPaymentType))]
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
