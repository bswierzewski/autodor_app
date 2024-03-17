using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Entities.Settings;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Data;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options) { }

    public DbSet<IFirma> IFirmaSettings => Set<IFirma>();
    public DbSet<Polcar> PolcarSettings => Set<Polcar>();
    public DbSet<MongoDB> MongoDBSettings => Set<MongoDB>();
    public DbSet<UserSetting> UserSettings => Set<UserSetting>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}