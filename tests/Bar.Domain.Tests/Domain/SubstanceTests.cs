// Copyright (c) 2021, Olaf Kober <olaf.kober@outlook.com>

using System;
using NFluent;
using NUnit.Framework;


namespace Bar.Domain;

[TestFixture]
public class SubstanceTests
{
    [Test]
    public void Usage()
    {
        var id = "Liqueurs/GrandMarnier";

        var sub = new Substance(id, "Grand Marnier");

        Check.That(sub.Id)
           .IsEqualTo(id);

        Check.That(sub.Name)
           .IsEqualTo("Grand Marnier");

        Check.That(sub.Category)
           .IsNull();

        Check.That(sub.Unit)
           .IsNull();

        sub = new Substance(id, "Grand Marnier") {
            Category = "Liqueurs",
            Unit     = "cl",
        };

        Check.That(sub.Id)
           .IsEqualTo(id);

        Check.That(sub.Name)
           .IsEqualTo("Grand Marnier");

        Check.That(sub.Category)
           .IsEqualTo("Liqueurs");

        Check.That(sub.Unit)
           .IsEqualTo("cl");

        sub = sub with { Category = "Liköre" };

        Check.That(sub.Id)
           .IsEqualTo(id);

        Check.That(sub.Name)
           .IsEqualTo("Grand Marnier");

        Check.That(sub.Category)
           .IsEqualTo("Liköre");

        Check.That(sub.Unit)
           .IsEqualTo("cl");
    }
}
