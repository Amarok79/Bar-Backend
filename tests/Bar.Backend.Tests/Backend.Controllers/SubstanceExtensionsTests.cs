// Copyright (c) 2021, Olaf Kober <olaf.kober@outlook.com>

using System;
using Bar.Domain;
using NFluent;
using NUnit.Framework;


namespace Bar.Backend.Controllers
{
    [TestFixture]
    public class SubstanceExtensionsTests
    {
        [Test]
        public void ToEntity_Name()
        {
            var dto = new SubstanceDto {
                Name = "Grand Marnier",
            };

            var entity = dto.ToEntity();

            Check.That(entity.Id).IsNotEmpty();
            Check.That(entity.Name).IsEqualTo("Grand Marnier");
            Check.That(entity.Category).IsNull();
            Check.That(entity.Unit).IsNull();
        }

        [Test]
        public void ToEntity_Id_Name()
        {
            var dto = new SubstanceDto {
                Id   = "KEY",
                Name = "Grand Marnier",
            };

            var entity = dto.ToEntity();

            Check.That(entity.Id).IsEqualTo("KEY");
            Check.That(entity.Name).IsEqualTo("Grand Marnier");
            Check.That(entity.Category).IsNull();
            Check.That(entity.Unit).IsNull();
        }

        [Test]
        public void ToEntity_Id_Name_Category_Unit()
        {
            var dto = new SubstanceDto {
                Id       = "KEY",
                Name     = "Grand Marnier",
                Category = "Liqueurs",
                Unit     = "cl",
            };

            var entity = dto.ToEntity();

            Check.That(entity.Id).IsEqualTo("KEY");
            Check.That(entity.Name).IsEqualTo("Grand Marnier");
            Check.That(entity.Category).IsEqualTo("Liqueurs");
            Check.That(entity.Unit).IsEqualTo("cl");
        }


        [Test]
        public void ToDto()
        {
            var entity = new Substance("KEY", "Grand Marnier") {
                Category = "Liqueurs",
                Unit     = "cl",
            };

            var dto = entity.ToDto();

            Check.That(dto.Id).IsEqualTo("KEY");
            Check.That(dto.Name).IsEqualTo("Grand Marnier");
            Check.That(dto.Category).IsEqualTo("Liqueurs");
            Check.That(dto.Unit).IsEqualTo("cl");
        }
    }
}
