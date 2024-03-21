using Domain.Entities.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class PolcarSettingsConfiguration : IEntityTypeConfiguration<PolcarSetting>
{
    public void Configure(EntityTypeBuilder<PolcarSetting> builder)
    {
    }
}
