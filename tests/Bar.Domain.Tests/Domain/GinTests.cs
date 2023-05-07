// Copyright (c) 2022, Olaf Kober <olaf.kober@outlook.com>

using System;
using NFluent;
using NUnit.Framework;


namespace Bar.Domain;


[TestFixture]
public class GinTests
{
    [Test]
    public void Usage()
    {
        var id = Guid.NewGuid();

        var gin = new Gin(id, "The Stin Dry Gin");

        Check.That(gin.Id).IsEqualTo(id);
        Check.That(gin.Name).IsEqualTo("The Stin Dry Gin");
        Check.That(gin.Teaser).IsEmpty();
        Check.That(gin.Description).IsEmpty();
        Check.That(gin.Images).IsEmpty();
        Check.That(gin.IsDraft).IsTrue();


        gin = new Gin(id, "The Stin Dry Gin") {
            Teaser = "Styrian Dry Gin",
            Description = "Foo",
            Images = new[] { new Image("KRO01046.jpg"), new Image("KRO00364.jpg") },
            IsDraft = false,
        };

        Check.That(gin.Id).IsEqualTo(id);
        Check.That(gin.Name).IsEqualTo("The Stin Dry Gin");
        Check.That(gin.Teaser).IsEqualTo("Styrian Dry Gin");
        Check.That(gin.Description).IsEqualTo("Foo");
        Check.That(gin.Images).HasSize(2);
        Check.That(gin.IsDraft).IsFalse();
        Check.That(gin.Images[0].FileName).IsEqualTo("KRO01046.jpg");
        Check.That(gin.Images[1].FileName).IsEqualTo("KRO00364.jpg");


        gin = gin with {
            Teaser = "Bar",
            Description = "Made in Styria",
            IsDraft = true,
        };

        Check.That(gin.Id).IsEqualTo(id);
        Check.That(gin.Name).IsEqualTo("The Stin Dry Gin");
        Check.That(gin.Teaser).IsEqualTo("Bar");
        Check.That(gin.Description).IsEqualTo("Made in Styria");
        Check.That(gin.Images).HasSize(2);
        Check.That(gin.Images[0].FileName).IsEqualTo("KRO01046.jpg");
        Check.That(gin.Images[1].FileName).IsEqualTo("KRO00364.jpg");
        Check.That(gin.IsDraft).IsTrue();
    }
}
