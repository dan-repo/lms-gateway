using LmsGateway.Paystack.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LmsGateway.Paystack.Data.Mappings
{
    public class PaystackTransactionLogMap
    {
        public PaystackTransactionLogMap(EntityTypeBuilder<PaystackTransactionLog> entityBuilder)
        {
            entityBuilder.ToTable("PAYSTACK_TRANSACTION_LOG");

            entityBuilder.HasKey(x => x.Id);

            entityBuilder.Property(x => x.RegistrationId).HasColumnName("Registration_Id");
            entityBuilder.Property(x => x.Amount);
            entityBuilder.Property(x => x.Currency).HasMaxLength(10);
            entityBuilder.Property(x => x.TransactionDate).HasColumnName("Transaction_Date");
            entityBuilder.Property(x => x.Status).HasMaxLength(50);
            entityBuilder.HasIndex(x => x.Reference);
            entityBuilder.Property(x => x.Reference).IsRequired().HasMaxLength(50);
            entityBuilder.Property(x => x.Domain).HasMaxLength(10);
            entityBuilder.Property(x => x.GatewayResponse).HasColumnName("Gateway_Response").HasMaxLength(250);
            entityBuilder.Property(x => x.Message).HasMaxLength(250);
            entityBuilder.Property(x => x.IPAddress).HasColumnName("IP_Address").HasMaxLength(50);
            entityBuilder.Property(x => x.Fees);
            entityBuilder.Property(x => x.AuthorizationCode).HasColumnName("Authorization_Code").HasMaxLength(250);
            entityBuilder.Property(x => x.CardType).HasColumnName("Card_Type").HasMaxLength(50);
            entityBuilder.Property(x => x.Last4).HasMaxLength(10);
            entityBuilder.Property(x => x.ExpiryMonth).HasColumnName("Expiry_Month").HasMaxLength(10);
            entityBuilder.Property(x => x.ExpiryYear).HasColumnName("Expiry_Year").HasMaxLength(10);
            entityBuilder.Property(x => x.Bin).HasMaxLength(50);
            entityBuilder.Property(x => x.Bank).HasMaxLength(100);
            entityBuilder.Property(x => x.Channel).HasMaxLength(10);
            entityBuilder.Property(x => x.Signature).HasMaxLength(150);
            entityBuilder.Property(x => x.Brand).HasMaxLength(50);
            entityBuilder.Property(x => x.Reusable);
            entityBuilder.Property(x => x.CountryCode).HasColumnName("Country_Code").HasMaxLength(10);
            entityBuilder.Property(x => x.AuthorizationUrl).HasColumnName("Authorization_Url");
            entityBuilder.Property(x => x.AccessCode).HasColumnName("Access_Code").HasMaxLength(250);
        }
    }
}
