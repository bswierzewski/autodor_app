using Domain.Entities;
using Domain.Entities.Settings;

namespace Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<IFirma> IFirmaSettings { get; }
    DbSet<Polcar> PolcarSettings { get; }
    DbSet<MongoDB> MongoDBSettings { get; }
    DbSet<UserSetting> UserSettings { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
