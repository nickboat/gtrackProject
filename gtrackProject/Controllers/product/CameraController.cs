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
    /// RoleAdminController - CRUD Product GPS By admin, manu, cs, qc.
    /// </summary>
    [Authorize(Roles = "admin, manu, cs, qc")]
    public class CameraController : ApiController
    {
        private readonly ICameraRepository _repository;

        /// <summary>
        /// Call repository
        /// </summary>
        /// <param name="repository"> The <see cref="ICameraRepository"/>.</param>
        /// <exception cref="ArgumentNullException">repository isNull</exception>
        public CameraController(ICameraRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }
            _repository = repository;
        }

        // GET api/Camera
        /// <summary>
        /// Gets All Cameras
        /// </summary>
        /// <returns>ProductCamera</returns>
        [Authorize]
        public IQueryable<ProductCamera> Get()
        {
            return _repository.GetAll();
        }

        // GET api/Camera/5
        /// <summary>
        /// Get a Camera
        /// </summary>
        /// <param name="id">id *int*</param>
        /// <returns>ProductCamera</returns>
        [Authorize]
        [HttpGet]
        [ResponseType(typeof(ProductCamera))]
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                var cam = await _repository.Get(id);
                return Ok(cam);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // POST api/Camera
        /// <summary>
        /// Post a Camera
        /// </summary>
        /// <param name="value">The <see cref="ProductCamera"/>.</param>
        /// <returns>ProductCamera</returns>
        [HttpPost]
        [ResponseType(typeof(ProductCamera))]
        public async Task<IHttpActionResult> Post([FromBody]ProductCamera value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var cam = await _repository.Add(value);
                return Ok(cam);
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

        // PUT api/Camera/5
        /// <summary>
        /// Put a Camera
        /// </summary>
        /// <param name="id">id *int*</param>
        /// <param name="value">The <see cref="ProductCamera"/>.</param>
        /// <returns>HTTP Status</returns>
        [HttpPut]
        public async Task<IHttpActionResult> Put(int id, [FromBody]ProductCamera value)
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

        // DELETE api/Camera/5
        /// <summary>
        /// Delete a Camera
        /// </summary>
        /// <param name="id">id *int*</param>
        /// <returns>HTTP Status</returns>
        [HttpDelete]
        [Authorize(Roles = "admin, manu")]
        [ResponseType(typeof(ProductCamera))]
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
