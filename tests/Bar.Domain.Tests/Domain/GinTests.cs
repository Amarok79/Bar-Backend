// Copyright (c) 2021, Olaf Kober <olaf.kober@outlook.com>

using System;
using NFluent;
using NUnit.Framework;


namespace Bar.Domain
{
    [TestFixture]
    public class GinTests
    {
        [Test]
        public void Usage()
        {
            var id = Guid.NewGuid();

            var Gin = new Gin(id, "The Stin Dry Gin");

            Check.That(Gin.Id).IsEqualTo(id);
            Check.That(Gin.Name).IsEqualTo("The Stin Dry Gin");
            Check.That(Gin.Teaser).IsEmpty();
            Check.That(Gin.Images).IsEmpty();

            Gin = new Gin(id, "The Stin Dry Gin") {
                Teaser = "Styrian Dry Gin",
                Images = new[] { new Image("KRO01046.jpg"), new Image("KRO00364.jpg") },
            };

            Check.That(Gin.Id).IsEqualTo(id);
            Check.That(Gin.Name).IsEqualTo("The Stin Dry Gin");
            Check.That(Gin.Teaser).IsEqualTo("Styrian Dry Gin");
            Check.That(Gin.Images).HasSize(2);
            Check.That(Gin.Images[0].FileName).IsEqualTo("KRO01046.jpg");
            Check.That(Gin.Images[1].FileName).IsEqualTo("KRO00364.jpg");

            Gin = Gin with { Teaser = "foo" };

            Check.That(Gin.Id).IsEqualTo(id);
            Check.That(Gin.Name).IsEqualTo("The Stin Dry Gin");
            Check.That(Gin.Teaser).IsEqualTo("foo");
            Check.That(Gin.Images).HasSize(2);
            Check.That(Gin.Images[0].FileName).IsEqualTo("KRO01046.jpg");
            Check.That(Gin.Images[1].FileName).IsEqualTo("KRO00364.jpg");
        }
    }
}
