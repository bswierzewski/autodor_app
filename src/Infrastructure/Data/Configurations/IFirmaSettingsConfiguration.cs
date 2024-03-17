using Domain.Entities.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class IFirmaSettingsConfiguration : IEntityTypeConfiguration<IFirma>
{
    public void Configure(EntityTypeBuilder<IFirma> builder)
    {
    }
}
