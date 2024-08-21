using Application.Common.Interfaces;
using Application.Interfaces;
using Application.Invoices.Commands.CreateAllInvoices;
using Infrastructure;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

var configuration = CreateConfigurationBuilder()
    .Build();

using IHost host = CreateHostBuilder()
    .Build();

/// Logger configuration
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File(
        path: Path.Combine("Logs", "log.txt"),
        rollingInterval: RollingInterval.Day
        )
    .CreateLogger();

using var scope = host.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    await services.GetRequiredService<ISender>()
        .Send(new CreateAllInvoicesCommand
        {
            DateFrom = DateTime.Now.AddDays(-14),
            DateTo = DateTime.Now
        });
}
catch (Exception e)
{
    Log.Logger.Error(e, e.Message);

    await services.GetRequiredService<ISendGridService>().SendEmail(["swierzewski.bartosz@gmail.com"], "Error", e.Message);
}

IHostBuilder CreateHostBuilder()
{
    return Host.CreateDefaultBuilder()
    .ConfigureServices(services =>
    {
        services.AddApplicationServices(configuration);
        services.AddInfrastructureServices(configuration);

        services.AddSerilog();

        services.AddScoped<IUser, User>();
    });
}

IConfigurationBuilder CreateConfigurationBuilder()
{
    return new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development"}.json", optional: true)
        .AddEnvironmentVariables();
}