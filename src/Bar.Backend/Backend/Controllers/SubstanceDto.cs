// Copyright (c) 2021, Olaf Kober <olaf.kober@outlook.com>

using System;


namespace Bar.Backend.Controllers
{
    /// <summary>
    ///     Represents a Substrate used in Recipes.
    /// </summary>
    public sealed class SubstanceDto
    {
        /// <summary>
        ///     The unique Id of the Substrate.
        /// </summary>
        public String? Id { get; set; }

        /// <summary>
        ///     The name of the Substrate, e.g. "Grand Marnier".
        /// </summary>
        public String Name { get; set; } = default!;

        /// <summary>
        ///     The name of the category the Substrate belongs to, e.g. "Liqueurs".
        /// </summary>
        public String? Category { get; set; }

        /// <summary>
        ///     A list of images of the Substance, e.g. "KRO01084.jpg", "KRO00410.jpg".
        /// </summary>
        public String? Unit { get; set; }
    }
}
