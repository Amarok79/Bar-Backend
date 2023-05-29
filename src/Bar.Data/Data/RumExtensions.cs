// Copyright (c) 2023, Olaf Kober <olaf.kober@outlook.com>

using Bar.Domain;


namespace Bar.Data;


internal static class RumExtensions
{
    public static RumDbo ToDbo(
        this Rum entity
    )
    {
        return new RumDbo {
            Id = entity.Id.ToString(),
            Name = entity.Name,
            Teaser = entity.Teaser,
            Description = entity.Description,
            Images = entity.Images.Select(x => x.FileName).ToList(),
            IsDraft = entity.IsDraft,
        };
    }

    public static Rum ToEntity(
        this RumDbo dbo
    )
    {
        return new Rum(new Guid(dbo.Id), dbo.Name) {
            Teaser = dbo.Teaser ?? String.Empty,
            Description = dbo.Description ?? String.Empty,
            Images = mapImages(),
            IsDraft = dbo.IsDraft,
        };


        IReadOnlyList<Image> mapImages()
        {
            return dbo.Images.Select(x => new Image(x)).ToList();
        }
    }
}
