// Copyright (c) 2021, Olaf Kober <olaf.kober@outlook.com>

using System;
using System.ComponentModel.DataAnnotations.Schema;


namespace Bar.Data
{
    /// <summary>
    ///     Represents a Substance.
    /// </summary>
    [Table("Substances")]
    internal sealed class SubstanceDbo
    {
        /// <summary>
        ///     The unique Id of the Substance.
        /// </summary>
        public String Id { get; set; } = default!;

        /// <summary>
        ///     The name of the Substance, e.g. "Grand Marnier".
        /// </summary>
        public String Name { get; set; } = default!;

        /// <summary>
        ///     The name of the category the Substrate belongs to, e.g. "Liqueurs".
        /// </summary>
        public String? Category { get; set; }

        /// <summary>
        ///     The unit in which the Substrate is used in Recipes, e.g. "cl".
        /// </summary>
        public String? Unit { get; set; }
    }
}
