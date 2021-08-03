﻿// Copyright (c) 2021, Olaf Kober <olaf.kober@outlook.com>

using System;


namespace Bar.Domain
{
    /// <summary>
    ///     Represents a Rum.
    /// </summary>
    public sealed class Rum
    {
        /// <summary>
        ///     The unique Id of the Rum.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     The name of the Rum, e.g. "Clément Rhum Blanc".
        /// </summary>
        public String Name { get; set; } = default!;

        /// <summary>
        ///     A teaser of the Rum, e.g. "Martinique".
        /// </summary>
        public String? Teaser { get; set; }

        /// <summary>
        ///     A semi-colon separated list of images of the Rum, e.g. "KRO01084.jpg;KRO00410.jpg".
        /// </summary>
        public String? Images { get; set; } = default!;
    }
}
