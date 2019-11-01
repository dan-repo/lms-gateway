using LmsGateway.Paystack.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LmsGateway.Paystack.Data.Mappings
{
    public class PaystackSupportedCurrencyMap
    {
        public PaystackSupportedCurrencyMap(EntityTypeBuilder<PaystackSupportedCurrency> entityBuilder)
        {
            entityBuilder.ToTable("PAYSTACK_SUPPORTED_CURRENCY");

            entityBuilder.HasKey(x => x.Id);

            entityBuilder.HasIndex(x => x.Code).IsUnique();
            entityBuilder.Property(x => x.Code).IsRequired();
            entityBuilder.Property(x => x.Alias).HasMaxLength(5).IsRequired();
            entityBuilder.Property(x => x.Name).HasMaxLength(50).IsRequired();
            entityBuilder.Property(x => x.Description).HasMaxLength(250);
            entityBuilder.Property(x => x.LeastValueUnitMultiplier).HasColumnName("Least_Value_Unit_Multiplier");
            entityBuilder.Property(x => x.IsSupported).HasColumnName("Is_Supported");



        }


    }
}
