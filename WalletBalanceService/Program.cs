using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using WalletBalanceService.Domain;
using WalletBalanceService.Features.GetBalance;
using WalletBalanceService.Features.UpdateBalance;
using WalletBalanceService.Infra;
using WalletBalanceService.Infra.EntityFramework;
using WalletBalanceService.Kafka;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging(config => config.AddConsole());

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

Console.WriteLine(connectionString);

var serverVersion = ServerVersion.AutoDetect(connectionString);

builder.Services.AddDbContext<BalanceDbContext>(options =>
    options.UseMySql(connectionString, serverVersion).LogTo(Console.WriteLine, LogLevel.Information)
);

builder.Services.AddSingleton<IDateTimeService, DateTimeService>();
builder.Services.AddScoped<IRepositoryBase<Balance>, Repository>();
builder.Services.AddScoped<IGetBalanceService, GetBalanceService>();
builder.Services.AddScoped<IUpdateBalanceService, UpdateBalanceService>();
builder.Services.Configure<KafkaSettings>(builder.Configuration.GetSection("Kafka"));
builder.Services.AddHostedService<UpdatedBalanceConsumer>();

var app = builder.Build();

app.UseGetBalance();

app.Run();
