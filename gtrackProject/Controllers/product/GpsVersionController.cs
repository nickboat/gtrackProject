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
    /// GpsVersionController - CRUD GPS's Version By admin, manu, cs, qc.
    /// </summary>
    [Authorize(Roles = "admin, manu, cs, qc")]
    public class GpsVersionController : ApiController
    {
        private readonly IGpsVersionRepository _repository;

        /// <summary>
        /// Call repository
        /// </summary>
        /// <param name="repository"> The <see cref="IGpsVersionRepository"/>.</param>
        /// <exception cref="ArgumentNullException">repository isNull</exception>
        public GpsVersionController(IGpsVersionRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }
            _repository = repository;
        }

        // GET api/gpsversion
        /// <summary>
        /// Gets All Versions
        /// </summary>
        /// <returns>ProductGpsVersion</returns>
        public IQueryable<ProductGpsVersion> Get()
        {
            return _repository.GetAll();
        }

        // GET api/gpsversion/5
        /// <summary>
        /// Get a Version
        /// </summary>
        /// <param name="id">id *byte*</param>
        /// <returns>ProductGpsVersion</returns>
        [HttpGet]
        [ResponseType(typeof(ProductGpsVersion))]
        public async Task<IHttpActionResult> Get(byte id)
        {
            try
            {
                var ver = await _repository.Get(id);
                return Ok(ver);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // POST api/gpsversion
        /// <summary>
        /// Post a Version
        /// </summary>
        /// <param name="value">The <see cref="ProductGpsVersion"/>.</param>
        /// <returns>ProductGpsVersion</returns>
        [HttpPost]
        [ResponseType(typeof(ProductGpsVersion))]
        public async Task<IHttpActionResult> Post([FromBody]ProductGpsVersion value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var ver = await _repository.Add(value);
                return Ok(ver);
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

        // PUT api/gpsversion/5
        /// <summary>
        /// Put a Version
        /// </summary>
        /// <param name="id">id *byte*</param>
        /// <param name="value">The <see cref="ProductGpsVersion"/>.</param>
        /// <returns>HTTP Status</returns>
        [HttpPut]
        public async Task<IHttpActionResult> Put(byte id, [FromBody]ProductGpsVersion value)
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

        // DELETE api/gpsversion/5
        /// <summary>
        /// Delete a Version
        /// </summary>
        /// <param name="id">id *byte*</param>
        /// <returns>HTTP Status</returns>
        [HttpDelete]
        [Authorize(Roles = "admin, manu")]
        [ResponseType(typeof(ProductGpsVersion))]
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
