using Domain.Entities;
using Domain.Entities.Settings;

namespace Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<IFirmaSetting> IFirmaSettings { get; }
    DbSet<PolcarSetting> PolcarSettings { get; }
    DbSet<MongoDBSetting> MongoDBSettings { get; }
    DbSet<UserSetting> UserSettings { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
