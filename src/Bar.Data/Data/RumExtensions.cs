// Copyright (c) 2022, Olaf Kober <olaf.kober@outlook.com>

using Bar.Domain;


namespace Bar.Data;


internal static class RumExtensions
{
    public static RumDbo ToDbo(
        this Rum entity
    )
    {
        return new RumDbo {
            Id = entity.Id,
            Name = entity.Name,
            Teaser = entity.Teaser,
            Images = String.Join(';', entity.Images.Select(x => x.FileName)),
        };
    }

    public static Rum ToEntity(
        this RumDbo dbo
    )
    {
        return new Rum(dbo.Id, dbo.Name) {
            Teaser = dbo.Teaser ?? String.Empty,
            Images = mapImages(),
        };


        IReadOnlyList<Image> mapImages()
        {
            return dbo.Images == null
                ? Array.Empty<Image>()
                : dbo.Images.Split(';', StringSplitOptions.RemoveEmptyEntries).Select(x => new Image(x)).ToList();
        }
    }
}
