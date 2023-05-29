// Copyright (c) 2022, Olaf Kober <olaf.kober@outlook.com>

using Bar.Domain;
using NFluent;
using NUnit.Framework;


namespace Bar.Backend.Controllers;


[TestFixture]
public class GinExtensionsTests
{
    [Test]
    public void ToEntity_Name()
    {
        var dto = new GinDto {
            Name = "The Stin Dry Gin",
        };

        var entity = dto.ToEntity();

        Check.That(entity.Id).IsNotEqualTo(Guid.Empty);
        Check.That(entity.Name).IsEqualTo("The Stin Dry Gin");
        Check.That(entity.Teaser).IsEmpty();
        Check.That(entity.Description).IsEmpty();
        Check.That(entity.Images).IsEmpty();
        Check.That(entity.IsDraft).IsTrue();
    }

    [Test]
    public void ToEntity_Id_Name()
    {
        var id = Guid.NewGuid();

        var dto = new GinDto {
            Id = id,
            Name = "The Stin Dry Gin",
        };

        var entity = dto.ToEntity();

        Check.That(entity.Id).IsEqualTo(id);
        Check.That(entity.Name).IsEqualTo("The Stin Dry Gin");
        Check.That(entity.Teaser).IsEmpty();
        Check.That(entity.Description).IsEmpty();
        Check.That(entity.Images).IsEmpty();
        Check.That(entity.IsDraft).IsTrue();
    }

    [Test]
    public void ToEntity_Id_Name_Teaser_Description_Images()
    {
        var id = Guid.NewGuid();

        var dto = new GinDto {
            Id = id,
            Name = "The Stin Dry Gin",
            Teaser = "Styrian Dry Gin",
            Description = "Foo",
            Images = new[] { "KRO01046.jpg", "KRO00364.jpg" },
            IsDraft = false,
        };

        var entity = dto.ToEntity();

        Check.That(entity.Id).IsEqualTo(id);
        Check.That(entity.Name).IsEqualTo("The Stin Dry Gin");
        Check.That(entity.Teaser).IsEqualTo("Styrian Dry Gin");
        Check.That(entity.Description).IsEqualTo("Foo");
        Check.That(entity.Images).HasSize(2);
        Check.That(entity.Images[0].FileName).IsEqualTo("KRO01046.jpg");
        Check.That(entity.Images[1].FileName).IsEqualTo("KRO00364.jpg");
        Check.That(entity.IsDraft).IsFalse();
    }


    [Test]
    public void ToDto()
    {
        var id = Guid.NewGuid();

        var entity = new Gin(id, "The Stin Dry Gin") {
            Teaser = "Styrian Dry Gin",
            Description = "Foo",
            Images = new[] { new Image("KRO01046.jpg"), new Image("KRO00364.jpg") },
        };

        var dto = entity.ToDto();

        Check.That(dto.Id).IsEqualTo(id);
        Check.That(dto.Name).IsEqualTo("The Stin Dry Gin");
        Check.That(dto.Teaser).IsEqualTo("Styrian Dry Gin");
        Check.That(dto.Description).IsEqualTo("Foo");
        Check.That(dto.Images).ContainsExactly("KRO01046.jpg", "KRO00364.jpg");
        Check.That(dto.IsDraft).IsTrue();
    }
}
