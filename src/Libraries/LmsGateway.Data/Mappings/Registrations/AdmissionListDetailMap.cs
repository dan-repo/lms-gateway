using LmsGateway.Domain.Registrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LmsGateway.Data.Mappings.Registrations
{
    public class AdmissionListDetailMap
    {
        public AdmissionListDetailMap(EntityTypeBuilder<AdmissionListDetail> entityBuilder)
        {
            entityBuilder.ToTable("ADMISSION_LIST_DETAIL");

            entityBuilder.HasKey(x => x.Id);

            entityBuilder.Property(x => x.AdmissionListId).HasColumnName("Admission_List_Id");
            entityBuilder.Property(x => x.RegNo).HasColumnName("Reg_No").HasMaxLength(20).IsRequired();
            entityBuilder.Property(x => x.FirstName).HasColumnName("First_Name").HasMaxLength(20).IsRequired();
            entityBuilder.Property(u => u.Surname).HasColumnName("Surname").HasMaxLength(20).IsRequired();
            entityBuilder.Property(u => u.OtherName).HasColumnName("Other_Name").HasMaxLength(20);
            entityBuilder.Property(x => x.LevelId).HasColumnName("Level_Id");
            entityBuilder.Property(x => x.ProgrammeId).HasColumnName("Programme_Id");
            entityBuilder.Property(u => u.DepartmentId).HasColumnName("Department_Id");
            entityBuilder.Property(u => u.SessionId).HasColumnName("Session_Id");

            
                       
        }
    }



}
