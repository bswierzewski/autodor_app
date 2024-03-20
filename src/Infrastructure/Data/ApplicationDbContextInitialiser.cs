using Domain.Entities.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data;

public static class InitialiserExtensions
{
    public static async Task InitialiseDatabaseAsync(this IHost host)
    {
        using var scope = host.Services.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

        await initialiser.InitialiseAsync();

        await initialiser.SeedAsync();
    }
}

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            await _context.Database.EnsureDeletedAsync();
            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default data
        // Seed, if necessary

        if (!_context.IFirmaSettings.Any())
        {
            var settings = new List<IFirma> 
            {
                new IFirma
                {
                    Email = "test@test.com",
                    FakturaApiKey = "123456123456"
                },
                new IFirma
                {
                    Email = "api@api.com",
                    FakturaApiKey = "qwertyqwerty"
                },
            };

            await _context.AddRangeAsync(settings);

            await _context.SaveChangesAsync();
        }

        if (!_context.PolcarSettings.Any())
        {
            var settings = new List<Polcar>
            {
                new Polcar
                {
                    Id = 1,
                    BranchId = 11,
                    LanguageId = 111,
                    Login = "login_1",
                    Password = "password_1",
                    DistributorCode = "MTP"
                },
                new Polcar
                {
                    Id = 2,
                    BranchId = 22,
                    LanguageId = 222,
                    Login = "login_2",
                    Password = "password_2",
                    DistributorCode = "SWE"
                },
            };

            await _context.AddRangeAsync(settings);

            await _context.SaveChangesAsync();
        }

        if (!_context.MongoDBSettings.Any())
        {
            var settings = new List<MongoDB>
            {
                new MongoDB
                {
                    Id = 1,
                    ConnectionURI = "ConnectionURI_mongus",
                    DatabaseName = "DatabaseName_mongus",                    
                    CollectionName = "mongus"
                },
                new MongoDB
                {
                    Id = 2,
                    ConnectionURI = "ConnectionURI_poslwr",
                    DatabaseName = "DatabaseName_poslwr",
                    CollectionName = "poslwr"
                },
            };

            await _context.AddRangeAsync(settings);

            await _context.SaveChangesAsync();
        }
    }
}
