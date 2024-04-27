using Reconcile.BusinessLogic;
using Reconcile.Entity;
using Reconcile.Repository;
using Moq;
using System.Data.Common;
using Xunit;
using Microsoft.Extensions.Logging;

namespace Reconcile.Test;

public class ReconcileReportTest
{
    private const string ValideSFSupplierName = "Steel Limited Test";
    private const string ValideSupplierName = "CHINA Global";
    private const string InvalideSupplierName = "Steel Limited Test";

    private const string ValidInsuranceLimitUsd = "53215";
    private const string InvalidInsuranceLimitUsd = "0";

    private const string ValidNetExposure = "53215";
    private const string InValidNetExposure = "0";

    private const decimal AmountInJDE = 123M;
    private const decimal AmountInCTRM = 1234M;

    private const decimal ValidPdRate = 0.36M;
    private const decimal ValidInsuranceRate = 0.9M;
    private const decimal ValidLimitUsd = 53215M;


    private readonly ReconcileReport _reconcileReport;
    private readonly Mock<IInsuranceRepository> _insuranceRepository;
    private readonly Mock<ICounterPartyRepository> _cpRepository;
    private readonly Mock<ILogger<ReconcileReport>> _logger;
    private List<ArapJde> _arapList= new List<ArapJde>();

    public ReconcileReportTest()
    {
        _insuranceRepository = new Mock<IInsuranceRepository>();
        _cpRepository = new Mock<ICounterPartyRepository>();
        _logger = new Mock<ILogger<ReconcileReport>>();
        _reconcileReport = new ReconcileReport(_cpRepository.Object, _insuranceRepository.Object, _logger.Object);
        
    }

    /*
    Section #1. Calcuate the Insurance value

    Scenario #1. Calculate Insurance value
    Given Supplier_name exists in Insurance based on cp_name column
        Or SF_Acct_Title exists in Insurance based on cp_name column
    When reconcile report API is invoked
    Then set the Insurance=1

    Scenario #2. Calculate Insurance value
    Given Supplier_name not exists in Insurance based on cp_name column
        and SF_Acct_Title not exists in Insurance based on cp_name column
    When reconcile report API is invoked
    Then set the Insurance=0
    */

    [Theory]
    [InlineData(ValideSupplierName, true)]
    [InlineData(InvalideSupplierName, false)]
    public async void IsInsuranceLogicCorrect(string supplierName, bool insurance)
    {
        //1. Arrange
        GivenInsuranceRepositoryIsReady();GivenCounterPartyRepositoryIsReady();
        GivenOneSupplierNameArapList(supplierName, AmountInJDE, AmountInCTRM);

        //2. Act
        var reconResult = await _reconcileReport.Recon(_arapList);

        //3.Assert
        var reconItem = reconResult.FirstOrDefault();
        Assert.Equal(insurance, reconItem?.Insurance);

    }


    /*Section #2. Calculate insurance limi USD

    Scenario 1. SF_Acct_Tittle exists in Insurance
        Given SF_Acct_Tittle exists in Insurance based on cp_name column
    When reconcile report API is invoked
    Then Insurance Limit USD = Insurance.limit__c_usd/ count(SF_Acct_Tittle)

    Scenario 2. SF_Acct_Tittle doesn't exists in Insurance

    Given SF_Acct_Tittle doesn't exists in Insurance based on cp_name column
    When reconcile report API is invoked
    Then Insurance Limit USD = 0 
    */
    [Theory]
    [InlineData(ValideSupplierName, ValidInsuranceLimitUsd)]
    [InlineData(InvalideSupplierName, InvalidInsuranceLimitUsd)]
    public async void IsInsuranceLimitUsdCorrect(string supplierName, string insuranceLimitUsd)
    {
        //1. Arrange
        GivenInsuranceRepositoryIsReady();GivenCounterPartyRepositoryIsReady();
        GivenOneSupplierNameArapList(supplierName, AmountInJDE, AmountInCTRM);

        //2. Act
        var reconResult = await _reconcileReport.Recon(_arapList);

        //3.Assert
        var reconItem = reconResult.FirstOrDefault();
        Assert.Equal(insuranceLimitUsd, reconItem?.InsuranceLimitUsd.ToString());
    }

