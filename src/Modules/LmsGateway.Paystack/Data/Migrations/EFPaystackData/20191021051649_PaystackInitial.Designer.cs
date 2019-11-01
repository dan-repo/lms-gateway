using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using LmsGateway.Paystack.Data;

namespace LmsGateway.Paystack.Data.Migrations.EFPaystackData
{
    [DbContext(typeof(EFPaystackDataContext))]
    [Migration("20191021051649_PaystackInitial")]
    partial class PaystackInitial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LmsGateway.Paystack.Domain.PaystackSupportedCurrency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Alias")
                        .IsRequired()
                        .HasMaxLength(5);

                    b.Property<int>("Code");

                    b.Property<string>("Description")
                        .HasMaxLength(250);

                    b.Property<bool>("IsSupported")
                        .HasColumnName("Is_Supported");

                    b.Property<int>("LeastValueUnitMultiplier")
                        .HasColumnName("Least_Value_Unit_Multiplier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("PAYSTACK_SUPPORTED_CURRENCY");
                });

            modelBuilder.Entity("LmsGateway.Paystack.Domain.PaystackTransactionLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AccessCode")
                        .HasColumnName("Access_Code")
                        .HasMaxLength(250);

                    b.Property<decimal?>("Amount");

                    b.Property<string>("AuthorizationCode")
                        .HasColumnName("Authorization_Code")
                        .HasMaxLength(250);

                    b.Property<string>("AuthorizationUrl")
                        .HasColumnName("Authorization_Url");

                    b.Property<string>("Bank")
                        .HasMaxLength(100);

                    b.Property<string>("Bin")
                        .HasMaxLength(50);

                    b.Property<string>("Brand")
                        .HasMaxLength(50);

                    b.Property<string>("CardType")
                        .HasColumnName("Card_Type")
                        .HasMaxLength(50);

                    b.Property<string>("Channel")
                        .HasMaxLength(10);

                    b.Property<string>("CountryCode")
                        .HasColumnName("Country_Code")
                        .HasMaxLength(10);

                    b.Property<string>("Currency")
                        .HasMaxLength(10);

                    b.Property<string>("Domain")
                        .HasMaxLength(10);

                    b.Property<string>("ExpiryMonth")
                        .HasColumnName("Expiry_Month")
                        .HasMaxLength(10);

                    b.Property<string>("ExpiryYear")
                        .HasColumnName("Expiry_Year")
                        .HasMaxLength(10);

                    b.Property<int?>("Fees");

                    b.Property<string>("GatewayResponse")
                        .HasColumnName("Gateway_Response")
                        .HasMaxLength(250);

                    b.Property<string>("IPAddress")
                        .HasColumnName("IP_Address")
                        .HasMaxLength(50);

                    b.Property<string>("Last4")
                        .HasMaxLength(10);

                    b.Property<string>("Message")
                        .HasMaxLength(250);

                    b.Property<string>("Reference")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("RegistrationId")
                        .HasColumnName("Registration_Id");

                    b.Property<bool?>("Reusable");

                    b.Property<string>("Signature")
                        .HasMaxLength(150);

                    b.Property<string>("Status")
                        .HasMaxLength(50);

                    b.Property<DateTime?>("TransactionDate")
                        .HasColumnName("Transaction_Date");

                    b.HasKey("Id");

                    b.HasIndex("Reference");

                    b.ToTable("PAYSTACK_TRANSACTION_LOG");
                });

            modelBuilder.Entity("LmsGateway.Paystack.Domain.PaystackTransactionStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasMaxLength(500);

                    b.Property<string>("Name")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("PAYSTACK_TRANSACTION_STATUS");
                });
        }
    }
}
