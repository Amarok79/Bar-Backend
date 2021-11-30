// Copyright (c) 2021, Olaf Kober <olaf.kober@outlook.com>

using System;
using System.Collections.Generic;
using System.Linq;
using Bar.Domain;


namespace Bar.Data;

internal static class GinExtensions
{
    public static GinDbo ToDbo(this Gin entity)
    {
        return new GinDbo {
            Id     = entity.Id,
            Name   = entity.Name,
            Teaser = entity.Teaser,
            Images = String.Join(';', entity.Images.Select(x => x.FileName)),
        };
    }

    public static Gin ToEntity(this GinDbo dbo)
    {
        return new Gin(dbo.Id, dbo.Name) {
            Teaser = dbo.Teaser ?? String.Empty,
            Images = mapImages(),
        };


        IReadOnlyList<Image> mapImages()
        {
            return dbo.Images == null
                ? Array.Empty<Image>()
                : dbo.Images.Split(';', StringSplitOptions.RemoveEmptyEntries)
                   .Select(x => new Image(x))
                   .ToList();
        }
    }
}
