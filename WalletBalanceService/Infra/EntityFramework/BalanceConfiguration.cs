using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WalletBalanceService.Domain;

namespace WalletBalanceService.Infra.EntityFramework;

public class BalanceConfiguration : IEntityTypeConfiguration<Balance>
{
    public void Configure(EntityTypeBuilder<Balance> builder)
    {
        builder.ToTable("balances");
        builder.HasKey(x => x.AccountId);

        builder.Property(x => x.AccountId)
            .HasColumnName("account_id")
            .HasColumnType("VARCHAR(255)");
        
        builder.Property(x => x.CurrentBalance)
            .HasColumnName("current_balance")
            .HasColumnType("DECIMAL(10,2)")
            .IsRequired();
        builder.Property(x => x.UpdatedAt)
            .HasColumnName("updated_at")
            .HasColumnType("DATETIME")
            .IsRequired();
    }
}
