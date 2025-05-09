using Microsoft.EntityFrameworkCore;

namespace WalletBalanceService.Infra.EntityFramework;

public class BalanceDbContext(DbContextOptions<BalanceDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BalanceDbContext).Assembly);
    }
}
