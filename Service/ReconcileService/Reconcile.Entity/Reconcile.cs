using System;
using System.Text.Json.Serialization;

namespace Reconcile.Entity
{
	public class ReconcileItem
	{
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("ac_code")]
        public string AcCode { get; set; }

        [JsonPropertyName("description")]
        public required string Description { get; set; }

        [JsonPropertyName("supplier_code")]
        public required string SupplierCode { get; set; }

        [JsonPropertyName("supplier_name")]
        public required string SupplierName { get; set; }

        [JsonPropertyName("contract_no")]
        public required string ContractNo { get; set; }

        [JsonPropertyName("due_date")]
        public DateTime DueDate { get; set; }

        [JsonPropertyName("amount_in_ctrm")]
        public decimal AmountInCtrm { get; set; }

        [JsonPropertyName("amount_in_jde")]
        public decimal AmountInJde { get; set; }

        [JsonPropertyName("pd_rate")]
        public decimal PdRate { get; set; }

        [JsonPropertyName("expected_loss")]
        public decimal ExpectedLoss { get; set; }

        [JsonPropertyName("sf_acct_title")]
        public required string SfAcctTitle { get; set; }

        [JsonPropertyName("insurance")]
        public bool Insurance { get; set; }

        [JsonPropertyName("insurance_rate")]
        public decimal InsuranceRate { get; set; }

        [JsonPropertyName("insurance_limit_usd")]
        public decimal InsuranceLimitUsd { get; set; }

        [JsonPropertyName("net_exposure")]
        public decimal NetExposure { get; set; }
    }
}

