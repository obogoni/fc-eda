using Ardalis.GuardClauses;
using Ardalis.Specification;
using WalletBalanceService.Domain;
using WalletBalanceService.Infra;
using WalletBalanceService.Specifications;

namespace WalletBalanceService.Features.UpdateBalance;

public class UpdateBalanceService(ILogger<UpdateBalanceService> logger, IRepositoryBase<Balance> repo, IDateTimeService dateTimeService) : IUpdateBalanceService
{
    public async Task UpdateBalance(string id, decimal currentBalance, CancellationToken cancellationToken)
    {
        Guard.Against.NullOrWhiteSpace(id, nameof(id));
        
        var found = await repo.FirstOrDefaultAsync(new AccountByIdSpec(id), cancellationToken);

        if (found != null)
        {
            found.UpdateBalance(currentBalance, dateTimeService.Now);
        }
        else
        {
            var newBalance = Balance.Create(id, currentBalance, dateTimeService.Now);
            
            await repo.AddAsync(newBalance, cancellationToken);
        }

        await repo.SaveChangesAsync(cancellationToken);
    }
}