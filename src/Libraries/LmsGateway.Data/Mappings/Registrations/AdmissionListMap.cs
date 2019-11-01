
using LmsGateway.Domain.Registrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LmsGateway.Data.Mappings.Registrations
{
    public class AdmissionListMap
    {
        public AdmissionListMap(EntityTypeBuilder<AdmissionList> entityBuilder)
        {
            entityBuilder.ToTable("ADMISSION_LIST");

            entityBuilder.HasKey(x => x.Id);

            entityBuilder.Property(x => x.MediaId).HasColumnName("Media_Id");
            entityBuilder.Property(x => x.SessionId).HasColumnName("Session_Id");
            entityBuilder.Property(u => u.DatePosted).HasColumnName("Date_Posted");
            entityBuilder.Property(u => u.PostedBy).HasColumnName("Posted_By").HasMaxLength(450);
           

        }
    }



}
