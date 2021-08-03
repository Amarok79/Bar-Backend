// Copyright (c) 2021, Olaf Kober <olaf.kober@outlook.com>

using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Bar.Domain
{
    /// <summary>
    ///     Represents a repository of Rums.
    /// </summary>
    public interface IRumRepository
    {
        /// <summary>
        ///     Gets all items.
        /// </summary>
        Task<IReadOnlyCollection<Rum>> GetAsync();

        /// <summary>
        ///     Gets the item with the given Id, or null if not existing.
        /// </summary>
        /// 
        /// <param name="id">
        ///     The Id of the item to return.
        /// </param>
        Task<Rum?> GetAsync(Guid id);

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
        Task<Boolean> DeleteAsync(Guid id);

        /// <summary>
        ///     Updates an existing item, or creates a new one.
        /// </summary>
        /// 
        /// <param name="item">
        ///     The item to update or create.
        /// </param>
        /// <returns>
        ///     True, if an existing item has been updated, otherwise False.
        /// </returns>
        Task<Boolean> UpdateAsync(Rum item);
    }
}
