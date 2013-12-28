using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using gtrackProject.Models;
using gtrackProject.Models.account;
using gtrackProject.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.Validation;

namespace gtrackProject.Controllers
{
    //[Authorize(Roles = "admin")]
    public class EmployeeAdminController : ApiController
    {
        private readonly IEmployeeAdminRepository _repository;
        
        public EmployeeAdminController(IEmployeeAdminRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }
            this._repository = repository;
        }

        // GET api/useradmin
        public IEnumerable<EmployeeAdminModel> GetUsers()
        {
            return _repository.GetAll();
        }

        // GET api/useradmin/(Id)
        [HttpGet]
        [ResponseType(typeof(EmployeeAdminModel))]
        public async Task<IHttpActionResult> GetUser(int id)
        {
            try
            {
                var item = _repository.Get(id);
                return Ok(item);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // POST api/useradmin
        [HttpPost]
        [ResponseType(typeof(EmployeeAdminModel))]
        public async Task<IHttpActionResult> PostUser(EmployeeAdminModel postEmp)
        {
            if (!ModelState.IsValid)
            {
                /*{
                Id = "?",
                AspId = "?",
                UserName = "?",
                FullName = "?",
                Phone = "?",
                Gender = "?",
                BirthDate = "?",
                "EmployeeRoles":[{"Id":"???","Name":"???"},{"Id":"???","Name":"???"} ...]
                }*/

                return BadRequest(ModelState);
            }

            try
            {
                postEmp = _repository.Add(postEmp);
            }
            catch (DbUpdateException mgsDbUpdateException)
            {
                return InternalServerError(mgsDbUpdateException);
            }
            catch (ArgumentNullException mgsArgumentNullException)
            {
                return BadRequest(mgsArgumentNullException.Message);
            }

            var uri = Url.Link("DefaultApi", new { id = postEmp.Id });
            return Created(uri, postEmp);
        }

        // PUT api/useradmin/(Id)
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
                _repository.Update(putEmp);
            }
            catch (DbUpdateException mgsDbUpdateException)
            {
                return InternalServerError(mgsDbUpdateException);
            }
            catch (ArgumentNullException mgsArgumentNullException)
            {
                return BadRequest(mgsArgumentNullException.Message);
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE api/useradmin/5
        [HttpDelete]
        [ResponseType(typeof(EmployeeAdminModel))]
        public async Task<IHttpActionResult> DeleteRole(int id)
        {
            try
            {
                _repository.Remove(id);
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
    }
}
