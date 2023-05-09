// Copyright (c) 2022, Olaf Kober <olaf.kober@outlook.com>

using Bar.Domain;
using NFluent;
using NUnit.Framework;


namespace Bar.Data;


[TestFixture]
public class RumExtensionsTests
{
    [Test]
    public void ToDbo_Id_Name()
    {
        var id = Guid.NewGuid();
        var entity = new Rum(id, "Clément Rhum Blanc");
        var dbo = entity.ToDbo();

        Check.That(dbo.Id).IsEqualTo(id);

        Check.That(dbo.Name).IsEqualTo("Clément Rhum Blanc");

        Check.That(dbo.Teaser).IsEmpty();

        Check.That(dbo.Images).IsEmpty();
    }

    [Test]
    public void ToDbo_Id_Name_Teaser_Images()
    {
        var id = Guid.NewGuid();

        var entity = new Rum(id, "Clément Rhum Blanc") {
            Teaser = "Martinique",
            Images = new[] { new Image("KRO01084.jpg"), new Image("KRO00410.jpg") },
        };

        var dbo = entity.ToDbo();

        Check.That(dbo.Id).IsEqualTo(id);

        Check.That(dbo.Name).IsEqualTo("Clément Rhum Blanc");

        Check.That(dbo.Teaser).IsEqualTo("Martinique");

        Check.That(dbo.Images).IsEqualTo("KRO01084.jpg;KRO00410.jpg");
    }


    [Test]
    public void ToEntity_Id_Name()
    {
        var id = Guid.NewGuid();

        var dbo = new RumDbo {
            Id = id,
            Name = "Clément Rhum Blanc",
            Teaser = null,
            Images = null,
        };

        var entity = dbo.ToEntity();

        Check.That(entity.Id).IsEqualTo(id);

        Check.That(entity.Name).IsEqualTo("Clément Rhum Blanc");

        Check.That(entity.Teaser).IsEmpty();

        Check.That(entity.Images).IsEmpty();
    }

    [Test]
    public void ToEntity_Id_Name_Teaser_Images()
    {
        var id = Guid.NewGuid();

        var dbo = new RumDbo {
            Id = id,
            Name = "Clément Rhum Blanc",
            Teaser = "Martinique",
            Images = "KRO01084.jpg;KRO00410.jpg",
        };

        var entity = dbo.ToEntity();

        Check.That(entity.Id).IsEqualTo(id);

        Check.That(entity.Name).IsEqualTo("Clément Rhum Blanc");

        Check.That(entity.Teaser).IsEqualTo("Martinique");

        Check.That(entity.Images).HasSize(2);

        Check.That(entity.Images[0].FileName).IsEqualTo("KRO01084.jpg");

        Check.That(entity.Images[1].FileName).IsEqualTo("KRO00410.jpg");
    }
}
