// Copyright (c) 2021, Olaf Kober <olaf.kober@outlook.com>

using System;


namespace Bar.Domain
{
    /// <summary>
    ///     Represents an Image of a Drink, Gin or Rum.
    /// </summary>
    public sealed class Image
    {
        /// <summary>
        ///     The unique Id of the Image.
        /// </summary>
        public Int32 Id { get; set; }

        /// <summary>
        ///     The name of the Image.
        /// </summary>
        public String Name { get; set; }
    }
}
