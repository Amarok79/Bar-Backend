// Copyright (c) 2021, Olaf Kober <olaf.kober@outlook.com>

using System;
using NFluent;
using NUnit.Framework;


namespace Bar.Domain
{
    [TestFixture]
    public class RumTests
    {
        [Test]
        public void Usage()
        {
            var id = Guid.NewGuid();

            var rum = new Rum(id, "Clément Rhum Blanc");

            Check.That(rum.Id).IsEqualTo(id);
            Check.That(rum.Name).IsEqualTo("Clément Rhum Blanc");
            Check.That(rum.Teaser).IsEmpty();
            Check.That(rum.Images).IsEmpty();

            rum = new Rum(id, "Clément Rhum Blanc") {
                Teaser = "Martinique",
                Images = new[] { new Image("KRO01084.jpg"), new Image("KRO00410.jpg") },
            };

            Check.That(rum.Id).IsEqualTo(id);
            Check.That(rum.Name).IsEqualTo("Clément Rhum Blanc");
            Check.That(rum.Teaser).IsEqualTo("Martinique");
            Check.That(rum.Images).HasSize(2);
            Check.That(rum.Images[0].FileName).IsEqualTo("KRO01084.jpg");
            Check.That(rum.Images[1].FileName).IsEqualTo("KRO00410.jpg");

            rum = rum with { Teaser = "foo" };

            Check.That(rum.Id).IsEqualTo(id);
            Check.That(rum.Name).IsEqualTo("Clément Rhum Blanc");
            Check.That(rum.Teaser).IsEqualTo("foo");
            Check.That(rum.Images).HasSize(2);
            Check.That(rum.Images[0].FileName).IsEqualTo("KRO01084.jpg");
            Check.That(rum.Images[1].FileName).IsEqualTo("KRO00410.jpg");
        }
    }
}
