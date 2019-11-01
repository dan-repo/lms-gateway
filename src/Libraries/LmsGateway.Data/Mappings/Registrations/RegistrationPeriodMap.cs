using LmsGateway.Domain.Registrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LmsGateway.Data.Mappings.Registrations
{
    public class RegistrationPeriodMap
    {
        public RegistrationPeriodMap(EntityTypeBuilder<RegistrationPeriod> entityBuilder)
        {
            entityBuilder.ToTable("REGISTRATION_PERIOD");

            entityBuilder.HasKey(x => x.Id);

            entityBuilder.Property(x => x.StartDate).HasColumnName("Start_Date");
            entityBuilder.Property(x => x.EndDate).HasColumnName("End_Date");
            entityBuilder.Property(x => x.CreatedOn).HasColumnName("Created_On");
            entityBuilder.Property(x => x.CreatedBy).HasColumnName("Created_By").HasMaxLength(450).IsRequired();
            entityBuilder.Property(x => x.SessionId).HasColumnName("Session_Id");
            entityBuilder.Property(x => x.SemesterId).HasColumnName("Semester_Id");

            entityBuilder.HasMany(x => x.RegistrationFees)
                .WithOne(x => x.RegistrationPeriod)
                .OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Restrict);


        }

    }
}
