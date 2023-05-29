// Copyright (c) 2023, Olaf Kober <olaf.kober@outlook.com>

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

        Check.That(dbo.Id).IsEqualTo(id.ToString());
        Check.That(dbo.Name).IsEqualTo("Clément Rhum Blanc");
        Check.That(dbo.Teaser).IsEmpty();
        Check.That(dbo.Description).IsEmpty();
        Check.That(dbo.Images).IsEmpty();
        Check.That(dbo.IsDraft).IsTrue();
    }

    [Test]
    public void ToDbo_Id_Name_Teaser_Description_Images()
    {
        var id = Guid.NewGuid();

        var entity = new Rum(id, "Clément Rhum Blanc") {
            Teaser = "Martinique",
            Description = "Foo",
            Images = new[] { new Image("KRO01084.jpg"), new Image("KRO00410.jpg") },
            IsDraft = true,
        };

        var dbo = entity.ToDbo();

        Check.That(dbo.Id).IsEqualTo(id.ToString());
        Check.That(dbo.Name).IsEqualTo("Clément Rhum Blanc");
        Check.That(dbo.Teaser).IsEqualTo("Martinique");
        Check.That(dbo.Description).IsEqualTo("Foo");
        Check.That(dbo.Images).ContainsExactly("KRO01084.jpg", "KRO00410.jpg");
        Check.That(dbo.IsDraft).IsTrue();
    }


    [Test]
    public void ToEntity_Id_Name()
    {
        var id = Guid.NewGuid();

        var dbo = new RumDbo {
            Id = id.ToString(),
            Name = "Clément Rhum Blanc",
            Teaser = null,
            Description = null,
            Images = Array.Empty<String>(),
            IsDraft = true,
        };

        var entity = dbo.ToEntity();

        Check.That(entity.Id).IsEqualTo(id);
        Check.That(entity.Name).IsEqualTo("Clément Rhum Blanc");
        Check.That(entity.Teaser).IsEmpty();
        Check.That(entity.Description).IsEmpty();
        Check.That(entity.Images).IsEmpty();
        Check.That(entity.IsDraft).IsTrue();
    }

    [Test]
    public void ToEntity_Id_Name_Teaser_Description_Images()
    {
        var id = Guid.NewGuid();

        var dbo = new RumDbo {
            Id = id.ToString(),
            Name = "Clément Rhum Blanc",
            Teaser = "Martinique",
            Description = "Foo",
            Images = new[] { "KRO01084.jpg", "KRO00410.jpg" },
            IsDraft = true,
        };

        var entity = dbo.ToEntity();

        Check.That(entity.Id).IsEqualTo(id);
        Check.That(entity.Name).IsEqualTo("Clément Rhum Blanc");
        Check.That(entity.Teaser).IsEqualTo("Martinique");
        Check.That(entity.Description).IsEqualTo("Foo");
        Check.That(entity.Images).HasSize(2);
        Check.That(entity.Images[0].FileName).IsEqualTo("KRO01084.jpg");
        Check.That(entity.Images[1].FileName).IsEqualTo("KRO00410.jpg");
        Check.That(entity.IsDraft).IsTrue();
    }
}
