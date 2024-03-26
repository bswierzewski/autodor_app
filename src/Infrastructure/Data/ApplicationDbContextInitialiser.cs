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

        //if (!_context.MongoDBSettings.Any())
        //{
        //    var mongoDBSettings = new List<MongoDBSetting>
        //{
        //    new MongoDBSetting
        //    {
        //        ConnectionURI = "ConnectionURI_mongus",
        //        DatabaseName = "DatabaseName_mongus",
        //        CollectionName = "mongus"
        //    },
        //    new MongoDBSetting
        //    {
        //        ConnectionURI = "ConnectionURI_poslwr",
        //        DatabaseName = "DatabaseName_poslwr",
        //        CollectionName = "poslwr"
        //    },
        //};

        //    await _context.AddRangeAsync(mongoDBSettings);

        //    await _context.SaveChangesAsync();
        //}
    }
}
