using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using gtrackProject.Models.vehicle;
using gtrackProject.Repositories.vehicle.IRepos;

namespace gtrackProject.Controllers.vehicle
{
    /// <summary>
    /// ProvinceController - Read only Provinces.
    /// </summary>
    [Authorize]
    public class ProvinceController : ApiController
    {
        private readonly IProvinceRepository _repository;

        /// <summary>
        /// Call repository
        /// </summary>
        /// <param name="repository"> The <see cref="IProvinceRepository"/>.</param>
        /// <exception cref="ArgumentNullException">repository isNull</exception>
        public ProvinceController(IProvinceRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }
            _repository = repository;
        }

        // GET api/province
        /// <summary>
        /// Gets All Provinces
        /// </summary>
        /// <returns>Provinces</returns>
        public IQueryable<Province> Get()
        {
            return _repository.GetAll();
        }

        // GET api/province/5
        /// <summary>
        /// Get a Province
        /// </summary>
        /// <param name="id">id *byte*</param>
        /// <returns>Province</returns>
        [HttpGet]
        [ResponseType(typeof(Province))]
        public async Task<IHttpActionResult> Get(byte id)
        {
            try
            {
                var pv = await _repository.Get(id);
                return Ok(pv);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // POST api/province
        //[HttpPost]
        //[ResponseType(typeof(Province))]
        //public async Task<IHttpActionResult> Post([FromBody]Province value)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    try
        //    {
        //        var pv = await _repository.Add(value);
        //        return Ok(pv);
        //    }
        //    catch (ArgumentException msgArgumentException)
        //    {
        //        return BadRequest(msgArgumentException.Message);
        //    }
        //    catch (DbUpdateException msgDbUpdateException)
        //    {
        //        return InternalServerError(msgDbUpdateException);
        //    }
        //}

        // PUT api/province/5
        //[HttpPut]
        //public async Task<IHttpActionResult> Put(byte id, [FromBody]Province value)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != value.Id)
        //    {
        //        return BadRequest();
        //    }

        //    try
        //    {
        //        await _repository.Update(value);
        //    }
        //    catch (DbUpdateConcurrencyException msgDbUpdateConcurrencyException)
        //    {
        //        return InternalServerError(msgDbUpdateConcurrencyException);
        //    }
        //    catch (KeyNotFoundException)
        //    {
        //        return NotFound();
        //    }
        //    catch (ArgumentException msgArgumentException)
        //    {
        //        return BadRequest(msgArgumentException.Message);
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        // DELETE api/province/5
        //[HttpDelete]
        //[ResponseType(typeof(Province))]
        //public async Task<IHttpActionResult> Delete(byte id)
        //{
        //    try
        //    {
        //        await _repository.Remove(id);
        //    }
        //    catch (DbUpdateConcurrencyException msgDbUpdateConcurrencyException)
        //    {
        //        return InternalServerError(msgDbUpdateConcurrencyException);
        //    }
        //    catch (KeyNotFoundException)
        //    {
        //        return NotFound();
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}
    }
}
