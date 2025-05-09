using Ardalis.Specification;
using WalletBalanceService.Domain;
using WalletBalanceService.Specifications;

namespace WalletBalanceService.Features.GetBalance;

public class GetBalanceService(IRepositoryBase<Balance> repo, ILogger<GetBalanceService> logger)
    : IGetBalanceService
{
    public async Task<BalanceResponse?> GetBalance(string id, CancellationToken cancellationToken = default)
    {
        var balance = await repo.SingleOrDefaultAsync(new AccountByIdSpec(id), cancellationToken);
        if (balance == null) return null;
        
        return new BalanceResponse
        {
            CurrentBalance = balance.CurrentBalance,
            AccountId = balance.AccountId,
            UpdatedAt = balance.UpdatedAt
        };
    }
}
