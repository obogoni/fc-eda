namespace WalletBalanceService.Domain;

public class Balance
{
    public string AccountId { get; set; } = null!;

    public decimal CurrentBalance { get; set; }

    public DateTime UpdatedAt { get; set; }

    public static Balance Create(string accountId, decimal currentBalance, DateTime updatedAt)
    {
        return new Balance
        {
            AccountId = accountId,
            CurrentBalance = currentBalance,
            UpdatedAt = updatedAt,
        };
    }

    public void UpdateBalance(decimal amount, DateTime updatedAt)
    {
        CurrentBalance = amount;
        UpdatedAt = updatedAt;
    }
}
