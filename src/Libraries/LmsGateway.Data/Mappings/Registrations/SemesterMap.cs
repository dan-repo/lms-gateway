using LmsGateway.Domain.Registrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LmsGateway.Data.Mappings.Registrations
{
    public class SemesterMap
    {
        public SemesterMap(EntityTypeBuilder<Semester> entityBuilder)
        {
            entityBuilder.ToTable("SEMESTER");

            entityBuilder.HasKey(x => x.Id);

            entityBuilder.Property(x => x.Name).IsRequired().HasMaxLength(100);

        }

    }
}
