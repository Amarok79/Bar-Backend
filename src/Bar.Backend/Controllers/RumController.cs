// Copyright (c) 2021, Olaf Kober <olaf.kober@outlook.com>

using System;
using System.Collections.Generic;
using System.Linq;
using Bar.Data;
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

        [HttpGet, Route("{id}")]
        public ActionResult<RumDto> GetById([FromRoute] Guid id)
        {
            var entity = mDbContext.Rums.SingleOrDefault(x => x.Id == id);

            if (entity is null)
                return NotFound();

            return Ok(entity.ToDto());
        }

        [HttpPost]
        public ActionResult<RumDbo> Create([FromBody] RumDto dto)
        {
            if (mDbContext.Rums.Any(x => x.Id == dto.Id))
                return Conflict();

            mDbContext.Rums.Add(dto.ToEntity());
            mDbContext.SaveChanges();

            var entity = mDbContext.Rums.Single(x => x.Id == dto.Id);

            return Ok(entity.ToDto());
        }

        [HttpDelete, Route("{id}")]
        public IActionResult DeleteById([FromRoute] Guid id)
        {
            var entity = mDbContext.Rums.SingleOrDefault(x => x.Id == id);

            if (entity is null)
                return NoContent();

            mDbContext.Rums.Remove(entity);
            mDbContext.SaveChanges();

            return NoContent();
        }
    }
}
