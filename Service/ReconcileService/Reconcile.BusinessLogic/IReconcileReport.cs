using System;
using Reconcile.Entity;

namespace Reconcile.BusinessLogic
{
    public interface IReconcileReport
    {
        /// <summary>
        /// Performs reconciliation on the provided list of ARAP/JDE items.
        /// </summary>
        /// <param name="arapList">A list of ARAP/JDE items to be reconciled.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. 
        /// The task result contains an enumerable of ReconcileItem objects representing the reconciliation results.
        /// </returns>
        public Task<IEnumerable<ReconcileItem>> Recon(IEnumerable<ArapJde> arapList);
    }
}

