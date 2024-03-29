﻿// Copyright (c) 2023, Olaf Kober <olaf.kober@outlook.com>

using Bar.Domain;
using NFluent;
using NUnit.Framework;


namespace Bar.Data;


[TestFixture]
public class GinExtensionsTests
{
    [Test]
    public void ToDbo_Id_Name()
    {
        var id = Guid.NewGuid();
        var entity = new Gin(id, "The Stin Dry Gin");
        var dbo = entity.ToDbo();

        Check.That(dbo.Id).IsEqualTo(id.ToString());
        Check.That(dbo.Name).IsEqualTo("The Stin Dry Gin");
        Check.That(dbo.Teaser).IsEmpty();
        Check.That(dbo.Description).IsEmpty();
        Check.That(dbo.Images).IsEmpty();
        Check.That(dbo.IsDraft).IsTrue();
    }

    [Test]
    public void ToDbo_Id_Name_Teaser_Description_Images_IsDraft()
    {
        var id = Guid.NewGuid();

        var entity = new Gin(id, "The Stin Dry Gin") {
            Teaser = "Styrian Dry Gin",
            Description = "Foo",
            Images = new[] { new Image("KRO01046.jpg"), new Image("KRO00364.jpg") },
            IsDraft = true,
        };

        var dbo = entity.ToDbo();

        Check.That(dbo.Id).IsEqualTo(id.ToString());
        Check.That(dbo.Name).IsEqualTo("The Stin Dry Gin");
        Check.That(dbo.Teaser).IsEqualTo("Styrian Dry Gin");
        Check.That(dbo.Description).IsEqualTo("Foo");
        Check.That(dbo.Images).ContainsExactly("KRO01046.jpg", "KRO00364.jpg");
        Check.That(dbo.IsDraft).IsTrue();
    }


    [Test]
    public void ToEntity_Id_Name()
    {
        var id = Guid.NewGuid();

        var dbo = new GinDbo {
            Id = id.ToString(),
            Name = "The Stin Dry Gin",
            Teaser = null,
            Description = null,
            Images = Array.Empty<String>(),
            IsDraft = true,
        };

        var entity = dbo.ToEntity();

        Check.That(entity.Id).IsEqualTo(id);
        Check.That(entity.Name).IsEqualTo("The Stin Dry Gin");
        Check.That(entity.Teaser).IsEmpty();
        Check.That(entity.Description).IsEmpty();
        Check.That(entity.Images).IsEmpty();
        Check.That(entity.IsDraft).IsTrue();
    }

    [Test]
    public void ToEntity_Id_Name_Teaser_Description_Images_IsDraft()
    {
        var id = Guid.NewGuid();

        var dbo = new GinDbo {
            Id = id.ToString(),
            Name = "The Stin Dry Gin",
            Teaser = "Styrian Dry Gin",
            Description = "Foo",
            Images = new[] { "KRO01046.jpg", "KRO00364.jpg" },
            IsDraft = true,
        };

        var entity = dbo.ToEntity();

        Check.That(entity.Id).IsEqualTo(id);
        Check.That(entity.Name).IsEqualTo("The Stin Dry Gin");
        Check.That(entity.Teaser).IsEqualTo("Styrian Dry Gin");
        Check.That(entity.Description).IsEqualTo("Foo");
        Check.That(entity.Images).HasSize(2);
        Check.That(entity.Images[0].FileName).IsEqualTo("KRO01046.jpg");
        Check.That(entity.Images[1].FileName).IsEqualTo("KRO00364.jpg");
        Check.That(entity.IsDraft).IsTrue();
    }
}
