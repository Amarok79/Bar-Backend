// Copyright (c) 2022, Olaf Kober <olaf.kober@outlook.com>

using Bar.Domain;


namespace Bar.Data;


internal static class GinExtensions
{
    public static GinDbo ToDbo(
        this Gin entity
    )
    {
        return new GinDbo {
            Id = entity.Id,
            Name = entity.Name,
            Teaser = entity.Teaser,
            Description = entity.Description,
            Images = String.Join(';', entity.Images.Select(x => x.FileName)),
            IsDraft = entity.IsDraft,
        };
    }

    public static Gin ToEntity(
        this GinDbo dbo
    )
    {
        return new Gin(dbo.Id, dbo.Name) {
            Teaser = dbo.Teaser ?? String.Empty,
            Description = dbo.Description ?? String.Empty,
            Images = mapImages(),
            IsDraft = dbo.IsDraft,
        };


        IReadOnlyList<Image> mapImages()
        {
            return dbo.Images == null
                ? Array.Empty<Image>()
                : dbo.Images.Split(';', StringSplitOptions.RemoveEmptyEntries).Select(x => new Image(x)).ToList();
        }
    }
}
