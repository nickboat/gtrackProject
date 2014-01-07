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
    public class CameraController : ApiController
    {
        private readonly ICameraRepository _repository;

        public CameraController(ICameraRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }
            _repository = repository;
        }

        // GET api/Camera
        public IQueryable<ProductCamera> Get()
        {
            return _repository.GetAll();
        }

        // GET api/Camera/5
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
        [HttpDelete]
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
