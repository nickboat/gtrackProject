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
    /// GpsMemoryController - CRUD GPS Memory store By admin Only.
    /// </summary>
    [Authorize(Roles = "admin")]
    public class GpsMemoryController : ApiController
    {
        private readonly IGpsMemoryRepository _repository;

        /// <summary>
        /// Call repository
        /// </summary>
        /// <param name="repository"> The <see cref="IGpsMemoryRepository"/>.</param>
        /// <exception cref="ArgumentNullException">repository isNull</exception>
        public GpsMemoryController(IGpsMemoryRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }
            _repository = repository;
        }

        // GET api/GpsMemory
        /// <summary>
        /// Gets All StatusTypes
        /// </summary>
        /// <returns>ProductGpsMemoryStatus</returns>
        public IQueryable<ProductGpsMemoryStatus> Get()
        {
            return _repository.GetAll();
        }

        // GET api/GpsMemory/5
        /// <summary>
        /// Get a StatusType.
        /// </summary>
        /// <param name="id">id *byte*</param>
        /// <returns>ProductGpsMemoryStatus</returns>
        [HttpGet]
        [ResponseType(typeof(ProductGpsMemoryStatus))]
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

        // POST api/GpsMemory
        /// <summary>
        /// Post a StatusType.
        /// </summary>
        /// <param name="value">The <see cref="ProductGpsMemoryStatus"/>.</param>
        /// <returns>ProductGpsMemoryStatus</returns>
        [HttpPost]
        [ResponseType(typeof(ProductGpsMemoryStatus))]
        public async Task<IHttpActionResult> Post([FromBody]ProductGpsMemoryStatus value)
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

        // PUT api/GpsMemory/5
        /// <summary>
        /// Put a StatusType
        /// </summary>
        /// <param name="id">id *byte*</param>
        /// <param name="value">The <see cref="ProductGpsMemoryStatus"/>.</param>
        /// <returns>ProductGpsMemoryStatus</returns>
        [HttpPut]
        public async Task<IHttpActionResult> Put(byte id, [FromBody]ProductGpsMemoryStatus value)
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

        // DELETE api/GpsMemory/5
        /// <summary>
        /// Delete a StatusType.
        /// </summary>
        /// <param name="id">id *byte*</param>
        /// <returns>ProductGpsMemoryStatus</returns>
        [HttpDelete]
        [ResponseType(typeof(ProductGpsMemoryStatus))]
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
