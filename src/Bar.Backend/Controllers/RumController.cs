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
        public ActionResult<IList<String>> GetAll()
        {
            IQueryable<String>? dto = mDbContext.Rums.Select(x => x.Name);

            return Ok(dto);
        }
    }
}
