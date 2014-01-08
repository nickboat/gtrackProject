﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using gtrackProject.Models.vehicle;
using gtrackProject.Repositories.vehicle.IRepos;

namespace gtrackProject.Controllers.vehicle
{
    public class VehicleColorController : ApiController
    {
        private readonly IVehicleColorRepository _repository;

        public VehicleColorController(IVehicleColorRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }
            _repository = repository;
        }

        // GET api/VehicleColor
        public IQueryable<VehicleColor> Get()
        {
            return _repository.GetAll();
        }

        // GET api/VehicleColor/5
        [HttpGet]
        [ResponseType(typeof(VehicleColor))]
        public async Task<IHttpActionResult> Get(byte id)
        {
            try
            {
                var color = await _repository.Get(id);
                return Ok(color);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // POST api/VehicleColor
        [HttpPost]
        [ResponseType(typeof(VehicleColor))]
        public async Task<IHttpActionResult> Post([FromBody]VehicleColor value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var color = await _repository.Add(value);
                return Ok(color);
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

        // PUT api/VehicleColor/5
        //[HttpPut]
        public async Task<IHttpActionResult> Put(byte id, [FromBody]VehicleColor value)
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

        // DELETE api/VehicleColor/5
        //[HttpDelete]
        //[ResponseType(typeof(VehicleColor))]
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
