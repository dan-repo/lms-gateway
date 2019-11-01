using LmsGateway.Domain.Registrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LmsGateway.Data.Mappings.Registrations
{
    public class RegistrationDetailMap
    {
        public RegistrationDetailMap(EntityTypeBuilder<RegistrationDetail> entityBuilder)
        {
            entityBuilder.ToTable("REGISTRATION_DETAIL");

            entityBuilder.HasKey(x => x.Id);

            entityBuilder.Property(x => x.RegistrationId).HasColumnName("Registration_Id");
            entityBuilder.Property(x => x.PaymentStatus).HasColumnName("Payment_Status");
            entityBuilder.Property(x => x.PaymentMethod).HasColumnName("Payment_Method").HasMaxLength(50).IsRequired();
            entityBuilder.Property(x => x.AmountPaid).HasColumnName("Amount_Paid");
            entityBuilder.Property(x => x.RegisteredOn).HasColumnName("Registered_On");

        }
    }


}
