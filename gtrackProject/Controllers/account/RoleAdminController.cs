using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using gtrackProject.Repositories.account;
using Microsoft.AspNet.Identity.EntityFramework;

namespace gtrackProject.Controllers.account
{
    //[Authorize(Roles = "admin")]
    public class RoleAdminController : ApiController
    {
        private readonly IRoleAdminRepository _repository;
        
        public RoleAdminController(IRoleAdminRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }
            _repository = repository;
        }

        // GET api/RoleAdmin
        public IQueryable<IdentityRole> GetRoles()
        {
            return _repository.GetAll();
        }

        // GET api/RoleAdmin/(Id)
        [HttpGet]
        [ResponseType(typeof(IdentityRole))]
        public async Task<IHttpActionResult> GetRole(string id)
        {
            try
            {
                var role = await _repository.Get(id);
                return Ok(role);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // POST api/RoleAdmin/
        [HttpPost]
        [ResponseType(typeof(IdentityRole))]
        public async Task<IHttpActionResult> PostRole(IdentityRole rolePost)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var role = await _repository.Add(rolePost);
                return Ok(role);
            }
            catch (DbUpdateException msgDbUpdateException)
            {
                return InternalServerError(msgDbUpdateException);
            }
        }

        // PUT api/RoleAdmin/(Id)
        [HttpPut]
        public async Task<IHttpActionResult> PutRole(string id, IdentityRole rolePut)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rolePut.Id)
            {
                return BadRequest();
            }

            try
            {
                await _repository.Update(rolePut);
            }
            catch (DbUpdateConcurrencyException msgDbUpdateConcurrencyException)
            {
                return InternalServerError(msgDbUpdateConcurrencyException);
            }
            catch (ArgumentException msgArgumentException)
            {
                return BadRequest(msgArgumentException.Message);
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE api/RoleAdmin/(Id)
        [HttpDelete]
        [ResponseType(typeof(IdentityRole))]
        public async Task<IHttpActionResult> DeleteRole(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

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
            //If you look at DeleteAsync with a decompiler you'll see it throws a NotImplementedException, and so does not provide the ability to delete a role!
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}