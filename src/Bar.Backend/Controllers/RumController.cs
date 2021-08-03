// Copyright (c) 2021, Olaf Kober <olaf.kober@outlook.com>

using System;
using System.Collections.Generic;
using System.Linq;
using Bar.Data;
using Bar.Domain;
using Microsoft.AspNetCore.Mvc;


namespace Bar.Backend.Controllers
{
    [ApiController, Route("api/rums")]
    public sealed class RumController : ControllerBase
    {
        private readonly BarDbContext mDbContext;


        public RumController(BarDbContext dbContext)
        {
            mDbContext = dbContext;
        }


        [HttpGet]
        public ActionResult<IList<RumDto>> GetAll()
        {
            var dto = mDbContext.Rums.Select(x => x.ToDto()).ToList();

            return Ok(dto);
        }


        [HttpPost]
        public ActionResult<Rum> Create([FromBody] RumDto dto)
        {
            if (mDbContext.Rums.Any(x => x.Id == dto.Id))
                return Conflict("Already exists");

            var entity = dto.ToEntity();

            mDbContext.Rums.Add(entity);

            mDbContext.SaveChanges();

            var rum = mDbContext.Rums.Single(x => x.Id == dto.Id);

            return Ok(rum.ToDto());
        }


        [HttpDelete, Route("{id}")]
        public IActionResult DeleteById([FromRoute] Guid id)
        {
            var rum = mDbContext.Rums.SingleOrDefault(x => x.Id == id);

            if (rum is null)
                return NoContent();

            mDbContext.Rums.Remove(rum);

            mDbContext.SaveChanges();

            return NoContent();
        }
    }
}
