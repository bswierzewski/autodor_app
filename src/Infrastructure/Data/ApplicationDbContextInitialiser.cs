using Domain.Entities;
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

        #region Data

        var iFirmaSettings = new List<IFirmaSetting>
        {
            new IFirmaSetting
            {
                Email = "test@test.com",
                FakturaApiKey = "123456123456"
            },
            new IFirmaSetting
            {
                Email = "api@api.com",
                FakturaApiKey = "qwertyqwerty"
            },
        };

        var polcarSettings = new List<PolcarSetting>
        {
            new PolcarSetting
            {
                BranchId = 11,
                LanguageId = 111,
                Login = "login_1",
                Password = "password_1",
                DistributorCode = "MTP"
            },
            new PolcarSetting
            {
                BranchId = 22,
                LanguageId = 222,
                Login = "login_2",
                Password = "password_2",
                DistributorCode = "SWE"
            },
        };

        var mongoDBSettings = new List<MongoDBSetting>
        {
            new MongoDBSetting
            {
                ConnectionURI = "ConnectionURI_mongus",
                DatabaseName = "DatabaseName_mongus",
                CollectionName = "mongus"
            },
            new MongoDBSetting
            {
                ConnectionURI = "ConnectionURI_poslwr",
                DatabaseName = "DatabaseName_poslwr",
                CollectionName = "poslwr"
            },
        };

        var userSettings = new List<UserSetting>
        {
            new UserSetting
            {
                Email = "test@test.com",
                IFirmaSetting = iFirmaSettings[0],
                MongoDBSetting = mongoDBSettings[0],
                PolcarSetting = polcarSettings[0]
            },
            new UserSetting
            {
                Email = "two@two.net.com",
                IFirmaSetting = iFirmaSettings[1],
                MongoDBSetting = mongoDBSettings[1],
                PolcarSetting = polcarSettings[1]
            },
        };

        #endregion

        #region Context

        // IFirma settings
        if (!_context.IFirmaSettings.Any())
        {
            await _context.AddRangeAsync(iFirmaSettings);

            await _context.SaveChangesAsync();
        }

        // Polcar settings
        if (!_context.PolcarSettings.Any())
        {
            await _context.AddRangeAsync(polcarSettings);

            await _context.SaveChangesAsync();
        }

        // MongoDB settings
        if (!_context.MongoDBSettings.Any())
        {
            await _context.AddRangeAsync(mongoDBSettings);

            await _context.SaveChangesAsync();
        }

        // User settings
        if (!_context.UserSettings.Any())
        {
            await _context.AddRangeAsync(userSettings);

            await _context.SaveChangesAsync();
        }

        #endregion
    }
}
