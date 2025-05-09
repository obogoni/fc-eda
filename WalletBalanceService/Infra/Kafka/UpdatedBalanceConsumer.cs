using System.Text.Json;
using System.Text.Json.Serialization;
using Confluent.Kafka;
using Microsoft.Extensions.Options;
using WalletBalanceService.Features.UpdateBalance;
using WalletBalanceService.Infra.Kafka;

namespace WalletBalanceService.Kafka;

public class UpdatedBalanceConsumer(
    ILogger<UpdatedBalanceConsumer> logger,
    IOptions<KafkaSettings> kafkaOptions,
    IServiceScopeFactory serviceScopeFactory)
    : BackgroundService
{
    private readonly KafkaSettings kafkaSettings = kafkaOptions.Value;
    private IConsumer<Ignore, string> consumer = null!;
    private readonly IServiceScopeFactory serviceScopeFactory = serviceScopeFactory;

    public override Task StartAsync(CancellationToken cancellationToken)
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = kafkaSettings.BootstrapServers,
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = true,
            GroupId = kafkaSettings.GroupId,
        };

        consumer = new ConsumerBuilder<Ignore, string>(config).Build();
        consumer.Subscribe(kafkaSettings.Topic);

        logger.LogInformation(
            "Kafka consumer started and subscribed to topic: {Topic}",
            kafkaSettings.Topic
        );

        return base.StartAsync(cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Yield();

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                logger.LogInformation("Listening for events ...");

                var result = consumer.Consume(stoppingToken);
                if (result == null || string.IsNullOrWhiteSpace(result.Message?.Value))
                    continue;

                logger.LogInformation(result.Message.Value);

                var @event = JsonSerializer.Deserialize<KafkaEvent<UpdatedBalancePayload>>(result.Message.Value);
                var payload = @event!.Payload;
                
                using var serviceScope = serviceScopeFactory.CreateScope();
                var updateBalanceService = serviceScope.ServiceProvider.GetRequiredService<IUpdateBalanceService>();

                await updateBalanceService.UpdateBalance(payload!.AccountIdFrom, payload!.BalanceAccountFrom, stoppingToken);
                await updateBalanceService.UpdateBalance(payload!.AccountIdTo, payload!.BalanceAccountTo, stoppingToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error processing Kafka message");
            }
        }
    }
}
