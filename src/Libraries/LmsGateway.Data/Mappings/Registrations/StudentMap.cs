using LmsGateway.Domain.Registrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LmsGateway.Data.Mappings.Registrations
{
    public class StudentMap
    {
        public StudentMap(EntityTypeBuilder<Student> entityBuilder)
        {
            entityBuilder.ToTable("STUDENT");

            entityBuilder.HasKey(x => x.Id);

            entityBuilder.Property(x => x.Id).HasMaxLength(450);
            entityBuilder.Property(x => x.RegNo).HasColumnName("Reg_No").HasMaxLength(50);
            entityBuilder.Property(x => x.Status);

            entityBuilder.Property(x => x.ProgrammeId).HasColumnName("Programme_Id");
            entityBuilder.Property(x => x.DepartmentId).HasColumnName("Department_Id");
            entityBuilder.Property(x => x.CurrentLevelId).HasColumnName("Current_Level_Id");
            entityBuilder.Property(x => x.AdmissionListDetailId).HasColumnName("Admission_List_Detail_Id");
            entityBuilder.Property(x => x.CreatedOn).HasColumnName("Created_On");

            entityBuilder.HasMany(x => x.LevelHistory)
                .WithOne(x => x.Student)
                .OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Restrict);


        }

    }
}
