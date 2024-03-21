using Domain.Entities.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class MongoDBSettingsConfiguration : IEntityTypeConfiguration<MongoDBSetting>
{
    public void Configure(EntityTypeBuilder<MongoDBSetting> builder)
    {
    }
}
