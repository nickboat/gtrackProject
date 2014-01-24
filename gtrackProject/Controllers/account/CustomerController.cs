using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using gtrackProject.Models.account;
using gtrackProject.Repositories.account.IRepos;

namespace gtrackProject.Controllers.account
{
    /// <summary>
    /// CustomerController - CRUD Customer User By cs,admin.
    /// </summary>
    [Authorize(Roles = "cs, admin")]
    public class CustomerController : ApiController
    {
        private readonly ICustomerRepository _repository;

        /// <summary>
        /// Call repository
        /// </summary>
        /// <param name="repository"><see cref="ICustomerRepository"/></param>
        /// <exception cref="ArgumentNullException">repository isNull</exception>
        public CustomerController(ICustomerRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }
            _repository = repository;
        }

        /// <summary>
        /// Gets All Customer by header
        /// </summary>
        /// <param name="hdId">header id *int*</param>
        /// <returns>CustomerModel</returns>
        [Route("api/CustomerByHd/{hdId:int}")]
        [HttpGet]
        [ResponseType(typeof(CustomerModel))]
        public IEnumerable<CustomerModel> GetByHd(int hdId)
        {
            return _repository.GetByHd(hdId);
        }

        // GET api/useradmin/(Id)
        /// <summary>
        /// Get Customer *for all user*
        /// </summary>
        /// <param name="id">id *int*</param>
        /// <returns>CustomerModel</returns>
        [Authorize]
        [HttpGet]
        [ResponseType(typeof(CustomerModel))]
        public async Task<IHttpActionResult> GetUser(int id)
        {
            try
            {
                var item = await _repository.Get(id);
                return Ok(item);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // POST api/useradmin
        /// <summary>
        /// Post Customer
        /// </summary>
        /// <param name="postCust">The <see cref="CustomerModel"/>.</param>
        /// <returns>CustomerModel</returns>
        [HttpPost]
        [ResponseType(typeof(CustomerModel))]
        public async Task<IHttpActionResult> PostUser(CustomerModel postCust)
        {
            if (!ModelState.IsValid)
            {
                /*{
                UserName = userIden.UserName,
                FullName = cust.FullName,
                Phone = cust.Phone,
                CompanyName = cust.CompanyName,
                Email = cust.Email
                }*/

                return BadRequest(ModelState);
            }

            try
            {
                postCust = await _repository.Add(postCust);
            }
            catch (DbUpdateException msgDbUpdateException)
            {
                return InternalServerError(msgDbUpdateException);
            }

            return CreatedAtRoute("DefaultApi", new { id = postCust.Id }, postCust);
        }

        // PUT api/useradmin/(Id)
        /// <summary>
        /// Put Customer
        /// </summary>
        /// <param name="id">id *int*</param>
        /// <param name="putCust">The <see cref="CustomerModel"/>.</param>
        /// <returns>HTTP Status</returns>
        [HttpPut]
        public async Task<IHttpActionResult> PutUser(int id, CustomerModel putCust)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != putCust.Id)
            {
                return BadRequest();
            }

            try
            {
                await _repository.Update(putCust);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (DbUpdateException mgsDbUpdateException)
            {
                return InternalServerError(mgsDbUpdateException);
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE api/useradmin/5
        /// <summary>
        /// Delete Customer
        /// </summary>
        /// <param name="id">id *int*</param>
        /// <returns>HTTP Status</returns>
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteRole(int id)
        {
            try
            {
                await _repository.Remove(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (DbUpdateConcurrencyException mgsDbUpdateConcurrencyException)
            {
                return InternalServerError(mgsDbUpdateConcurrencyException);
            }

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
