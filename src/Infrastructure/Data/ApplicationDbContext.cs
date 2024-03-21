using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Entities.Settings;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Data;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options) { }

    public DbSet<IFirmaSetting> IFirmaSettings => Set<IFirmaSetting>();
    public DbSet<PolcarSetting> PolcarSettings => Set<PolcarSetting>();
    public DbSet<MongoDBSetting> MongoDBSettings => Set<MongoDBSetting>();
    public DbSet<UserSetting> UserSettings => Set<UserSetting>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}