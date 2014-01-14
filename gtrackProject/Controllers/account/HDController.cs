using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using gtrackProject.Models.account;
using gtrackProject.Repositories.account.IRepos;

namespace gtrackProject.Controllers.account
{
    public class HdController : ApiController
    {
        private readonly IHdRepository _repository;

        public HdController(IHdRepository repository)
        {
            if (repository == null)
            {
                throw new  ArgumentException("repository");
            }
            _repository = repository;
        }

        // GET api/HD
        public IQueryable<Hd> Gethds()
        {
            return _repository.GetAll();
        }

        // GET api/HD/5
        [ResponseType(typeof(Hd))]
        public async Task<IHttpActionResult> Gethd(short id)
        {
            try
            {
                var hd = await _repository.Get(id);
                return Ok(hd);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // PUT api/HD/5
        public async Task<IHttpActionResult> Puthd(short id, Hd hd)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hd.Id)
            {
                return BadRequest();
            }
            
            try
            {
                await _repository.Update(hd);
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

        // POST api/HD
        [ResponseType(typeof(Hd))]
        public async Task<IHttpActionResult> Posthd(Hd hd)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var header = await _repository.Add(hd);
                return CreatedAtRoute("DefaultApi", new { id = header.Id }, header);
            }
            catch (DbUpdateException msgDbUpdateException)
            {
                return InternalServerError(msgDbUpdateException);
            }
        }

        // DELETE api/HD/5
        [ResponseType(typeof(Hd))]
        public async Task<IHttpActionResult> Deletehd(short id)
        {
            if (id <= 0)
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

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}