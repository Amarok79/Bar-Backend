// Copyright (c) 2021, Olaf Kober <olaf.kober@outlook.com>

using System;
using Bar.Data;


namespace Bar.Backend.Controllers
{
    public static class RumExtensions
    {
        public static RumDbo ToEntity(this RumDto dto)
        {
            return new RumDbo {
                Id     = dto.Id ?? Guid.NewGuid(),
                Name   = dto.Name,
                Teaser = dto.Teaser,
                Images = String.Join(';', dto.Images ?? Array.Empty<String>()),
            };
        }

        public static RumDto ToDto(this RumDbo entity)
        {
            return new RumDto {
                Id     = entity.Id,
                Name   = entity.Name,
                Teaser = entity.Teaser,
                Images = entity.Images?.Split(';', StringSplitOptions.RemoveEmptyEntries),
            };
        }
    }
}
