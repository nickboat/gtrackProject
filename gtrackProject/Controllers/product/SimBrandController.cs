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
    /// SimBrandController - CRUD Sim's Brand By admin, cs.
    /// ** test complete **
    /// </summary>
    [Authorize(Roles = "admin, cs")]
    public class SimBrandController : ApiController
    {
        private readonly ISimNetworkRepository _repository;

        /// <summary>
        /// Call repository
        /// </summary>
        /// <param name="repository"> The <see cref="ISimNetworkRepository"/>.</param>
        /// <exception cref="ArgumentNullException">repository isNull</exception>
        public SimBrandController(ISimNetworkRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }
            _repository = repository;
        }

        // GET api/simbrand
        /// <summary>
        /// Gets All SimBrands
        /// </summary>
        /// <returns>SimBrands</returns>
        [Queryable]
        public IQueryable<SimNetwork> Get()
        {
            return _repository.GetAll();
        }

        // GET api/simbrand/5
        /// <summary>
        /// Get a SimBrand
        /// </summary>
        /// <param name="id">id *byte*</param>
        /// <returns>SimBrand</returns>
        [HttpGet]
        [ResponseType(typeof(SimNetwork))]
        public async Task<IHttpActionResult> Get(byte id)
        {
            try
            {
                var brand = await _repository.Get(id);
                return Ok(brand);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // POST api/simbrand
        /// <summary>
        /// Post a SimBrand
        /// </summary>
        /// <param name="value">The <see cref="SimNetwork"/>.</param>
        /// <returns>SimBrand</returns>
        [HttpPost]
        [ResponseType(typeof(SimNetwork))]
        public async Task<IHttpActionResult> Post([FromBody]SimNetwork value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var brand = await _repository.Add(value);
                return Ok(brand);
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

        // PUT api/simbrand/5
        /// <summary>
        /// Put a SimBrand
        /// </summary>
        /// <param name="id">id *byte*</param>
        /// <param name="value">The <see cref="SimNetwork"/>.</param>
        /// <returns>HTTP Status</returns>
        [HttpPut]
        public async Task<IHttpActionResult> Put(byte id, [FromBody]SimNetwork value)
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

        // DELETE api/simbrand/5
        /// <summary>
        /// Delete a SimBrand
        /// </summary>
        /// <param name="id">id *byte*</param>
        /// <returns>HTTP Status</returns>
        [HttpDelete]
        [ResponseType(typeof(SimNetwork))]
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
