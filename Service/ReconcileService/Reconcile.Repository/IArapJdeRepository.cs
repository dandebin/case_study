using System;
using Reconcile.Entity;

namespace Reconcile.Repository
{
    /// <summary>
    /// Defines the contract for interacting with ARAP/JDE data.
    /// </summary>
    public interface IArapJdeRepository
    {
        /// <summary>
        /// Asynchronously retrieves the count of all ARAP/JDE records.
        /// </summary>
        /// <returns>The total count of ARAP/JDE records as an integer.</returns>
        Task<int> GetCount();

        /// <summary>
        /// Asynchronously retrieves a list of ARAP/JDE records based on the specified criteria.
        /// </summary>
        /// <param name="filter">The filter criteria to apply to the records.</param>
        /// <param name="sort">The sorting order of the results.</param>
        /// <param name="offset">The offset from which to start the list.</param>
        /// <param name="size">The number of records to retrieve.</param>
        /// <returns>An enumerable list of ARAP/JDE records.</returns>
        Task<IEnumerable<ArapJde>> GetList(string filter, string sort, int offset, int size);

        /// <summary>
        /// Asynchronously retrieves all ARAP/JDE records.
        /// </summary>
        /// <returns>An enumerable of all ARAP/JDE records.</returns>
        Task<IEnumerable<ArapJde>> GetAll();

        /// <summary>
        /// Asynchronously retrieves a single ARAP/JDE record by its identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the ARAP/JDE record to retrieve.</param>
        /// <returns>The ARAP/JDE record matching the given identifier.</returns>
        Task<ArapJde> GetById(int id);

        /// <summary>
        /// Asynchronously adds a new ARAP/JDE record to the repository.
        /// </summary>
        /// <param name="ArapJde">The ARAP/JDE record to add.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task Create(ArapJde ArapJde);

        /// <summary>
        /// Asynchronously updates an existing ARAP/JDE record.
        /// </summary>
        /// <param name="ArapJde">The ARAP/JDE record to update with new values.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task Update(ArapJde ArapJde);

        /// <summary>
        /// Asynchronously deletes an ARAP/JDE record by its identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the ARAP/JDE record to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task Delete(int id);
    }

}

