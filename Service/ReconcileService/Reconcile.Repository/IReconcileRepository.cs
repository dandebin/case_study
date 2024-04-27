using System;
using Reconcile.Entity;

namespace Reconcile.Repository
{
    /// <summary>
    /// Interface for interacting with a repository of ReconcileItem data.
    /// </summary>
    public interface IReconcileRepository
    {
        /// <summary>
        /// Retrieves all ReconcileItem objects from the repository.
        /// </summary>
        /// <returns>An asynchronous task returning an IEnumerable of ReconcileItem objects.</returns>
        Task<IEnumerable<ReconcileItem>> GetAll();

        /// <summary>
        /// Retrieves a ReconcileItem object by its unique identifier.
        /// </summary>
        /// <param name="id">The identifier of the ReconcileItem to retrieve.</param>
        /// <returns>An asynchronous task returning a ReconcileItem object with the matching identifier, or null if not found.</returns>
        Task<ReconcileItem> GetById(int id);

        /// <summary>
        /// Creates a new ReconcileItem object in the repository.
        /// </summary>
        /// <param name="reconcileItem">The ReconcileItem object to create.</param>
        /// <returns>An asynchronous task.</returns>
        Task Create(ReconcileItem reconcileItem);

        /// <summary>
        /// Updates an existing ReconcileItem object in the repository.
        /// </summary>
        /// <param name="reconcileItem">The ReconcileItem object with the updated information.</param>
        /// <returns>An asynchronous task.</returns>
        Task Update(ReconcileItem reconcileItem);

        /// <summary>
        /// Deletes a ReconcileItem object from the repository by its unique identifier.
        /// </summary>
        /// <param name="id">The identifier of the ReconcileItem to delete.</param>
        /// <returns>An asynchronous task.</returns>
        Task Delete(int id);
    }
}

