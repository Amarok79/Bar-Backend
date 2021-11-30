// Copyright (c) 2021, Olaf Kober <olaf.kober@outlook.com>

using System;


namespace Bar.Domain;

/// <summary>
///     Represents a Substrate used in Recipes.
/// </summary>
/// 
/// <param name="Id">
///     The unique Id of the Substrate.
/// </param>
/// <param name="Name">
///     The name of the Substrate, e.g. "Grand Marnier".
/// </param>
public sealed record Substance(String Id, String Name)
{
    /// <summary>
    ///     The name of the category the Substrate belongs to, e.g. "Liqueurs".
    /// </summary>
    public String? Category { get; init; }

    /// <summary>
    ///     The unit in which the Substrate is used in Recipes, e.g. "cl".
    /// </summary>
    public String? Unit { get; init; }
}
