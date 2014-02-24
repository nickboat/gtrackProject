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
    /// RoleAdminController - CRUD Product GPS By admin, manu, cs, install, qc.
    /// </summary>
    [Authorize(Roles = "admin, manu, cs, install, qc")]
    public class ProductGpsController : ApiController
    {
        private readonly IProductGpsRepository _repository;

        /// <summary>
        /// Call repository
        /// </summary>
        /// <param name="repository"> The <see cref="IProductGpsRepository"/>.</param>
        /// <exception cref="ArgumentNullException">repository isNull</exception>
        public ProductGpsController(IProductGpsRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }
            _repository = repository;
        }

        // GET api/productgps
        /// <summary>
        /// Gets All ProductGps
        /// </summary>
        /// <returns>ProductGps</returns>
        [Authorize]
        [Queryable]
        public IQueryable<Gps> Get()
        {
            return _repository.GetAll();
        }

        // GET api/productgps/5
        /// <summary>
        /// Get a ProductGps
        /// </summary>
        /// <param name="id">id *int*</param>
        /// <returns>ProductGps</returns>
        [Authorize]
        [HttpGet]
        [ResponseType(typeof(Gps))]
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                var product = await _repository.Get(id);
                return Ok(product);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // POST api/productgps
        /// <summary>
        /// Post a ProductGps
        /// </summary>
        /// <param name="value">The <see cref="Gps"/>.</param>
        /// <returns>ProductGps</returns>
        [HttpPost]
        [ResponseType(typeof(Gps))]
        public async Task<IHttpActionResult> Post([FromBody]Gps value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var product = await _repository.Add(value);
                return Ok(product);
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

        // PUT api/productgps/5
        /// <summary>
        /// Put a ProductGps
        /// </summary>
        /// <param name="id">id *int*</param>
        /// <param name="value">The <see cref="Gps"/>.</param>
        /// <returns>HTTP Status</returns>
        [HttpPut]
        public async Task<IHttpActionResult> Put(int id, [FromBody]Gps value)
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

        // DELETE api/productgps/5
        /// <summary>
        /// Delete a ProductGps
        /// </summary>
        /// <param name="id">id *int*</param>
        /// <returns>HTTP Status</returns>
        [HttpDelete]
        [Authorize(Roles = "admin, manu")]
        [ResponseType(typeof(Gps))]
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
