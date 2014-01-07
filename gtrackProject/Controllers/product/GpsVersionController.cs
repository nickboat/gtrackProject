using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using gtrackProject.Models.product;
using gtrackProject.Repositories.product;
using gtrackProject.Repositories.product.IRepos;

namespace gtrackProject.Controllers.product
{
    public class GpsVersionController : ApiController
    {
        private readonly IGpsVersionRepository _repository;

        public GpsVersionController(IGpsVersionRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }
            _repository = repository;
        }

        // GET api/gpsversion
        public IQueryable<ProductGpsVersion> Get()
        {
            return _repository.GetAll();
        }

        // GET api/gpsversion/5
        [HttpGet]
        [ResponseType(typeof(ProductGpsVersion))]
        public async Task<IHttpActionResult> Get(byte id)
        {
            try
            {
                var ver = await _repository.Get(id);
                return Ok(ver);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // POST api/gpsversion
        [HttpPost]
        [ResponseType(typeof(ProductGpsVersion))]
        public async Task<IHttpActionResult> Post([FromBody]ProductGpsVersion value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var ver = await _repository.Add(value);
                return Ok(ver);
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

        // PUT api/gpsversion/5
        [HttpPut]
        public async Task<IHttpActionResult> Put(byte id, [FromBody]ProductGpsVersion value)
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

        // DELETE api/gpsversion/5
        [HttpDelete]
        [ResponseType(typeof(ProductGpsVersion))]
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
