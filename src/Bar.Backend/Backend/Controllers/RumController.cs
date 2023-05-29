// Copyright (c) 2023, Olaf Kober <olaf.kober@outlook.com>

using Bar.Domain;
using Microsoft.AspNetCore.Mvc;


namespace Bar.Backend.Controllers;


[ApiController]
[Route("api/rums")]
public sealed class RumController : ControllerBase
{
    private readonly IRumRepository mRepository;


    public RumController(
        IRumRepository repository
    )
    {
        mRepository = repository;
    }


    [HttpGet]
    public async Task<ActionResult<IList<RumDto>>> GetAll(
        [FromQuery] Boolean includeDrafts = false
    )
    {
        var items = await mRepository.GetAllAsync(includeDrafts);

        var dto = items.Select(x => x.ToDto()).OrderBy(x => x.Name);

        return Ok(dto);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RumDto>> GetSingle(
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
    public async Task<ActionResult<RumDto>> CreateOrUpdateSingle(
        [FromRoute] Guid id,
        [FromBody] RumDto dto
    )
    {
        dto.Id = id;

        await mRepository.AddOrUpdateAsync(dto.ToEntity());

        var item = await mRepository.GetOrDefaultAsync(id);

        return Ok(item!.ToDto());
    }

    [HttpPost]
    public async Task<ActionResult<RumDto>> CreateSingle(
        [FromBody] RumDto dto
    )
    {
        var entity = dto.ToEntity();

        await mRepository.AddOrUpdateAsync(entity);

        var item = await mRepository.GetOrDefaultAsync(entity.Id);

        return CreatedAtAction(nameof(GetSingle), new { id = entity.Id }, item);
    }
}
