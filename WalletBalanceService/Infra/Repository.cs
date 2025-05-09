using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WalletBalanceService.Domain;
using WalletBalanceService.Infra.EntityFramework;

namespace WalletBalanceService.Infra;

public class Repository : RepositoryBase<Balance>
{
    public Repository(BalanceDbContext dbContext) : base(dbContext)
    {
    }

    public Repository(BalanceDbContext dbContext, ISpecificationEvaluator specificationEvaluator) : base(dbContext, specificationEvaluator)
    {
    }
}
