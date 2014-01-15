﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using gtrackProject.Models.vehicle;
using gtrackProject.Repositories.vehicle.IRepos;

namespace gtrackProject.Controllers.vehicle
{
    public class VehicleTypeController : ApiController
    {
        private readonly IVehicleTypeRepository _repository;

        public VehicleTypeController(IVehicleTypeRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }
            _repository = repository;
        }

        // GET api/Vehicletype
        public IQueryable<VehicleType> Get()
        {
            return _repository.GetAll();
        }

        // GET api/Vehicletype/5
        [HttpGet]
        [ResponseType(typeof(VehicleType))]
        public async Task<IHttpActionResult> Get(byte id)
        {
            try
            {
                var type = await _repository.Get(id);
                return Ok(type);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // POST api/Vehicletype
        [HttpPost]
        [ResponseType(typeof(VehicleType))]
        public async Task<IHttpActionResult> Post([FromBody]VehicleType value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var type = await _repository.Add(value);
                return Ok(type);
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

        // PUT api/Vehicletype/5
        [HttpPut]
        public async Task<IHttpActionResult> Put(byte id, [FromBody]VehicleType value)
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

        // DELETE api/Vehicletype/5
        /*[HttpDelete]
        [ResponseType(typeof(VehicleType))]
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
        }*/
    }
}