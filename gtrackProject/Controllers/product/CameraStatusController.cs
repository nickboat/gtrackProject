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
    /// CameraStatusController - CRUD Camera StatusType By admin Only.
    /// </summary>
    [Authorize(Roles = "admin")]
    public class CameraStatusController : ApiController
    {
        private readonly ICameraStatusRepository _repository;

        /// <summary>
        /// Call repository
        /// </summary>
        /// <param name="repository"> The <see cref="ICameraStatusRepository"/>.</param>
        /// <exception cref="ArgumentNullException">repository isNull</exception>
        public CameraStatusController(ICameraStatusRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }
            _repository = repository;
        }

        // GET api/CameraStatus
        /// <summary>
        /// Gets All StatusTypes
        /// </summary>
        /// <returns>ProductCameraStatus</returns>
        public IQueryable<ProductCameraStatus> Get()
        {
            return _repository.GetAll();
        }

        // GET api/CameraStatus/5
        /// <summary>
        /// Get a StatusType.
        /// </summary>
        /// <param name="id">id *string*</param>
        /// <returns>ProductCameraStatus</returns>
        [HttpGet]
        [ResponseType(typeof(ProductCameraStatus))]
        public async Task<IHttpActionResult> Get(string id)
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

        // POST api/CameraStatus
        /// <summary>
        /// Post a StatusType.
        /// </summary>
        /// <param name="value">The <see cref="ProductCameraStatus"/>.</param>
        /// <returns>ProductCameraStatus</returns>
        [HttpPost]
        [ResponseType(typeof(ProductCameraStatus))]
        public async Task<IHttpActionResult> Post([FromBody]ProductCameraStatus value)
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

        // PUT api/CameraStatus/5
        /// <summary>
        /// Put a StatusType
        /// </summary>
        /// <param name="id">id *string*</param>
        /// <param name="value">The <see cref="ProductCameraStatus"/>.</param>
        /// <returns>ProductCameraStatus</returns>
        [HttpPut]
        public async Task<IHttpActionResult> Put(string id, [FromBody]ProductCameraStatus value)
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

        // DELETE api/CameraStatus/5
        /// <summary>
        /// Delete a StatusType.
        /// </summary>
        /// <param name="id">id *string*</param>
        /// <returns>ProductCameraStatus</returns>
        [HttpDelete]
        [ResponseType(typeof(ProductCameraStatus))]
        public async Task<IHttpActionResult> Delete(string id)
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
