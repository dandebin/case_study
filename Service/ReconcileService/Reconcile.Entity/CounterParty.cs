using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Reconcile.Entity;

public class CounterParty
{
    //[Required]
    [JsonPropertyName("id")]
    public int Id { get; set; }

    //[Required]
    [JsonPropertyName("abcode_number")]
    public string AbCodeNumber { get; set; }

    //[Required]
    [JsonPropertyName("sales_force_name")]
    public required string SalesForceCpName { get; set; }

    //[Required]
    [JsonPropertyName("jde_cp_name")]
    public required string JdeCPName { get; set; }

    //[Required]
    [JsonPropertyName("pd_rate")]
    public required decimal PdRate { get; set; }
}

