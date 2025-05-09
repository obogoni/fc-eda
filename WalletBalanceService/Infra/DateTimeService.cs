namespace WalletBalanceService.Infra;

public class DateTimeService : IDateTimeService
{
    public DateTime Now => DateTime.UtcNow;
    
    public DateTime Today => Now.Date;
}