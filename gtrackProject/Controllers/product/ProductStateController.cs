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
    /// ProductStateController - CRUD GPS's StatusType By admin Only.
    /// ** test complete **
    /// </summary>
    [Authorize(Roles = "admin")]
    public class ProductStateController : ApiController
    {
        private readonly IProductStateRepository _repository;

        /// <summary>
        /// Call repository
        /// </summary>
        /// <param name="repository"> The <see cref="IProductStateRepository"/>.</param>
        /// <exception cref="ArgumentNullException">repository isNull</exception>
        public ProductStateController(IProductStateRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }
            _repository = repository;
        }

        // GET api/ProductState
        /// <summary>
        /// Gets All StatusTypes
        /// </summary>
        /// <returns>ProductProductState</returns>
        [Queryable]
        public IQueryable<GpsState> Get()
        {
            return _repository.GetAll();
        }

        // GET api/ProductState/5
        /// <summary>
        /// Get a StatusType.
        /// </summary>
        /// <param name="id">id *byte*</param>
        /// <returns>ProductProductState</returns>
        [HttpGet]
        [ResponseType(typeof(GpsState))]
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

        // POST api/ProductState
        /// <summary>
        /// Post a StatusType.
        /// </summary>
        /// <param name="value">The <see cref="GpsState"/>.</param>
        /// <returns>ProductProductState</returns>
        [HttpPost]
        [ResponseType(typeof(GpsState))]
        public async Task<IHttpActionResult> Post([FromBody]GpsState value)
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

        // PUT api/ProductState/5
        /// <summary>
        /// Put a StatusType.
        /// </summary>
        /// <param name="id">id *byte*</param>
        /// <param name="value">The <see cref="GpsState"/>.</param>
        /// <returns>ProductProductState</returns>
        [HttpPut]
        public async Task<IHttpActionResult> Put(byte id, [FromBody]GpsState value)
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

        // DELETE api/ProductState/5
        /// <summary>
        /// Delete a StatusType.
        /// </summary>
        /// <param name="id">id *byte*</param>
        /// <returns>ProductProductState</returns>
        [HttpDelete]
        [ResponseType(typeof(GpsState))]
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
