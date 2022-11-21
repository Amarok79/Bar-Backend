// Copyright (c) 2022, Olaf Kober <olaf.kober@outlook.com>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bar.Domain;
using Microsoft.AspNetCore.Mvc;


namespace Bar.Backend.Controllers;


[ApiController, Route("api/substances")]
public sealed class SubstanceController : ControllerBase
{
    private readonly ISubstanceRepository mRepository;


    public SubstanceController(ISubstanceRepository repository)
    {
        mRepository = repository;
    }


    [HttpGet]
    public async Task<ActionResult<IList<SubstanceDto>>> GetAll()
    {
        var items = await mRepository.GetAllAsync();

        var dto = items.Select(x => x.ToDto()).OrderBy(x => x.Name);

        return Ok(dto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SubstanceDto>> GetSingle([FromRoute] String id)
    {
        var item = await mRepository.GetOrDefaultAsync(id);

        if (item is null)
        {
            return NotFound();
        }

        return Ok(item.ToDto());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSingle([FromRoute] String id)
    {
        await mRepository.DeleteAsync(id);

        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<SubstanceDto>> CreateOrUpdateSingle(
        [FromRoute] String id,
        [FromBody] SubstanceDto dto
    )
    {
        dto.Id = id;

        await mRepository.AddOrUpdateAsync(dto.ToEntity());

        var item = await mRepository.GetOrDefaultAsync(id);

        return Ok(item!.ToDto());
    }
}
