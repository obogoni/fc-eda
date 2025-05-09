namespace WalletBalanceService.Infra;

public interface IDateTimeService { 
    DateTime Now { get; }
    DateTime Today { get; }
}