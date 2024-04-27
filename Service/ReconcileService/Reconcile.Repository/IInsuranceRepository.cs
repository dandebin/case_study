using System;
using Reconcile.Entity;

namespace Reconcile.Repository
{
    
    /// <summary>
    /// Interface for interacting with a repository of Insurance data.
    /// </summary>
    public interface IInsuranceRepository
    {
        /// <summary>
        /// Retrieves all Insurance objects from the repository.
        /// </summary>
        /// <returns>An asynchronous task returning an IEnumerable of Insurance objects.</returns>
        Task<IEnumerable<Insurance>> GetAll();

        /// <summary>
        /// Retrieves an Insurance object by its unique identifier.
        /// </summary>
        /// <param name="id">The identifier of the Insurance to retrieve.</param>
        /// <returns>An asynchronous task returning an Insurance object with the matching identifier, or null if not found.</returns>
        Task<Insurance> GetById(int id);

        /// <summary>
        /// Retrieves a list of Insurance objects based on a provided list of CounterParty names.
        /// </summary>
        /// <param name="cp_name_list">A list of strings representing CounterParty names.</param>
        /// <returns>An asynchronous task returning an IEnumerable of Insurance objects associated with the CounterParty names provided, or an empty list if none are found.</returns>
        //Task<IEnumerable<Insurance>> GetByCpNameList(IList<string> cp_name_list);

        /// <summary>
        /// Creates a new Insurance object in the repository.
        /// </summary>
        /// <param name="insurance">The Insurance object to create.</param>
        /// <returns>An asynchronous task.</returns>
        Task Create(Insurance insurance);

        /// <summary>
        /// Updates an existing Insurance object in the repository.
        /// </summary>
        /// <param name="insurance">The Insurance object with the updated information.</param>
        /// <returns>An asynchronous task.</returns>
        Task Update(Insurance insurance);

        /// <summary>
        /// Deletes an Insurance object from the repository by its unique identifier.
        /// </summary>
        /// <param name="id">The identifier of the Insurance to delete.</param>
        /// <returns>An asynchronous task.</returns>
        Task Delete(int id);
    }
}

