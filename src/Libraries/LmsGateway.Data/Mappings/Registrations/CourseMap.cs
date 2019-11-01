
using LmsGateway.Domain.Registrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LmsGateway.Data.Mappings.Registrations
{
    public class CourseMap
    {
        public CourseMap(EntityTypeBuilder<Course> entityBuilder)
        {
            entityBuilder.ToTable("COURSE");

            entityBuilder.HasKey(x => x.Id);

            entityBuilder.Property(x => x.Code).HasColumnName("Course_Code").HasMaxLength(15).IsRequired();
            entityBuilder.Property(x => x.Name).IsRequired().HasMaxLength(60);
            entityBuilder.Property(u => u.IsOptional).HasColumnName("Is_Optional");
            entityBuilder.Property(u => u.LevelId).HasColumnName("Level_Id");
            entityBuilder.Property(u => u.SemesterId).HasColumnName("Semester_Id");
            entityBuilder.Property(u => u.ProgrammeId).HasColumnName("Programme_Id");
            entityBuilder.Property(u => u.DepartmentId).HasColumnName("Department_Id");
            entityBuilder.Property(x => x.CreatedOn).HasColumnName("Created_On");
            entityBuilder.Property(x => x.CreatedBy).HasColumnName("Created_By").HasMaxLength(450);

        }




    }
}
