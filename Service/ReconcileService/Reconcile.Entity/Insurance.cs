using System;
using System.Text.Json.Serialization;

namespace Reconcile.Entity
{
	public class Insurance
	{
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("biz_date")]
        public DateTime BizDate { get; set; }

        [JsonPropertyName("cp_master_id")]
        public string CpMasterId { get; set; }

        [JsonPropertyName("cp_name")]
        public string CpName { get; set; }

        [JsonPropertyName("limit_c_usd")]
        public decimal LimitUsd { get; set; }

        [JsonPropertyName("pd_rate")]
        public decimal PdRate { get; set; }

        [JsonPropertyName("insurance_rate")]
        public decimal InsuranceRate { get; set; }
    }
}

