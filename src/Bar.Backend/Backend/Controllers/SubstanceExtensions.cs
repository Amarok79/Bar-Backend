// Copyright (c) 2022, Olaf Kober <olaf.kober@outlook.com>

using System;
using System.Globalization;
using Bar.Domain;


namespace Bar.Backend.Controllers;


public static class SubstanceExtensions
{
    public static Substance ToEntity(
        this SubstanceDto dto
    )
    {
        dto.Id ??= Guid.NewGuid().ToString("N", CultureInfo.InvariantCulture);

        return new Substance(dto.Id, dto.Name) {
            Category = dto.Category,
            Unit = dto.Unit,
        };
    }

    public static SubstanceDto ToDto(
        this Substance entity
    )
    {
        return new SubstanceDto {
            Id = entity.Id,
            Name = entity.Name,
            Category = entity.Category,
            Unit = entity.Unit,
        };
    }
}
