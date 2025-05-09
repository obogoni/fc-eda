namespace WalletBalanceService.Features;

public record BalanceResponse
{
    public string AccountId { get; set; } = null!;

    public decimal CurrentBalance { get; set; }

    public DateTime UpdatedAt { get; set; }
}
