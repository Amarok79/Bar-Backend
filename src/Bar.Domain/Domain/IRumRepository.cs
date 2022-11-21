// Copyright (c) 2022, Olaf Kober <olaf.kober@outlook.com>

using System;


namespace Bar.Domain;


/// <summary>
///     Represents a repository of Rums.
/// </summary>
public interface IRumRepository : IRepository<Guid, Rum>
{
}
