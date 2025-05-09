using Ardalis.Specification;
using WalletBalanceService.Domain;

namespace WalletBalanceService.Specifications;

public class AccountByIdSpec : Specification<Balance>, ISingleResultSpecification<Balance>
{
    public AccountByIdSpec(string id)
    {
        Query.Where(balance => balance.AccountId == id);
    }
}