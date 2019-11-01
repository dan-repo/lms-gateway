using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LmsGateway.Paystack.Data.Migrations.EFPaystackData
{
    public partial class PaystackInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PAYSTACK_SUPPORTED_CURRENCY",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Alias = table.Column<string>(maxLength: 5, nullable: false),
                    Code = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 250, nullable: true),
                    Is_Supported = table.Column<bool>(nullable: false),
                    Least_Value_Unit_Multiplier = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PAYSTACK_SUPPORTED_CURRENCY", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PAYSTACK_TRANSACTION_LOG",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Access_Code = table.Column<string>(maxLength: 250, nullable: true),
                    Amount = table.Column<decimal>(nullable: true),
                    Authorization_Code = table.Column<string>(maxLength: 250, nullable: true),
                    Authorization_Url = table.Column<string>(nullable: true),
                    Bank = table.Column<string>(maxLength: 100, nullable: true),
                    Bin = table.Column<string>(maxLength: 50, nullable: true),
                    Brand = table.Column<string>(maxLength: 50, nullable: true),
                    Card_Type = table.Column<string>(maxLength: 50, nullable: true),
                    Channel = table.Column<string>(maxLength: 10, nullable: true),
                    Country_Code = table.Column<string>(maxLength: 10, nullable: true),
                    Currency = table.Column<string>(maxLength: 10, nullable: true),
                    Domain = table.Column<string>(maxLength: 10, nullable: true),
                    Expiry_Month = table.Column<string>(maxLength: 10, nullable: true),
                    Expiry_Year = table.Column<string>(maxLength: 10, nullable: true),
                    Fees = table.Column<int>(nullable: true),
                    Gateway_Response = table.Column<string>(maxLength: 250, nullable: true),
                    IP_Address = table.Column<string>(maxLength: 50, nullable: true),
                    Last4 = table.Column<string>(maxLength: 10, nullable: true),
                    Message = table.Column<string>(maxLength: 250, nullable: true),
                    Reference = table.Column<string>(maxLength: 50, nullable: false),
                    Registration_Id = table.Column<int>(nullable: false),
                    Reusable = table.Column<bool>(nullable: true),
                    Signature = table.Column<string>(maxLength: 150, nullable: true),
                    Status = table.Column<string>(maxLength: 50, nullable: true),
                    Transaction_Date = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PAYSTACK_TRANSACTION_LOG", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PAYSTACK_TRANSACTION_STATUS",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PAYSTACK_TRANSACTION_STATUS", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PAYSTACK_SUPPORTED_CURRENCY_Code",
                table: "PAYSTACK_SUPPORTED_CURRENCY",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PAYSTACK_TRANSACTION_LOG_Reference",
                table: "PAYSTACK_TRANSACTION_LOG",
                column: "Reference");

            migrationBuilder.CreateIndex(
                name: "IX_PAYSTACK_TRANSACTION_STATUS_Name",
                table: "PAYSTACK_TRANSACTION_STATUS",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PAYSTACK_SUPPORTED_CURRENCY");

            migrationBuilder.DropTable(
                name: "PAYSTACK_TRANSACTION_LOG");

            migrationBuilder.DropTable(
                name: "PAYSTACK_TRANSACTION_STATUS");
        }
    }
}
