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
    /// FixOrderController - CRUD Fix Order By admin, cs, install and qc.
    /// </summary>
    [Authorize(Roles = "admin, cs, install, qc")]
    public class FixOrderController : ApiController
    {
        private readonly IFixOrderRepository _repository;

        /// <summary>
        /// Call repository
        /// </summary>
        /// <param name="repository"> The <see cref="IFixOrderRepository"/>.</param>
        /// <exception cref="ArgumentNullException">repository isNull</exception>
        public FixOrderController(IFixOrderRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }
            _repository = repository;
        }

        // GET api/FixOrder
        /// <summary>
        /// Gets All FixOrders. *Queryable*
        /// </summary>
        /// <returns>FixOrders</returns>
        [Authorize]
        [Queryable]
        public IQueryable<FixOrder> Get()
        {
            return _repository.GetAll();
        }

        // GET api/FixOrder/5
        /// <summary>
        /// Get a FixOrder
        /// </summary>
        /// <param name="id">id int**</param>
        /// <returns>FixOrder</returns>
        [Authorize]
        [HttpGet]
        [ResponseType(typeof(FixOrder))]
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
        /// <summary>
        /// Post a FixOrder
        /// </summary>
        /// <param name="value">The <see cref="FixOrder"/>.</param>
        /// <returns>FixOrder</returns>
        [HttpPost]
        [ResponseType(typeof(FixOrder))]
        public async Task<IHttpActionResult> Post([FromBody]FixOrder value)
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
        /// <summary>
        /// Put a FixOrder
        /// </summary>
        /// <param name="id">id *int*</param>
        /// <param name="value">The <see cref="FixOrder"/>.</param>
        /// <returns>HTTP Status</returns>
        [HttpPut]
        public async Task<IHttpActionResult> Put(int id, [FromBody]FixOrder value)
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
        /// <summary>
        /// Delete a FixOrder
        /// </summary>
        /// <param name="id">id *int*</param>
        /// <returns>HTTP Status</returns>
        [HttpDelete]
        [ResponseType(typeof(FixOrder))]
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
