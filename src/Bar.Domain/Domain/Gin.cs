// Copyright (c) 2021, Olaf Kober <olaf.kober@outlook.com>

using System;
using System.Collections.Generic;


namespace Bar.Domain
{
    /// <summary>
    ///     Represents a Gin.
    /// </summary>
    /// 
    /// <param name="Id">
    ///     The unique Id of the Gin.
    /// </param>
    /// <param name="Name">
    ///     The name of the Gin, e.g. "The Stin Dry Gin".
    /// </param>
    public sealed record Gin(Guid Id, String Name)
    {
        /// <summary>
        ///     A teaser of the Gin, e.g. "Styrian Dry Gin".
        /// </summary>
        public String Teaser { get; init; } = "";

        /// <summary>
        ///     A list of images of the Gin.
        /// </summary>
        public IReadOnlyList<Image> Images { get; init; } = Array.Empty<Image>();
    }
}
