using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using gtrackProject.Models.driver;
using gtrackProject.Repositories.driver.IRepos;

namespace gtrackProject.Controllers.driver
{

    /// <summary>
    /// DriverCateController - CRUD Driver's category By Admin Only.
    /// ** test complete **
    /// </summary>
    [Authorize(Roles = "admin")]
    public class DriverCateController : ApiController
    {
        private readonly IDriverCateRepository _repository;

        /// <summary>
        /// Call repository
        /// </summary>
        /// <param name="repository"> The <see cref="IDriverCateRepository"/>.</param>
        /// <exception cref="ArgumentNullException">repository isNull</exception>
        public DriverCateController(IDriverCateRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }
            _repository = repository;
        }

        // GET api/DriverCate
        /// <summary>
        /// Gets All categories. *Queryable*
        /// </summary>
        /// <returns>DriverCategory</returns>
        [Queryable]
        public IQueryable<DriverCategory> Get()
        {
            return _repository.GetAll();
        }

        // GET api/DriverCate/5
        /// <summary>
        /// Get a category.
        /// </summary>
        /// <param name="id">id *byte*</param>
        /// <returns>DriverCategory</returns>
        [HttpGet]
        [ResponseType(typeof(DriverCategory))]
        public async Task<IHttpActionResult> Get(byte id)
        {
            try
            {
                var cate = await _repository.Get(id);
                return Ok(cate);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // POST api/DriverCate
        /// <summary>
        /// Post a category.
        /// </summary>
        /// <param name="value">The <see cref="DriverCategory"/>.</param>
        /// <returns>DriverCategory</returns>
        [HttpPost]
        [ResponseType(typeof(DriverCategory))]
        public async Task<IHttpActionResult> Post([FromBody]DriverCategory value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var cate = await _repository.Add(value);
                return Ok(cate);
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

        // PUT api/DriverCate/5
        /// <summary>
        /// Put a category.
        /// </summary>
        /// <param name="id">id *byte*</param>
        /// <param name="value">The <see cref="DriverCategory"/>.</param>
        /// <returns>HTTP Status</returns>
        [HttpPut]
        public async Task<IHttpActionResult> Put(byte id, [FromBody]DriverCategory value)
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

        // DELETE api/DriverCate/5
        /// <summary>
        /// Delete a category.
        /// </summary>
        /// <param name="id">id *byte*</param>
        /// <returns>HTTP Status</returns>
        [HttpDelete]
        [ResponseType(typeof(DriverCategory))]
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
