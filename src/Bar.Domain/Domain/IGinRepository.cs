// Copyright (c) 2021, Olaf Kober <olaf.kober@outlook.com>

using System;


namespace Bar.Domain;

/// <summary>
///     Represents a repository of Gins.
/// </summary>
public interface IGinRepository : IRepository<Guid, Gin>
{
}
