// Copyright (c) 2021, Olaf Kober <olaf.kober@outlook.com>

using System;
using NFluent;
using NUnit.Framework;


namespace Bar.Domain
{
    [TestFixture]
    public class ImageTests
    {
        [Test]
        public void Usage()
        {
            var image = new Image("KRO01084.jpg");

            Check.That(image.FileName).IsEqualTo("KRO01084.jpg");

            image = image with { FileName = "KRO00410.jpg" };

            Check.That(image.FileName).IsEqualTo("KRO00410.jpg");
        }
    }
}
