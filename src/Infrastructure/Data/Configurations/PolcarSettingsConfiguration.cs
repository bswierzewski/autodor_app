using Domain.Entities.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class PolcarSettingsConfiguration : IEntityTypeConfiguration<Polcar>
{
    public void Configure(EntityTypeBuilder<Polcar> builder)
    {
    }
}
