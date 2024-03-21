using Domain.Entities.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class IFirmaSettingsConfiguration : IEntityTypeConfiguration<IFirmaSetting>
{
    public void Configure(EntityTypeBuilder<IFirmaSetting> builder)
    {
    }
}
