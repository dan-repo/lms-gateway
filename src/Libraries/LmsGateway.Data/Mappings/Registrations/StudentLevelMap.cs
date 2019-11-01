using LmsGateway.Domain.Registrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LmsGateway.Data.Mappings.Registrations
{
    public class StudentLevelMap
    {
        public StudentLevelMap(EntityTypeBuilder<StudentLevel> entityBuilder)
        {
            entityBuilder.ToTable("STUDENT_LEVEL");

            entityBuilder.HasKey(x => x.Id);

            entityBuilder.Property(x => x.StudentId).HasColumnName("Student_Id").HasMaxLength(450).IsRequired();
            entityBuilder.Property(x => x.LevelId).HasColumnName("Level_Id");
            entityBuilder.Property(x => x.CreatedOn).HasColumnName("Created_On");
            entityBuilder.Property(x => x.CreatedBy).HasColumnName("Created_By").HasMaxLength(450);


        }

    }
}
