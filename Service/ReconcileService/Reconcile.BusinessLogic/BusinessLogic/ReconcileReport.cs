using System.Linq;
using Reconcile.Entity;
using Reconcile.Repository;
using Microsoft.Extensions.Logging;

namespace Reconcile.BusinessLogic;

public class ReconcileReport : IReconcileReport
{
    private readonly ICounterPartyRepository _cpRepository;
    private readonly IInsuranceRepository _insuranceRepository;
    ILogger<ReconcileReport> _logger;

    public ReconcileReport(ICounterPartyRepository cpRepository, IInsuranceRepository insuranceRepository, ILogger<ReconcileReport> logger)
    {
        _insuranceRepository = insuranceRepository;
        _cpRepository = cpRepository;
        _logger = logger;
    }

    /// <summary>
    /// Performs reconciliation on the provided list of ARAP/JDE items.
    /// </summary>
    /// <param name="arapList">A list of ARAP/JDE items to be reconciled.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. 
    /// The task result contains an enumerable of ReconcileItem objects representing the reconciliation results.
    /// </returns>
    public async Task<IEnumerable<ReconcileItem>> Recon(IEnumerable<ArapJde> arapList)
    {
        var reconList = new List<ReconcileItem>();

        //Step 1. Group by supplier name
        var groupList = from item in arapList
                        group item by item.SupplierName;

        //Step 2. Calculate by sub group
        var insurances = await _insuranceRepository.GetAll();
        var cps = await _cpRepository.GetAll();
        foreach(var group in groupList)
        {
            var cp = cps.FirstOrDefault<CounterParty>(item => item.JdeCPName.Equals(group.Key) || item.SalesForceCpName.Equals(group.Key));
            var insurance = insurances.FirstOrDefault<Insurance>(item => item.CpName.Equals(group.Key));
            if(cp!=null)
            {
                var subArapList = group.ToList();
                var subReconList = await ReconByGroup(cp, insurance, subArapList);
                reconList.AddRange(subReconList);
            }else
            {
                _logger.LogWarning($"!!!{group.Key} can not be found");
            }
            
        }
        return reconList;
    }

    private async Task<IEnumerable<ReconcileItem>> ReconByGroup(CounterParty cp, Insurance? insurance, IEnumerable<ArapJde> arapList)
    {
        //Step 1. Calculate Insurance
        var count = arapList.Count();
        var reconList = from arap in arapList
                        select new ReconcileItem()
                        {
                            ContractNo = arap.ContractNo,
                            Description = arap.Description,
                            SfAcctTitle = arap.SupplierName,
                            SupplierCode = arap.SupplierCode,
                            SupplierName = arap.SupplierName,
                            AcCode = arap.AcCode,
                            AmountInCtrm = arap.AmountInCtrm,
                            AmountInJde = arap.AmountInJde,
                            DueDate = arap.DueDate,
                            Id = arap.Id,
                            ExpectedLoss=arap.AmountInJde* cp.PdRate,
                            PdRate =cp.PdRate,//Calculate Pd rate
                            Insurance = insurance == null ? false : true, //Calculate Insurance
                            InsuranceRate = insurance == null ? 0 : insurance.InsuranceRate
                        };
        var totalAmtInCtrmWithRate = reconList.Sum(x => x.AmountInCtrm * x.InsuranceRate);
        var rList = reconList.ToList();
        await Parallel.ForEachAsync(rList, async (item, cancellationToken) =>
        {
            //Step 2. Calculate insurance limi USD
            CalcInsuranceLimitUsd(item, insurance, count);
            //Step 3. Calculate Calculate Net exposure
            CalcInsuranceNetExposure(item, insurance, totalAmtInCtrmWithRate);
            await Task.CompletedTask;
        });

        return rList;
    }

    private void CalcInsuranceLimitUsd( ReconcileItem reconcileItem, Insurance? insurance, int count)
    {
        reconcileItem.InsuranceLimitUsd = reconcileItem.Insurance? (insurance?.LimitUsd??0)/Math.Max(count, 1): 0;
    }

    private void CalcInsuranceNetExposure( ReconcileItem reconcileItem, Insurance? insurance, decimal totalAmtInCtrmWithRate)
    {
        reconcileItem.NetExposure = reconcileItem.Insurance ?
            reconcileItem.InsuranceLimitUsd
            : reconcileItem.AmountInJde;
    }
}

