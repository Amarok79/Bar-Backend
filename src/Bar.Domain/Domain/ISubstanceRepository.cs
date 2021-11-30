﻿// Copyright (c) 2021, Olaf Kober <olaf.kober@outlook.com>

using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Bar.Domain;

/// <summary>
///     Represents a repository of Substances.
/// </summary>
public interface ISubstanceRepository
{
    /// <summary>
    ///     Gets all items.
    /// </summary>
    Task<IReadOnlyList<Substance>> GetAllAsync();

    /// <summary>
    ///     Gets the item with the given Id, or null if not existing.
    /// </summary>
    /// 
    /// <param name="id">
    ///     The Id of the item to return.
    /// </param>
    Task<Substance?> GetOrDefaultAsync(String id);

    /// <summary>
    ///     Deletes the item with the given Id.
    /// </summary>
    /// 
    /// <param name="id">
    ///     The Id of the item to delete.
    /// </param>
    /// <returns>
    ///     True, if the item has been deleted; False, if not existing.
    /// </returns>
    Task<Boolean> DeleteAsync(String id);

    /// <summary>
    ///     Adds a new item or updates an existing one.
    /// </summary>
    /// 
    /// <param name="item">
    ///     The item to add or update.
    /// </param>
    /// <returns>
    ///     True, if a new item has been added, otherwise False if an existing item has been updated.
    /// </returns>
    Task<Boolean> AddOrUpdateAsync(Substance item);
}
