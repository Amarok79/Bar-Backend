﻿// Copyright (c) 2022, Olaf Kober <olaf.kober@outlook.com>

using Bar.Domain;
using NFluent;
using NUnit.Framework;


namespace Bar.Backend.Controllers;


[TestFixture]
public class RumExtensionsTests
{
    [Test]
    public void ToEntity_Name()
    {
        var dto = new RumDto {
            Name = "Clément Rhum Blanc",
        };

        var entity = dto.ToEntity();

        Check.That(entity.Id).IsNotEqualTo(Guid.Empty);
        Check.That(entity.Name).IsEqualTo("Clément Rhum Blanc");
        Check.That(entity.Teaser).IsEmpty();
        Check.That(entity.Description).IsEmpty();
        Check.That(entity.Images).IsEmpty();
        Check.That(entity.IsDraft).IsTrue();
    }

    [Test]
    public void ToEntity_Id_Name()
    {
        var id = Guid.NewGuid();

        var dto = new RumDto {
            Id = id,
            Name = "Clément Rhum Blanc",
        };

        var entity = dto.ToEntity();

        Check.That(entity.Id).IsEqualTo(id);
        Check.That(entity.Name).IsEqualTo("Clément Rhum Blanc");
        Check.That(entity.Teaser).IsEmpty();
        Check.That(entity.Description).IsEmpty();
        Check.That(entity.Images).IsEmpty();
        Check.That(entity.IsDraft).IsTrue();
    }

    [Test]
    public void ToEntity_Id_Name_Teaser_Images()
    {
        var id = Guid.NewGuid();

        var dto = new RumDto {
            Id = id,
            Name = "Clément Rhum Blanc",
            Teaser = "Martinique",
            Description = "Foo",
            Images = new[] { "KRO01084.jpg", "KRO00410.jpg" },
            IsDraft = false,
        };

        var entity = dto.ToEntity();

        Check.That(entity.Id).IsEqualTo(id);
        Check.That(entity.Name).IsEqualTo("Clément Rhum Blanc");
        Check.That(entity.Teaser).IsEqualTo("Martinique");
        Check.That(entity.Description).IsEqualTo("Foo");
        Check.That(entity.Images).HasSize(2);
        Check.That(entity.Images[0].FileName).IsEqualTo("KRO01084.jpg");
        Check.That(entity.Images[1].FileName).IsEqualTo("KRO00410.jpg");
        Check.That(entity.IsDraft).IsFalse();
    }


    [Test]
    public void ToDto()
    {
        var id = Guid.NewGuid();

        var entity = new Rum(id, "Clément Rhum Blanc") {
            Teaser = "Martinique",
            Description = "Foo",
            Images = new[] { new Image("KRO01084.jpg"), new Image("KRO00410.jpg") },
        };

        var dto = entity.ToDto();

        Check.That(dto.Id).IsEqualTo(id);
        Check.That(dto.Name).IsEqualTo("Clément Rhum Blanc");
        Check.That(dto.Teaser).IsEqualTo("Martinique");
        Check.That(dto.Description).IsEqualTo("Foo");
        Check.That(dto.Images).ContainsExactly("KRO01084.jpg", "KRO00410.jpg");
        Check.That(dto.IsDraft).IsTrue();
    }
}
