// Copyright (c) 2021, Olaf Kober <olaf.kober@outlook.com>

using System;
using System.Linq;
using Bar.Domain;


namespace Bar.Backend.Controllers
{
    public static class GinExtensions
    {
        public static Gin ToEntity(this GinDto dto)
        {
            dto.Id ??= Guid.NewGuid();

            return new Gin(dto.Id.Value, dto.Name) {
                Teaser = dto.Teaser ?? String.Empty,
                Images = dto.Images is null ? Array.Empty<Image>() : dto.Images.Select(x => new Image(x)).ToList(),
            };
        }

        public static GinDto ToDto(this Gin entity)
        {
            return new GinDto {
                Id     = entity.Id,
                Name   = entity.Name,
                Teaser = entity.Teaser,
                Images = entity.Images.Select(x => x.FileName).ToList(),
            };
        }
    }
}
