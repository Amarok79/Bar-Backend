// Copyright (c) 2021, Olaf Kober <olaf.kober@outlook.com>

using System;
using Bar.Domain;


namespace Bar.Data
{
    internal static class SubstanceExtensions
    {
        public static SubstanceDbo ToDbo(this Substance entity)
        {
            return new SubstanceDbo {
                Id       = entity.Id,
                Name     = entity.Name,
                Category = entity.Category,
                Unit     = entity.Unit,
            };
        }

        public static Substance ToEntity(this SubstanceDbo dbo)
        {
            return new Substance(dbo.Id, dbo.Name) {
                Category = dbo.Category,
                Unit     = dbo.Unit,
            };
        }
    }
}
