// Copyright (c) 2021, Olaf Kober <olaf.kober@outlook.com>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bar.Domain;
using Microsoft.AspNetCore.Mvc;


namespace Bar.Backend.Controllers
{
    [ApiController, Route("api/rums")]
    public sealed class RumController : ControllerBase
    {
        private readonly IRumRepository mRepository;


        public RumController(IRumRepository repository)
        {
            mRepository = repository;
        }


        [HttpGet]
        public async Task<ActionResult<IList<RumDto>>> GetAll()
        {
            var items = await mRepository.GetAllAsync();
            var dto   = items.Select(x => x.ToDto());

            return Ok(dto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RumDto>> GetSingle([FromRoute] Guid id)
        {
            var item = await mRepository.GetOrDefaultAsync(id);

            if (item is null)
                return NotFound();

            return Ok(item.ToDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSingle([FromRoute] Guid id)
        {
            await mRepository.DeleteAsync(id);

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RumDto>> CreateOrUpdateSingle([FromRoute] Guid id, [FromBody] RumDto dto)
        {
            dto.Id = id;

            await mRepository.AddOrUpdateAsync(dto.ToEntity());

            var item = await mRepository.GetOrDefaultAsync(id);

            return Ok(item!.ToDto());
        }
    }
}
