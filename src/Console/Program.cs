using Application.Common.Interfaces;
using Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var configuration = CreateConfigurationBuilder()
    .Build();

using IHost host = CreateHostBuilder()
    .Build();

using var scope = host.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    await services.GetRequiredService<App>().Run();
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}

IHostBuilder CreateHostBuilder()
{
    return Host.CreateDefaultBuilder()
    .ConfigureServices(services =>
    {

        services.AddApplicationServices(configuration);
        services.AddInfrastructureServices(configuration);

        services.AddScoped<IUser, User>();
        services.AddScoped<App>();

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