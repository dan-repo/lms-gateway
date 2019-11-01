
using LmsGateway.Domain.Registrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LmsGateway.Data.Mappings.Registrations
{
    public class DepartmentMap
    {
        public DepartmentMap(EntityTypeBuilder<Department> entityBuilder)
        {
            entityBuilder.ToTable("DEPARTMENT");

            entityBuilder.HasKey(x => x.Id);
                        
            entityBuilder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            entityBuilder.Property(x => x.Description).HasMaxLength(500);
            entityBuilder.Property(x => x.FacultyId).HasColumnName("Faculty_Id");
        }

    }
}
