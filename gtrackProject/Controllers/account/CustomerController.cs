using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using gtrackProject.Models.account;
using gtrackProject.Repositories.account;

namespace gtrackProject.Controllers.account
{
    //[Authorize(Roles = "cs", Roles = "admin")]
    public class CustomerController : ApiController
    {
        private readonly ICustomerRepository _repository;

        public CustomerController(ICustomerRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }
            _repository = repository;
        }

        [Route("api/CustomerByHd/{hdId:int}")]
        [HttpGet]
        [ResponseType(typeof(CustomerModel))]
        public IEnumerable<CustomerModel> GetByHd(int hdId)
        {
            return _repository.GetByHd(hdId);
        }

        // GET api/useradmin/(Id)
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
