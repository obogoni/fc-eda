namespace WalletBalanceService.Infra.Kafka;

public class KafkaEvent<T> where T : class
{
    public string Name { get; set; }
    
    public T Payload { get; set; }
}