// Copyright (c) 2022, Olaf Kober <olaf.kober@outlook.com>

namespace Bar.Backend.Controllers;


/// <summary>
///     Represents Version information.
/// </summary>
public sealed class VersionDto
{
    /// <summary>
    ///     The version of the Web Api server.
    /// </summary>
    public String? ServerVersion { get; set; }
}
