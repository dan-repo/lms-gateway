using LmsGateway.Domain.Registrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LmsGateway.Data.Mappings.Registrations
{
    public class LevelMap
    {
        public LevelMap(EntityTypeBuilder<Level> entityBuilder)
        {
            entityBuilder.ToTable("LEVEL");

            entityBuilder.HasKey(x => x.Id);

            entityBuilder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            entityBuilder.Property(x => x.Description).HasMaxLength(500);

        }

    }
}
