﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using gtrackProject.Models.universe;
using gtrackProject.Repositories.universe.IRepos;

namespace gtrackProject.Controllers.universe
{
    public class UnCmEngineController : ApiController
    {
        private readonly IUnCmEngineRepository _repository;

        public UnCmEngineController(IUnCmEngineRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }
            _repository = repository;
        }

        // GET api/UnCmEngine
        public IQueryable<UnCmEngine> Get()
        {
            return _repository.GetAll();
        }

        // GET api/UnCmEngine/5
        [HttpGet]
        [ResponseType(typeof(UnCmEngine))]
        public async Task<IHttpActionResult> Get(string id)
        {
            try
            {
                var engine = await _repository.Get(id);
                return Ok(engine);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // POST api/UnCmEngine
        [HttpPost]
        [ResponseType(typeof(UnCmEngine))]
        public async Task<IHttpActionResult> Post([FromBody]UnCmEngine value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var engine = await _repository.Add(value);
                return Ok(engine);
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

        // PUT api/UnCmEngine/5
        [HttpPut]
        public async Task<IHttpActionResult> Put(string id, [FromBody]UnCmEngine value)
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

        // DELETE api/UnCmEngine/5
        [HttpDelete]
        [ResponseType(typeof(UnCmEngine))]
        public async Task<IHttpActionResult> Delete(string id)
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