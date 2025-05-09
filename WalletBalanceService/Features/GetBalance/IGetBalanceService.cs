namespace WalletBalanceService.Features.GetBalance;

public interface IGetBalanceService
{
    Task<BalanceResponse?> GetBalance(string id, CancellationToken cancellationToken);
}
