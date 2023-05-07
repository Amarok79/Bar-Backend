// Copyright (c) 2023, Olaf Kober <olaf.kober@outlook.com>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bar.Domain;
using Microsoft.AspNetCore.Mvc;


namespace Bar.Backend.Controllers;


[ApiController]
[Route("api/gins")]
public sealed class GinController : ControllerBase
{
    private readonly IGinRepository mRepository;


    public GinController(
        IGinRepository repository
    )
    {
        mRepository = repository;
    }


    [HttpGet]
    public async Task<ActionResult<IList<GinDto>>> GetAll(
        [FromQuery] Boolean includeDrafts = false
    )
    {
        var items = await mRepository.GetAllAsync(includeDrafts);

        var dto = items.Select(x => x.ToDto()).OrderBy(x => x.Name);

        return Ok(dto);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GinDto>> GetSingle(
        [FromRoute] Guid id
    )
    {
        var item = await mRepository.GetOrDefaultAsync(id);

        if (item is null)
        {
            return NotFound();
        }

        return Ok(item.ToDto());
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteSingle(
        [FromRoute] Guid id
    )
    {
        await mRepository.DeleteAsync(id);

        return NoContent();
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<GinDto>> CreateOrUpdateSingle(
        [FromRoute] Guid id,
        [FromBody] GinDto dto
    )
    {
        dto.Id = id;

        await mRepository.AddOrUpdateAsync(dto.ToEntity());

        var item = await mRepository.GetOrDefaultAsync(id);

        return Ok(item!.ToDto());
    }
}
