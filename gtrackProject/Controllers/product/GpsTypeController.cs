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
    /// GpsTypeController - CRUD GPS's StatusType By admin Only.
    /// </summary>
    [Authorize(Roles = "admin")]
    public class GpsTypeController : ApiController
    {
        private readonly IGpsTypeRepository _repository;

        /// <summary>
        /// Call repository
        /// </summary>
        /// <param name="repository"> The <see cref="IGpsTypeRepository"/>.</param>
        /// <exception cref="ArgumentNullException">repository isNull</exception>
        public GpsTypeController(IGpsTypeRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }
            _repository = repository;
        }

        // GET api/GpsType
        /// <summary>
        /// Gets All StatusTypes
        /// </summary>
        /// <returns>ProductGpsType</returns>
        public IQueryable<ProductGpsType> Get()
        {
            return _repository.GetAll();
        }

        // GET api/GpsType/5
        /// <summary>
        /// Get a StatusType.
        /// </summary>
        /// <param name="id">id *byte*</param>
        /// <returns>ProductGpsType</returns>
        [HttpGet]
        [ResponseType(typeof(ProductGpsType))]
        public async Task<IHttpActionResult> Get(byte id)
        {
            try
            {
                var type = await _repository.Get(id);
                return Ok(type);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // POST api/GpsType
        /// <summary>
        /// Post a StatusType.
        /// </summary>
        /// <param name="value">The <see cref="ProductGpsType"/>.</param>
        /// <returns>ProductGpsType</returns>
        [HttpPost]
        [ResponseType(typeof(ProductGpsType))]
        public async Task<IHttpActionResult> Post([FromBody]ProductGpsType value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var type = await _repository.Add(value);
                return Ok(type);
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

        // PUT api/GpsType/5
        /// <summary>
        /// Put a StatusType.
        /// </summary>
        /// <param name="id">id *byte*</param>
        /// <param name="value">The <see cref="ProductGpsType"/>.</param>
        /// <returns>ProductGpsType</returns>
        [HttpPut]
        public async Task<IHttpActionResult> Put(byte id, [FromBody]ProductGpsType value)
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

        // DELETE api/GpsType/5
        /// <summary>
        /// Delete a StatusType.
        /// </summary>
        /// <param name="id">id *byte*</param>
        /// <returns>ProductGpsType</returns>
        [HttpDelete]
        [ResponseType(typeof(ProductGpsType))]
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
