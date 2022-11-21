// Copyright (c) 2022, Olaf Kober <olaf.kober@outlook.com>

using System;
using Bar.Domain;
using NFluent;
using NUnit.Framework;


namespace Bar.Data;


[TestFixture]
public class SubstanceExtensionsTests
{
    [Test]
    public void ToDbo_Id_Name()
    {
        var entity = new Substance("KEY", "Grand Marnier");
        var dbo = entity.ToDbo();

        Check.That(dbo.Id).IsEqualTo("KEY");

        Check.That(dbo.Name).IsEqualTo("Grand Marnier");

        Check.That(dbo.Category).IsNull();

        Check.That(dbo.Unit).IsNull();
    }

    [Test]
    public void ToDbo_Id_Name_Category_Unit()
    {
        var entity = new Substance("KEY", "Grand Marnier") {
            Category = "Liqueurs",
            Unit = "cl",
        };

        var dbo = entity.ToDbo();

        Check.That(dbo.Id).IsEqualTo("KEY");

        Check.That(dbo.Name).IsEqualTo("Grand Marnier");

        Check.That(dbo.Category).IsEqualTo("Liqueurs");

        Check.That(dbo.Unit).IsEqualTo("cl");
    }


    [Test]
    public void ToEntity_Id_Name()
    {
        var dbo = new SubstanceDbo {
            Id = "KEY",
            Name = "Grand Marnier",
            Category = null,
            Unit = null,
        };

        var entity = dbo.ToEntity();

        Check.That(entity.Id).IsEqualTo("KEY");

        Check.That(entity.Name).IsEqualTo("Grand Marnier");

        Check.That(entity.Category).IsNull();

        Check.That(entity.Unit).IsNull();
    }

    [Test]
    public void ToEntity_Id_Name_Category_Unit()
    {
        var dbo = new SubstanceDbo {
            Id = "KEY",
            Name = "Grand Marnier",
            Category = "Liqueurs",
            Unit = "cl",
        };

        var entity = dbo.ToEntity();

        Check.That(entity.Id).IsEqualTo("KEY");

        Check.That(entity.Name).IsEqualTo("Grand Marnier");

        Check.That(entity.Category).IsEqualTo("Liqueurs");

        Check.That(entity.Unit).IsEqualTo("cl");
    }
}
