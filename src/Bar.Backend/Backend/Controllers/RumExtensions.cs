// Copyright (c) 2022, Olaf Kober <olaf.kober@outlook.com>

using Bar.Domain;


namespace Bar.Backend.Controllers;


public static class RumExtensions
{
    public static Rum ToEntity(
        this RumDto dto
    )
    {
        dto.Id ??= Guid.NewGuid();

        return new Rum(dto.Id.Value, dto.Name) {
            Teaser = dto.Teaser ?? String.Empty,
            Description = dto.Description ?? String.Empty,
            Images = dto.Images is null ? Array.Empty<Image>() : dto.Images.Select(x => new Image(x)).ToList(),
            IsDraft = dto.IsDraft,
        };
    }

    public static RumDto ToDto(
        this Rum entity
    )
    {
        return new RumDto {
            Id = entity.Id,
            Name = entity.Name,
            Teaser = entity.Teaser,
            Description = entity.Description,
            Images = entity.Images.Select(x => x.FileName).ToList(),
            IsDraft = entity.IsDraft,
        };
    }
}
