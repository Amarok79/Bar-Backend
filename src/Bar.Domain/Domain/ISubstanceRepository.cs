// Copyright (c) 2022, Olaf Kober <olaf.kober@outlook.com>

namespace Bar.Domain;


/// <summary>
///     Represents a repository of Substances.
/// </summary>
public interface ISubstanceRepository : IRepository<String, Substance>
{
}
