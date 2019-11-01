using LmsGateway.Paystack.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LmsGateway.Paystack.Data.Mappings
{
    public class PaystackTransactionStatusMap
    {
        public PaystackTransactionStatusMap(EntityTypeBuilder<PaystackTransactionStatus> entityBuilder)
        {
            entityBuilder.ToTable("PAYSTACK_TRANSACTION_STATUS");

            entityBuilder.HasKey(x => x.Id);

            entityBuilder.HasIndex(x => x.Name).IsUnique();
            entityBuilder.Property(x => x.Name).HasMaxLength(50);
            entityBuilder.Property(x => x.Description).HasMaxLength(500);


        }

    }


}
