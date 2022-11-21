// Copyright (c) 2022, Olaf Kober <olaf.kober@outlook.com>

using System;
using System.ComponentModel.DataAnnotations.Schema;


namespace Bar.Data;


/// <summary>
///     Represents a Rum.
/// </summary>
[Table("Rums")]
internal sealed class RumDbo
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
    public String? Images { get; set; }
}
