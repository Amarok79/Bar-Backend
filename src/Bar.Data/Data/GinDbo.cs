// Copyright (c) 2022, Olaf Kober <olaf.kober@outlook.com>

using System;
using System.ComponentModel.DataAnnotations.Schema;


namespace Bar.Data;


/// <summary>
///     Represents a Gin.
/// </summary>
[Table("Gins")]
internal sealed class GinDbo
{
    /// <summary>
    ///     The unique Id of the Gin.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     The name of the Gin, e.g. "The Stin Dry Gin".
    /// </summary>
    public String Name { get; set; } = default!;

    /// <summary>
    ///     A longer description of the Gin in Markdown markup language.
    /// </summary>
    public String? Description { get; set; }

    /// <summary>
    ///     A teaser of the Gin, e.g. "Styrian Dry Gin".
    /// </summary>
    public String? Teaser { get; set; }

    /// <summary>
    ///     A semi-colon separated list of images of the Gin, e.g. "KRO01046.jpg;KRO00364.jpg".
    /// </summary>
    public String? Images { get; set; }

    /// <summary>
    ///     Indicates whether the Gin is in draft-mode.
    /// </summary>
    public Boolean IsDraft { get; set; }
}
