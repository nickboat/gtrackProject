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
    /// RoleAdminController - CRUD Employee User By Admin Only.
    /// </summary>
    [Authorize(Roles = "admin")]
    public class EmployeeAdminController : ApiController
    {
        private readonly IEmployeeAdminRepository _repository;

        /// <summary>
        /// Call repository
        /// </summary>
        /// <param name="repository"> The <see cref="IEmployeeAdminRepository"/>.</param>
        /// <exception cref="ArgumentNullException">repository isNull</exception>
        public EmployeeAdminController(IEmployeeAdminRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }
            _repository = repository;
        }

        // GET api/useradmin
        /// <summary>
        /// Gets Employee
        /// </summary>
        /// <returns>EmployeeAdminModel</returns>
        public IEnumerable<EmployeeAdminModel> GetUsers()
        {
            return _repository.GetAll();
        }

        // GET api/useradmin/(Id)
        /// <summary>
        /// Get Employee
        /// </summary>
        /// <param name="id">id *int*</param>
        /// <returns>EmployeeAdminModel</returns>
        [HttpGet]
        [ResponseType(typeof(EmployeeAdminModel))]
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
        /// Post Employee
        /// </summary>
        /// <param name="postEmp">The <see cref="EmployeeAdminModel"/>.</param>
        /// <returns>EmployeeAdminModel</returns>
        [HttpPost]
        [ResponseType(typeof(EmployeeAdminModel))]
        public async Task<IHttpActionResult> PostUser(EmployeeAdminModel postEmp)
        {
            if (!ModelState.IsValid)
            {
                /*{
                "UserName" : "testboat",
                "FullName" : "chalothorn",
                "Phone" : "0849101166",
                "Gender" : "m",
                "BirthDate" : "1988-5-31",
                "Roles":["??","??"]
                }*/

                return BadRequest(ModelState);
            }

            try
            {
                postEmp = await _repository.Add(postEmp);
            }
            catch (DbUpdateException msgDbUpdateException)
            {
                return InternalServerError(msgDbUpdateException);
            }
            catch (ArgumentNullException mgsArgumentNullException)
            {
                return BadRequest(mgsArgumentNullException.Message);
            }
            catch (ArgumentException mgsException)
            {
                return BadRequest(mgsException.Message);
            }

            return CreatedAtRoute("DefaultApi", new { id = postEmp.Id }, postEmp);
        }

        // PUT api/useradmin/(Id)
        /// <summary>
        /// Put Employee
        /// </summary>
        /// <param name="id">id *int*</param>
        /// <param name="putEmp">The <see cref="EmployeeAdminModel"/>.</param>
        /// <returns>HTTP Status</returns>
        [HttpPut]
        public async Task<IHttpActionResult> PutUser(int id, EmployeeAdminModel putEmp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != putEmp.Id)
            {
                return BadRequest();
            }

            try
            {
                await _repository.Update(putEmp);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (DbUpdateException mgsDbUpdateException)
            {
                return InternalServerError(mgsDbUpdateException);
            }
            catch (ArgumentNullException mgsArgumentNullException)
            {
                return BadRequest(mgsArgumentNullException.Message);
            }
            catch (ArgumentException mgsException)
            {
                return BadRequest(mgsException.Message);
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE api/useradmin/5
        /// <summary>
        /// Delete Employee
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