    /*
     Section #3. Calculate Net exposure
        Scenario 1. Insurance=0
	        Given Insurance=0
        When reconcile report API is invoked
        Then Net_Exposure=Amount_In_JDE

        Scenario 2. Insurance<>0
	        Given Insurance<>0 
	        and sum(Amount_In_CTRM*InsuranceRate%)<sum(Insurance_limit_usd)
        When reconcile report API is invoked
        Then Net_Exposure = Amount_In_CTRM*(1-InsuranceRate%)

        Scenario 3. Insurance<>0
	        Given Insurance<>0 
	        and sum(Amount_In_CTRM*InsuranceRate%)>=sum(Insurance_limit_usd)
        When reconcile report API is invoked
        Then Net_Exposure = Insurance_limit_usd
     * */
    [Theory]
    [InlineData(InvalideSupplierName,"1", "123")]
    [InlineData(ValideSupplierName,"1", "53215")]
    [InlineData(ValideSupplierName, "1111111111", "53215")]
    public async void IsNet_ExposureCorrect(string supplierName, string amountInCTRM, string insuranceLimitUsd)
    {
        //1. Arrange
        GivenInsuranceRepositoryIsReady();GivenCounterPartyRepositoryIsReady();
        GivenOneSupplierNameArapList(supplierName, AmountInJDE, decimal.Parse(amountInCTRM));

        //2. Act
        var reconResult = await _reconcileReport.Recon(_arapList);

        //3.Assert
        var reconItem = reconResult.FirstOrDefault();
        Assert.Equal(insuranceLimitUsd, reconItem?.NetExposure.ToString());
    }

    [Theory]
    [InlineData(InvalideSupplierName, "44.28")] //
    [InlineData(ValideSupplierName, "44.28")]  //
    public async void IsExpectedLossCorrect(string supplierName, string expectedLoss)
    {
        //1. Arrange
        GivenInsuranceRepositoryIsReady();GivenCounterPartyRepositoryIsReady();
        GivenOneSupplierNameArapList(supplierName, AmountInJDE, AmountInCTRM);

        //2. Act
        var reconResult = await _reconcileReport.Recon(_arapList);

        //3.Assert
        var reconItem = reconResult.FirstOrDefault();
        Assert.Equal(expectedLoss, reconItem?.ExpectedLoss.ToString());
    }

    private void GivenInsuranceRepositoryIsReady()
    {
        _insuranceRepository.Setup(x => x.GetAll()).ReturnsAsync(new List<Insurance>() {
            new Insurance() { CpName = ValideSupplierName, BizDate= DateTime.Now,
                InsuranceRate=ValidInsuranceRate, PdRate= ValidPdRate, LimitUsd=ValidLimitUsd
            } });
    }

    private void GivenCounterPartyRepositoryIsReady()
    {
        _cpRepository.Setup(x => x.GetAll()).ReturnsAsync(new List<CounterParty>() {
            new CounterParty() {
                 JdeCPName=ValideSupplierName,
                 PdRate= ValidPdRate,
                 SalesForceCpName=ValideSFSupplierName,
            } });
    }

    private void GivenOneSupplierNameArapList(string supplierName, decimal amtInJde, decimal amtInCtrm)
    {
        _arapList = new List<ArapJde>()
        {
            new ArapJde(){ Id=1, AcCode="AcCode", ContractNo="ContractNo", Description="Desc", SupplierCode="SupplierCode",
                SupplierName=supplierName, AmountInCtrm=amtInCtrm, AmountInJde=amtInJde, DueDate=DateTime.Now},
        };
    }


}
