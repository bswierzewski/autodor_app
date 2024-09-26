using Application.Common.Interfaces;
using Application.Interfaces;
using Infrastructure.Data;
using Infrastructure.Data.Interceptors;
using Infrastructure.Services;
using Infrastructure.Services.Cache;
using Infrastructure.Services.Generator;
using Infrastructure.Services.Polcar;
using Infrastructure.Services.SendGrid;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System.Reflection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddSingleton(TimeProvider.System);

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<ApplicationDbContextInitialiser>();

        services.AddScoped<IDistributorsSalesService, DistributorsSalesService>();
        services.AddScoped<IProductsService, ProductsService>();
        services.AddScoped<IFirmaService, FirmaService>();
        services.AddScoped<IContractorService, ContractorService>();
        services.AddScoped<ISendGridService, SendGridService>();
        services.AddScoped<IHtmlGeneratorService, HtmlGeneratorService>();

        services.AddMemoryCache();
        services.AddScoped<ICacheService, CacheService>();

        services.AddSingleton(s 
            => new MongoClient(configuration["Credentials:MongoDB:ConnectionURI"]).GetDatabase(configuration["Credentials:MongoDB:DatabaseName"]));

        services.AddScoped<IPDFGeneratorService, PDFGeneratorService>();

        return services;
    }
}

