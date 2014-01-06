﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using gtrackProject.Models.product;
using gtrackProject.Repositories.product;

namespace gtrackProject.Controllers.product
{
    public class SimPaymentController : ApiController
    {
        private readonly ISimPaymentRepository _repository;

        public SimPaymentController(ISimPaymentRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }
            _repository = repository;
        }

        // GET api/simpayment
        public IQueryable<SimPaymentType> Get()
        {
            return _repository.GetAll();
        }

        // GET api/simpayment/5
        [HttpGet]
        [ResponseType(typeof(SimPaymentType))]
        public async Task<IHttpActionResult> Get(byte id)
        {
            try
            {
                var payment = await _repository.Get(id);
                return Ok(payment);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // POST api/simpayment
        [HttpPost]
        [ResponseType(typeof(SimPaymentType))]
        public async Task<IHttpActionResult> Post([FromBody]SimPaymentType value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var payment = await _repository.Add(value);
                return Ok(payment);
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

        // PUT api/simpayment/5
        [HttpPut]
        public async Task<IHttpActionResult> Put(byte id, [FromBody]SimPaymentType value)
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

        // DELETE api/simpayment/5
        [HttpDelete]
        [ResponseType(typeof(SimPaymentType))]
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
