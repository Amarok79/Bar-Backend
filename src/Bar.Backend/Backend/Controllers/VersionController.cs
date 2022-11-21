// Copyright (c) 2022, Olaf Kober <olaf.kober@outlook.com>

using System;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;


namespace Bar.Backend.Controllers;


[ApiController, Route("api/version")]
public sealed class VersionController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        var version = Assembly.GetEntryAssembly()
          ?.GetCustomAttribute<AssemblyInformationalVersionAttribute>()
          ?.InformationalVersion;

        var dto = new VersionDto {
            ServerVersion = version,
        };

        return Ok(dto);
    }
}
