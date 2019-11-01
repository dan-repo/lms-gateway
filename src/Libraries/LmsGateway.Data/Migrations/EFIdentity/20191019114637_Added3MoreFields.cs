using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LmsGateway.Data.Migrations.EFIdentity
{
    public partial class Added3MoreFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created_On",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date_Verified",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Reg_No",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created_On",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Date_Verified",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Reg_No",
                table: "AspNetUsers");
        }
    }
}
