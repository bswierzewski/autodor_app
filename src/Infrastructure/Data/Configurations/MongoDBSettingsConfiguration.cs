using Domain.Entities.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class MongoDBSettingsConfiguration : IEntityTypeConfiguration<MongoDB>
{
    public void Configure(EntityTypeBuilder<MongoDB> builder)
    {
    }
}
