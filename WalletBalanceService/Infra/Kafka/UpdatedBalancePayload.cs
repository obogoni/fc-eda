using System.Text.Json.Serialization;

namespace WalletBalanceService.Infra.Kafka;

public record UpdatedBalancePayload
{
    [JsonPropertyName("account_id_from")]
    public string AccountIdFrom { get; set; } = null!;

    [JsonPropertyName("balance_account_id_from")]
    public decimal BalanceAccountFrom { get; set; }

    [JsonPropertyName("account_id_to")]
    public string AccountIdTo { get; set; } = null!;
    
    [JsonPropertyName("balance_account_id_to")]
    public decimal BalanceAccountTo { get; set; }
}
