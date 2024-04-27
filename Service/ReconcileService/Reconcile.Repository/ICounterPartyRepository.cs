using System;
using Reconcile.Entity;

namespace Reconcile.Repository
{
    /// <summary>
    /// Interface for interacting with a repository of CounterParty data.
    /// </summary>
    public interface ICounterPartyRepository
    {
        /// <summary>
        /// Retrieves all CounterParty objects from the repository.
        /// </summary>
        /// <returns>An asynchronous task returning an IEnumerable of CounterParty objects.</returns>
        Task<IEnumerable<CounterParty>> GetAll();

        /// <summary>
        /// Retrieves a CounterParty object by its unique identifier.
        /// </summary>
        /// <param name="id">The identifier of the CounterParty to retrieve.</param>
        /// <returns>An asynchronous task returning a CounterParty object with the matching identifier, or null if not found.</returns>
        Task<CounterParty> GetById(int id);

        /// <summary>
        /// Creates a new CounterParty object in the repository.
        /// </summary>
        /// <param name="counterParty">The CounterParty object to create.</param>
        /// <returns>An asynchronous task.</returns>
        Task Create(CounterParty counterParty);

        /// <summary>
        /// Updates an existing CounterParty object in the repository.
        /// </summary>
        /// <param name="counterParty">The CounterParty object with the updated information.</param>
        /// <returns>An asynchronous task.</returns>
        Task Update(CounterParty counterParty);

        /// <summary>
        /// Deletes a CounterParty object from the repository by its unique identifier.
        /// </summary>
        /// <param name="id">The identifier of the CounterParty to delete.</param>
        /// <returns>An asynchronous task.</returns>
        Task Delete(int id);
    }

}

