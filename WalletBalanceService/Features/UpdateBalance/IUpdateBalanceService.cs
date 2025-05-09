namespace WalletBalanceService.Features.UpdateBalance;

public interface IUpdateBalanceService
{
    Task UpdateBalance(string id, decimal amount, CancellationToken cancellationToken);
}