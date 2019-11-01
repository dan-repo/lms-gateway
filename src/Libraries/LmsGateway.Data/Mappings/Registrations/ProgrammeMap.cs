using LmsGateway.Domain.Registrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LmsGateway.Data.Mappings.Registrations
{
    public class ProgrammeMap
    {
        public ProgrammeMap(EntityTypeBuilder<Programme> entityBuilder)
        {
            entityBuilder.ToTable("PROGRAMME");

            entityBuilder.HasKey(x => x.Id);

            entityBuilder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            entityBuilder.Property(x => x.Alias).HasMaxLength(20);

        }

    }
}
