using System;
using System.Text.Json.Serialization;

namespace Reconcile.Entity
{
	public class ArapJde
	{
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("ac_code")]
        public required string AcCode { get; set; }

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
    }
}

