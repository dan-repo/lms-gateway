using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LmsGateway.Data.Migrations.EFData
{
    public partial class GeneralEntitiesUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_REGISTRATION_DETAIL_REGISTRATION_Registration_Id",
                table: "REGISTRATION_DETAIL");

            migrationBuilder.RenameColumn(
                name: "Concurrency_Stamp",
                table: "SETTING",
                newName: "Value");

            migrationBuilder.AlterColumn<string>(
                name: "Payment_Method",
                table: "REGISTRATION_DETAIL",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "LEVEL",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(int),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "FACULTY",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(int),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "DEPARTMENT",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(int),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Posted_By",
                table: "ADMISSION_LIST",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "SETTING",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 30);

            migrationBuilder.AddForeignKey(
                name: "FK_REGISTRATION_DETAIL_REGISTRATION_Registration_Id",
                table: "REGISTRATION_DETAIL",
                column: "Registration_Id",
                principalTable: "REGISTRATION",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_REGISTRATION_DETAIL_REGISTRATION_Registration_Id",
                table: "REGISTRATION_DETAIL");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "SETTING",
                newName: "Concurrency_Stamp");

            migrationBuilder.AlterColumn<string>(
                name: "Payment_Method",
                table: "REGISTRATION_DETAIL",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "Name",
                table: "LEVEL",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<int>(
                name: "Name",
                table: "FACULTY",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<int>(
                name: "Name",
                table: "DEPARTMENT",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Posted_By",
                table: "ADMISSION_LIST",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 450,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "SETTING",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AddForeignKey(
                name: "FK_REGISTRATION_DETAIL_REGISTRATION_Registration_Id",
                table: "REGISTRATION_DETAIL",
                column: "Registration_Id",
                principalTable: "REGISTRATION",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
