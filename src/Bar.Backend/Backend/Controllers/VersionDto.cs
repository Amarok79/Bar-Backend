// Copyright (c) 2021, Olaf Kober <olaf.kober@outlook.com>

using System;


namespace Bar.Backend.Controllers
{
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
}
