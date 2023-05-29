// Copyright (c) 2022, Olaf Kober <olaf.kober@outlook.com>

namespace Bar.Domain;


/// <summary>
///     Represents an Image of a Rum, Gin, or a Drink.
/// </summary>
/// 
/// <param name="FileName">
///     The file name of the Image, e.g. "KRO01084.jpg".
/// </param>
public sealed record Image(
    String FileName
);
