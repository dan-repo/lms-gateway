using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LmsGateway.Data.Migrations.EFData
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SETTING",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Concurrency_Stamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SETTING", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MEDIA",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    File = table.Column<byte[]>(nullable: true),
                    Height = table.Column<int>(nullable: true),
                    Mime_Type = table.Column<string>(maxLength: 20, nullable: false),
                    Url = table.Column<string>(maxLength: 500, nullable: true),
                    Width = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MEDIA", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FACULTY",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    Name = table.Column<int>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FACULTY", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LEVEL",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    Name = table.Column<int>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LEVEL", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PROGRAMME",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Alias = table.Column<string>(maxLength: 20, nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PROGRAMME", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SEMESTER",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SEMESTER", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SESSION",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created_By = table.Column<string>(maxLength: 450, nullable: true),
                    Created_On = table.Column<DateTime>(nullable: false),
                    End_Date = table.Column<DateTime>(nullable: true),
                    Start_Date = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SESSION", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DEPARTMENT",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    Faculty_Id = table.Column<int>(nullable: false),
                    Name = table.Column<int>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DEPARTMENT", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DEPARTMENT_FACULTY_Faculty_Id",
                        column: x => x.Faculty_Id,
                        principalTable: "FACULTY",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ADMISSION_LIST",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date_Posted = table.Column<DateTime>(nullable: false),
                    Media_Id = table.Column<int>(nullable: true),
                    Posted_By = table.Column<string>(nullable: true),
                    Session_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADMISSION_LIST", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ADMISSION_LIST_MEDIA_Media_Id",
                        column: x => x.Media_Id,
                        principalTable: "MEDIA",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ADMISSION_LIST_SESSION_Session_Id",
                        column: x => x.Session_Id,
                        principalTable: "SESSION",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "REGISTRATION_PERIOD",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created_By = table.Column<string>(maxLength: 450, nullable: true),
                    Created_On = table.Column<DateTime>(nullable: false),
                    End_Date = table.Column<DateTime>(nullable: true),
                    Semester_Id = table.Column<int>(nullable: false),
                    Session_Id = table.Column<int>(nullable: false),
                    Start_Date = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REGISTRATION_PERIOD", x => x.Id);
                    table.ForeignKey(
                        name: "FK_REGISTRATION_PERIOD_SEMESTER_Semester_Id",
                        column: x => x.Semester_Id,
                        principalTable: "SEMESTER",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_REGISTRATION_PERIOD_SESSION_Session_Id",
                        column: x => x.Session_Id,
                        principalTable: "SESSION",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "COURSE",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Course_Code = table.Column<string>(maxLength: 15, nullable: false),
                    Created_By = table.Column<string>(maxLength: 450, nullable: true),
                    Created_On = table.Column<DateTime>(nullable: false),
                    Department_Id = table.Column<int>(nullable: false),
                    Is_Optional = table.Column<bool>(nullable: false),
                    Level_Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 60, nullable: false),
                    Programme_Id = table.Column<int>(nullable: false),
                    Semester_Id = table.Column<int>(nullable: false),
                    Unit = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COURSE", x => x.Id);
                    table.ForeignKey(
                        name: "FK_COURSE_DEPARTMENT_Department_Id",
                        column: x => x.Department_Id,
                        principalTable: "DEPARTMENT",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_COURSE_LEVEL_Level_Id",
                        column: x => x.Level_Id,
                        principalTable: "LEVEL",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_COURSE_PROGRAMME_Programme_Id",
                        column: x => x.Programme_Id,
                        principalTable: "PROGRAMME",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_COURSE_SEMESTER_Semester_Id",
                        column: x => x.Semester_Id,
                        principalTable: "SEMESTER",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ADMISSION_LIST_DETAIL",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Admission_List_Id = table.Column<int>(nullable: false),
                    Department_Id = table.Column<int>(nullable: false),
                    First_Name = table.Column<string>(maxLength: 20, nullable: false),
                    Level_Id = table.Column<int>(nullable: false),
                    Other_Name = table.Column<string>(maxLength: 20, nullable: true),
                    Programme_Id = table.Column<int>(nullable: false),
                    Reg_No = table.Column<string>(maxLength: 20, nullable: false),
                    Session_Id = table.Column<int>(nullable: false),
                    Surname = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADMISSION_LIST_DETAIL", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ADMISSION_LIST_DETAIL_ADMISSION_LIST_Admission_List_Id",
                        column: x => x.Admission_List_Id,
                        principalTable: "ADMISSION_LIST",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ADMISSION_LIST_DETAIL_DEPARTMENT_Department_Id",
                        column: x => x.Department_Id,
                        principalTable: "DEPARTMENT",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ADMISSION_LIST_DETAIL_LEVEL_Level_Id",
                        column: x => x.Level_Id,
                        principalTable: "LEVEL",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ADMISSION_LIST_DETAIL_PROGRAMME_Programme_Id",
                        column: x => x.Programme_Id,
                        principalTable: "PROGRAMME",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "REGISTRATION_FEE",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Access_Charge = table.Column<decimal>(nullable: false),
                    Amount_Payable = table.Column<decimal>(nullable: false),
                    Can_Make_Part_Payment = table.Column<bool>(nullable: true),
                    Created_By = table.Column<string>(maxLength: 450, nullable: true),
                    Created_On = table.Column<DateTime>(nullable: false),
                    Department_Id = table.Column<int>(nullable: false),
                    Level_Id = table.Column<int>(nullable: false),
                    Programme_Id = table.Column<int>(nullable: false),
                    Registration_Period_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REGISTRATION_FEE", x => x.Id);
                    table.ForeignKey(
                        name: "FK_REGISTRATION_FEE_DEPARTMENT_Department_Id",
                        column: x => x.Department_Id,
                        principalTable: "DEPARTMENT",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_REGISTRATION_FEE_LEVEL_Level_Id",
                        column: x => x.Level_Id,
                        principalTable: "LEVEL",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_REGISTRATION_FEE_PROGRAMME_Programme_Id",
                        column: x => x.Programme_Id,
                        principalTable: "PROGRAMME",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_REGISTRATION_FEE_REGISTRATION_PERIOD_Registration_Period_Id",
                        column: x => x.Registration_Period_Id,
                        principalTable: "REGISTRATION_PERIOD",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "STUDENT",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Admission_List_Detail_Id = table.Column<int>(nullable: true),
                    Created_On = table.Column<DateTime>(nullable: false),
                    Current_Level_Id = table.Column<int>(nullable: false),
                    Department_Id = table.Column<int>(nullable: false),
                    Programme_Id = table.Column<int>(nullable: false),
                    Reg_No = table.Column<string>(maxLength: 50, nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STUDENT", x => x.Id);
                    table.ForeignKey(
                        name: "FK_STUDENT_ADMISSION_LIST_DETAIL_Admission_List_Detail_Id",
                        column: x => x.Admission_List_Detail_Id,
                        principalTable: "ADMISSION_LIST_DETAIL",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_STUDENT_LEVEL_Current_Level_Id",
                        column: x => x.Current_Level_Id,
                        principalTable: "LEVEL",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_STUDENT_DEPARTMENT_Department_Id",
                        column: x => x.Department_Id,
                        principalTable: "DEPARTMENT",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_STUDENT_PROGRAMME_Programme_Id",
                        column: x => x.Programme_Id,
                        principalTable: "PROGRAMME",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "REGISTRATION",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Registered_On = table.Column<DateTime>(nullable: false),
                    Registration_Fee_Id = table.Column<int>(nullable: false),
                    Registration_Period_Id = table.Column<int>(nullable: false),
                    Student_Id = table.Column<string>(maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REGISTRATION", x => x.Id);
                    table.ForeignKey(
                        name: "FK_REGISTRATION_REGISTRATION_FEE_Registration_Fee_Id",
                        column: x => x.Registration_Fee_Id,
                        principalTable: "REGISTRATION_FEE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_REGISTRATION_REGISTRATION_PERIOD_Registration_Period_Id",
                        column: x => x.Registration_Period_Id,
                        principalTable: "REGISTRATION_PERIOD",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "STUDENT_LEVEL",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created_By = table.Column<string>(maxLength: 450, nullable: true),
                    Created_On = table.Column<DateTime>(nullable: false),
                    Level_Id = table.Column<int>(nullable: false),
                    Student_Id = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STUDENT_LEVEL", x => x.Id);
                    table.ForeignKey(
                        name: "FK_STUDENT_LEVEL_LEVEL_Level_Id",
                        column: x => x.Level_Id,
                        principalTable: "LEVEL",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_STUDENT_LEVEL_STUDENT_Student_Id",
                        column: x => x.Student_Id,
                        principalTable: "STUDENT",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "REGISTRATION_DETAIL",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount_Paid = table.Column<decimal>(nullable: false),
                    Payment_Method = table.Column<string>(nullable: true),
                    Payment_Status = table.Column<int>(nullable: false),
                    Registered_On = table.Column<DateTime>(nullable: false),
                    Registration_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REGISTRATION_DETAIL", x => x.Id);
                    table.ForeignKey(
                        name: "FK_REGISTRATION_DETAIL_REGISTRATION_Registration_Id",
                        column: x => x.Registration_Id,
                        principalTable: "REGISTRATION",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ADMISSION_LIST_Media_Id",
                table: "ADMISSION_LIST",
                column: "Media_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ADMISSION_LIST_Session_Id",
                table: "ADMISSION_LIST",
                column: "Session_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ADMISSION_LIST_DETAIL_Admission_List_Id",
                table: "ADMISSION_LIST_DETAIL",
                column: "Admission_List_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ADMISSION_LIST_DETAIL_Department_Id",
                table: "ADMISSION_LIST_DETAIL",
                column: "Department_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ADMISSION_LIST_DETAIL_Level_Id",
                table: "ADMISSION_LIST_DETAIL",
                column: "Level_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ADMISSION_LIST_DETAIL_Programme_Id",
                table: "ADMISSION_LIST_DETAIL",
                column: "Programme_Id");

            migrationBuilder.CreateIndex(
                name: "IX_COURSE_Department_Id",
                table: "COURSE",
                column: "Department_Id");

            migrationBuilder.CreateIndex(
                name: "IX_COURSE_Level_Id",
                table: "COURSE",
                column: "Level_Id");

            migrationBuilder.CreateIndex(
                name: "IX_COURSE_Programme_Id",
                table: "COURSE",
                column: "Programme_Id");

            migrationBuilder.CreateIndex(
                name: "IX_COURSE_Semester_Id",
                table: "COURSE",
                column: "Semester_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DEPARTMENT_Faculty_Id",
                table: "DEPARTMENT",
                column: "Faculty_Id");

            migrationBuilder.CreateIndex(
                name: "IX_REGISTRATION_Registration_Fee_Id",
                table: "REGISTRATION",
                column: "Registration_Fee_Id");

            migrationBuilder.CreateIndex(
                name: "IX_REGISTRATION_Registration_Period_Id",
                table: "REGISTRATION",
                column: "Registration_Period_Id");

            migrationBuilder.CreateIndex(
                name: "IX_REGISTRATION_DETAIL_Registration_Id",
                table: "REGISTRATION_DETAIL",
                column: "Registration_Id");

            migrationBuilder.CreateIndex(
                name: "IX_REGISTRATION_FEE_Department_Id",
                table: "REGISTRATION_FEE",
                column: "Department_Id");

            migrationBuilder.CreateIndex(
                name: "IX_REGISTRATION_FEE_Level_Id",
                table: "REGISTRATION_FEE",
                column: "Level_Id");

            migrationBuilder.CreateIndex(
                name: "IX_REGISTRATION_FEE_Programme_Id",
                table: "REGISTRATION_FEE",
                column: "Programme_Id");

            migrationBuilder.CreateIndex(
                name: "IX_REGISTRATION_FEE_Registration_Period_Id",
                table: "REGISTRATION_FEE",
                column: "Registration_Period_Id");

            migrationBuilder.CreateIndex(
                name: "IX_REGISTRATION_PERIOD_Semester_Id",
                table: "REGISTRATION_PERIOD",
                column: "Semester_Id");

            migrationBuilder.CreateIndex(
                name: "IX_REGISTRATION_PERIOD_Session_Id",
                table: "REGISTRATION_PERIOD",
                column: "Session_Id");

            migrationBuilder.CreateIndex(
                name: "IX_STUDENT_Admission_List_Detail_Id",
                table: "STUDENT",
                column: "Admission_List_Detail_Id");

            migrationBuilder.CreateIndex(
                name: "IX_STUDENT_Current_Level_Id",
                table: "STUDENT",
                column: "Current_Level_Id");

            migrationBuilder.CreateIndex(
                name: "IX_STUDENT_Department_Id",
                table: "STUDENT",
                column: "Department_Id");

            migrationBuilder.CreateIndex(
                name: "IX_STUDENT_Programme_Id",
                table: "STUDENT",
                column: "Programme_Id");

            migrationBuilder.CreateIndex(
                name: "IX_STUDENT_LEVEL_Level_Id",
                table: "STUDENT_LEVEL",
                column: "Level_Id");

            migrationBuilder.CreateIndex(
                name: "IX_STUDENT_LEVEL_Student_Id",
                table: "STUDENT_LEVEL",
                column: "Student_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SETTING");

            migrationBuilder.DropTable(
                name: "COURSE");

            migrationBuilder.DropTable(
                name: "REGISTRATION_DETAIL");

            migrationBuilder.DropTable(
                name: "STUDENT_LEVEL");

            migrationBuilder.DropTable(
                name: "REGISTRATION");

            migrationBuilder.DropTable(
                name: "STUDENT");

            migrationBuilder.DropTable(
                name: "REGISTRATION_FEE");

            migrationBuilder.DropTable(
                name: "ADMISSION_LIST_DETAIL");

            migrationBuilder.DropTable(
                name: "REGISTRATION_PERIOD");

            migrationBuilder.DropTable(
                name: "ADMISSION_LIST");

            migrationBuilder.DropTable(
                name: "DEPARTMENT");

            migrationBuilder.DropTable(
                name: "LEVEL");

            migrationBuilder.DropTable(
                name: "PROGRAMME");

            migrationBuilder.DropTable(
                name: "SEMESTER");

            migrationBuilder.DropTable(
                name: "MEDIA");

            migrationBuilder.DropTable(
                name: "SESSION");

            migrationBuilder.DropTable(
                name: "FACULTY");
        }
    }
}
