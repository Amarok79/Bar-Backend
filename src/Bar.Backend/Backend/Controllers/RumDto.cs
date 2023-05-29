// Copyright (c) 2022, Olaf Kober <olaf.kober@outlook.com>

namespace Bar.Backend.Controllers;


/// <summary>
///     Represents a Rum.
/// </summary>
public sealed class RumDto
{
    /// <summary>
    ///     The unique Id of the Rum.
    /// </summary>
    public Guid? Id { get; set; }

    /// <summary>
    ///     The name of the Rum, e.g. "Clément Rhum Blanc".
    /// </summary>
    public String Name { get; set; } = default!;

    /// <summary>
    ///     A teaser of the Rum, e.g. "Martinique".
    /// </summary>
    public String? Teaser { get; set; }

    /// <summary>
    ///     A longer description of the Rum in Markdown markup language.
    /// </summary>
    public String? Description { get; set; }

    /// <summary>
    ///     A list of images of the Rum, e.g. "KRO01084.jpg", "KRO00410.jpg".
    /// </summary>
    public IList<String>? Images { get; set; }

    /// <summary>
    ///     Indicates whether the Rum is in draft-mode. Defaults to true.
    /// </summary>
    public Boolean IsDraft { get; set; } = true;
}
