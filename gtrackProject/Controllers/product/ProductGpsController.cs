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
    public class ProductGpsController : ApiController
    {
        private readonly IProductGpsRepository _repository;

        public ProductGpsController(IProductGpsRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }
            _repository = repository;
        }

        // GET api/productgps
        public IQueryable<ProductGps> Get()
        {
            return _repository.GetAll();
        }

        // GET api/productgps/5
        [HttpGet]
        [ResponseType(typeof(ProductGps))]
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                var product = await _repository.Get(id);
                return Ok(product);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // POST api/productgps
        [HttpPost]
        [ResponseType(typeof(ProductGps))]
        public async Task<IHttpActionResult> Post([FromBody]ProductGps value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var product = await _repository.Add(value);
                return Ok(product);
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

        // PUT api/productgps/5
        [HttpPut]
        public async Task<IHttpActionResult> Put(int id, [FromBody]ProductGps value)
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

        // DELETE api/productgps/5
        [HttpDelete]
        [ResponseType(typeof(ProductGps))]
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
