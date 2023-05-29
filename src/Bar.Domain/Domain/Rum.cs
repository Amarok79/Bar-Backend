// Copyright (c) 2022, Olaf Kober <olaf.kober@outlook.com>

namespace Bar.Domain;


/// <summary>
///     Represents a Rum.
/// </summary>
/// 
/// <param name="Id">
///     The unique Id of the Rum.
/// </param>
/// <param name="Name">
///     The name of the Rum, e.g. "Clément Rhum Blanc".
/// </param>
public sealed record Rum(
    Guid Id,
    String Name
)
{
    /// <summary>
    ///     A teaser of the Rum, e.g. "Martinique".
    /// </summary>
    public String Teaser { get; init; } = "";

    /// <summary>
    ///     A longer description of the Rum in Markdown markup language.
    /// </summary>
    public String Description { get; init; } = String.Empty;

    /// <summary>
    ///     A list of images of the Rum.
    /// </summary>
    public IReadOnlyList<Image> Images { get; init; } = Array.Empty<Image>();

    /// <summary>
    ///     Indicates whether the Rum is in draft-mode.
    /// </summary>
    public Boolean IsDraft { get; init; } = true;
}
