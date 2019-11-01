using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LmsGateway.Data.Migrations.EFData
{
    public partial class AddedNameFieldToSession : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "SESSION",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Created_By",
                table: "REGISTRATION_PERIOD",
                maxLength: 450,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 450,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "SESSION");

            migrationBuilder.AlterColumn<string>(
                name: "Created_By",
                table: "REGISTRATION_PERIOD",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 450);
        }
    }
}
