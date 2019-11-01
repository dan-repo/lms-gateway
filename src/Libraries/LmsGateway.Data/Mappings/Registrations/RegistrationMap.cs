using Microsoft.EntityFrameworkCore;
using LmsGateway.Domain.Registrations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LmsGateway.Data.Mappings.Registrations
{
    public class RegistrationMap
    {
        public RegistrationMap(EntityTypeBuilder<Registration> entityBuilder)
        {
            entityBuilder.ToTable("REGISTRATION");

            entityBuilder.HasKey(x => x.Id);

            entityBuilder.Property(x => x.StudentId).IsRequired().HasMaxLength(450).HasColumnName("Student_Id");
            entityBuilder.Property(x => x.RegistrationPeriodId).HasColumnName("Registration_Period_Id");
            entityBuilder.Property(x => x.RegistrationFeeId).HasColumnName("Registration_Fee_Id");
            entityBuilder.Property(x => x.RegisteredOn).HasColumnName("Registered_On");

            entityBuilder.HasMany(x => x.Details)
                .WithOne(x => x.Registration)
                .OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Restrict);

        }

    }
}
