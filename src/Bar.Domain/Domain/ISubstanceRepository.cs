// Copyright (c) 2021, Olaf Kober <olaf.kober@outlook.com>

using System;


namespace Bar.Domain;

/// <summary>
///     Represents a repository of Substances.
/// </summary>
public interface ISubstanceRepository : IRepository<String, Substance>
{
}
