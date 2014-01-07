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
    public class SimBrandController : ApiController
    {
        private readonly ISimBrandRepository _repository;

        public SimBrandController(ISimBrandRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }
            _repository = repository;
        }

        // GET api/simbrand
        public IQueryable<SimBrand> Get()
        {
            return _repository.GetAll();
        }

        // GET api/simbrand/5
        [HttpGet]
        [ResponseType(typeof(SimBrand))]
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
        [HttpPost]
        [ResponseType(typeof(SimBrand))]
        public async Task<IHttpActionResult> Post([FromBody]SimBrand value)
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
        [HttpPut]
        public async Task<IHttpActionResult> Put(byte id, [FromBody]SimBrand value)
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
        [HttpDelete]
        [ResponseType(typeof(SimBrand))]
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
