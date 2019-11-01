using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using LmsGateway.Data;
using LmsGateway.Domain.Registrations;

namespace LmsGateway.Data.Migrations.EFData
{
    [DbContext(typeof(EFDataContext))]
    [Migration("20191023171027_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LmsGateway.Domain.Configuration.Setting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("Value")
                        .HasColumnName("Concurrency_Stamp");

                    b.HasKey("Id");

                    b.ToTable("SETTING");
                });

            modelBuilder.Entity("LmsGateway.Domain.Medias.Media", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("File");

                    b.Property<int?>("Height");

                    b.Property<string>("MimeType")
                        .IsRequired()
                        .HasColumnName("Mime_Type")
                        .HasMaxLength(20);

                    b.Property<string>("Url")
                        .HasMaxLength(500);

                    b.Property<int?>("Width");

                    b.HasKey("Id");

                    b.ToTable("MEDIA");
                });

            modelBuilder.Entity("LmsGateway.Domain.Registrations.AdmissionList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DatePosted")
                        .HasColumnName("Date_Posted");

                    b.Property<int?>("MediaId")
                        .HasColumnName("Media_Id");

                    b.Property<string>("PostedBy")
                        .HasColumnName("Posted_By");

                    b.Property<int>("SessionId")
                        .HasColumnName("Session_Id");

                    b.HasKey("Id");

                    b.HasIndex("MediaId");

                    b.HasIndex("SessionId");

                    b.ToTable("ADMISSION_LIST");
                });

            modelBuilder.Entity("LmsGateway.Domain.Registrations.AdmissionListDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AdmissionListId")
                        .HasColumnName("Admission_List_Id");

                    b.Property<int>("DepartmentId")
                        .HasColumnName("Department_Id");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnName("First_Name")
                        .HasMaxLength(20);

                    b.Property<int>("LevelId")
                        .HasColumnName("Level_Id");

                    b.Property<string>("OtherName")
                        .HasColumnName("Other_Name")
                        .HasMaxLength(20);

                    b.Property<int>("ProgrammeId")
                        .HasColumnName("Programme_Id");

                    b.Property<string>("RegNo")
                        .IsRequired()
                        .HasColumnName("Reg_No")
                        .HasMaxLength(20);

                    b.Property<int>("SessionId")
                        .HasColumnName("Session_Id");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnName("Surname")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.HasIndex("AdmissionListId");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("LevelId");

                    b.HasIndex("ProgrammeId");

                    b.ToTable("ADMISSION_LIST_DETAIL");
                });

            modelBuilder.Entity("LmsGateway.Domain.Registrations.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnName("Course_Code")
                        .HasMaxLength(15);

                    b.Property<string>("CreatedBy")
                        .HasColumnName("Created_By")
                        .HasMaxLength(450);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnName("Created_On");

                    b.Property<int>("DepartmentId")
                        .HasColumnName("Department_Id");

                    b.Property<bool>("IsOptional")
                        .HasColumnName("Is_Optional");

                    b.Property<int>("LevelId")
                        .HasColumnName("Level_Id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60);

                    b.Property<int>("ProgrammeId")
                        .HasColumnName("Programme_Id");

                    b.Property<int>("SemesterId")
                        .HasColumnName("Semester_Id");

                    b.Property<int>("Unit");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("LevelId");

                    b.HasIndex("ProgrammeId");

                    b.HasIndex("SemesterId");

                    b.ToTable("COURSE");
                });

            modelBuilder.Entity("LmsGateway.Domain.Registrations.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasMaxLength(500);

                    b.Property<int>("FacultyId")
                        .HasColumnName("Faculty_Id");

                    b.Property<int>("Name")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("FacultyId");

                    b.ToTable("DEPARTMENT");
                });

            modelBuilder.Entity("LmsGateway.Domain.Registrations.Faculty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasMaxLength(500);

                    b.Property<int>("Name")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("FACULTY");
                });

            modelBuilder.Entity("LmsGateway.Domain.Registrations.Level", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasMaxLength(500);

                    b.Property<int>("Name")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("LEVEL");
                });

            modelBuilder.Entity("LmsGateway.Domain.Registrations.Programme", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Alias")
                        .HasMaxLength(20);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("PROGRAMME");
                });

            modelBuilder.Entity("LmsGateway.Domain.Registrations.Registration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("RegisteredOn")
                        .HasColumnName("Registered_On");

                    b.Property<int>("RegistrationFeeId")
                        .HasColumnName("Registration_Fee_Id");

                    b.Property<int>("RegistrationPeriodId")
                        .HasColumnName("Registration_Period_Id");

                    b.Property<string>("StudentId")
                        .IsRequired()
                        .HasColumnName("Student_Id")
                        .HasMaxLength(450);

                    b.HasKey("Id");

                    b.HasIndex("RegistrationFeeId");

                    b.HasIndex("RegistrationPeriodId");

                    b.ToTable("REGISTRATION");
                });

            modelBuilder.Entity("LmsGateway.Domain.Registrations.RegistrationDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("AmountPaid")
                        .HasColumnName("Amount_Paid");

                    b.Property<string>("PaymentMethod")
                        .HasColumnName("Payment_Method");

                    b.Property<int>("PaymentStatus")
                        .HasColumnName("Payment_Status");

                    b.Property<DateTime>("RegisteredOn")
                        .HasColumnName("Registered_On");

                    b.Property<int>("RegistrationId")
                        .HasColumnName("Registration_Id");

                    b.HasKey("Id");

                    b.HasIndex("RegistrationId");

                    b.ToTable("REGISTRATION_DETAIL");
                });

            modelBuilder.Entity("LmsGateway.Domain.Registrations.RegistrationFee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("AccessCharge")
                        .HasColumnName("Access_Charge");

                    b.Property<decimal>("AmountPayable")
                        .HasColumnName("Amount_Payable");

                    b.Property<bool?>("CanMakePartPayment")
                        .HasColumnName("Can_Make_Part_Payment");

                    b.Property<string>("CreatedBy")
                        .HasColumnName("Created_By")
                        .HasMaxLength(450);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnName("Created_On");

                    b.Property<int>("DepartmentId")
                        .HasColumnName("Department_Id");

                    b.Property<int>("LevelId")
                        .HasColumnName("Level_Id");

                    b.Property<int>("ProgrammeId")
                        .HasColumnName("Programme_Id");

                    b.Property<int>("RegistrationPeriodId")
                        .HasColumnName("Registration_Period_Id");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("LevelId");

                    b.HasIndex("ProgrammeId");

                    b.HasIndex("RegistrationPeriodId");

                    b.ToTable("REGISTRATION_FEE");
                });

            modelBuilder.Entity("LmsGateway.Domain.Registrations.RegistrationPeriod", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy")
                        .HasColumnName("Created_By")
                        .HasMaxLength(450);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnName("Created_On");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnName("End_Date");

                    b.Property<int>("SemesterId")
                        .HasColumnName("Semester_Id");

                    b.Property<int>("SessionId")
                        .HasColumnName("Session_Id");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnName("Start_Date");

                    b.HasKey("Id");

                    b.HasIndex("SemesterId");

                    b.HasIndex("SessionId");

                    b.ToTable("REGISTRATION_PERIOD");
                });

            modelBuilder.Entity("LmsGateway.Domain.Registrations.Semester", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("SEMESTER");
                });

            modelBuilder.Entity("LmsGateway.Domain.Registrations.Session", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy")
                        .HasColumnName("Created_By")
                        .HasMaxLength(450);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnName("Created_On");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnName("End_Date");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnName("Start_Date");

                    b.HasKey("Id");

                    b.ToTable("SESSION");
                });

            modelBuilder.Entity("LmsGateway.Domain.Registrations.Student", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AdmissionListDetailId")
                        .HasColumnName("Admission_List_Detail_Id");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnName("Created_On");

                    b.Property<int>("CurrentLevelId")
                        .HasColumnName("Current_Level_Id");

                    b.Property<int>("DepartmentId")
                        .HasColumnName("Department_Id");

                    b.Property<int>("ProgrammeId")
                        .HasColumnName("Programme_Id");

                    b.Property<string>("RegNo")
                        .HasColumnName("Reg_No")
                        .HasMaxLength(50);

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.HasIndex("AdmissionListDetailId");

                    b.HasIndex("CurrentLevelId");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("ProgrammeId");

                    b.ToTable("STUDENT");
                });

            modelBuilder.Entity("LmsGateway.Domain.Registrations.StudentLevel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy")
                        .HasColumnName("Created_By")
                        .HasMaxLength(450);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnName("Created_On");

                    b.Property<int>("LevelId")
                        .HasColumnName("Level_Id");

                    b.Property<string>("StudentId")
                        .IsRequired()
                        .HasColumnName("Student_Id");

                    b.HasKey("Id");

                    b.HasIndex("LevelId");

                    b.HasIndex("StudentId");

                    b.ToTable("STUDENT_LEVEL");
                });

            modelBuilder.Entity("LmsGateway.Domain.Registrations.AdmissionList", b =>
                {
                    b.HasOne("LmsGateway.Domain.Medias.Media", "Media")
                        .WithMany()
                        .HasForeignKey("MediaId");

                    b.HasOne("LmsGateway.Domain.Registrations.Session", "Session")
                        .WithMany()
                        .HasForeignKey("SessionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LmsGateway.Domain.Registrations.AdmissionListDetail", b =>
                {
                    b.HasOne("LmsGateway.Domain.Registrations.AdmissionList", "AdmissionList")
                        .WithMany()
                        .HasForeignKey("AdmissionListId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LmsGateway.Domain.Registrations.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LmsGateway.Domain.Registrations.Level", "Level")
                        .WithMany()
                        .HasForeignKey("LevelId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LmsGateway.Domain.Registrations.Programme", "Programme")
                        .WithMany()
                        .HasForeignKey("ProgrammeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LmsGateway.Domain.Registrations.Course", b =>
                {
                    b.HasOne("LmsGateway.Domain.Registrations.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LmsGateway.Domain.Registrations.Level", "Level")
                        .WithMany()
                        .HasForeignKey("LevelId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LmsGateway.Domain.Registrations.Programme", "Programme")
                        .WithMany()
                        .HasForeignKey("ProgrammeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LmsGateway.Domain.Registrations.Semester", "Semester")
                        .WithMany()
                        .HasForeignKey("SemesterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LmsGateway.Domain.Registrations.Department", b =>
                {
                    b.HasOne("LmsGateway.Domain.Registrations.Faculty", "Faculty")
                        .WithMany()
                        .HasForeignKey("FacultyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LmsGateway.Domain.Registrations.Registration", b =>
                {
                    b.HasOne("LmsGateway.Domain.Registrations.RegistrationFee", "RegistrationFee")
                        .WithMany()
                        .HasForeignKey("RegistrationFeeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LmsGateway.Domain.Registrations.RegistrationPeriod", "RegistrationPeriod")
                        .WithMany()
                        .HasForeignKey("RegistrationPeriodId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LmsGateway.Domain.Registrations.RegistrationDetail", b =>
                {
                    b.HasOne("LmsGateway.Domain.Registrations.Registration", "Registration")
                        .WithMany()
                        .HasForeignKey("RegistrationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LmsGateway.Domain.Registrations.RegistrationFee", b =>
                {
                    b.HasOne("LmsGateway.Domain.Registrations.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LmsGateway.Domain.Registrations.Level", "Level")
                        .WithMany()
                        .HasForeignKey("LevelId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LmsGateway.Domain.Registrations.Programme", "Programme")
                        .WithMany()
                        .HasForeignKey("ProgrammeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LmsGateway.Domain.Registrations.RegistrationPeriod", "RegistrationPeriod")
                        .WithMany("RegistrationFees")
                        .HasForeignKey("RegistrationPeriodId");
                });

            modelBuilder.Entity("LmsGateway.Domain.Registrations.RegistrationPeriod", b =>
                {
                    b.HasOne("LmsGateway.Domain.Registrations.Semester", "Semester")
                        .WithMany()
                        .HasForeignKey("SemesterId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LmsGateway.Domain.Registrations.Session", "Session")
                        .WithMany()
                        .HasForeignKey("SessionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LmsGateway.Domain.Registrations.Student", b =>
                {
                    b.HasOne("LmsGateway.Domain.Registrations.AdmissionListDetail", "AdmissionListDetail")
                        .WithMany()
                        .HasForeignKey("AdmissionListDetailId");

                    b.HasOne("LmsGateway.Domain.Registrations.Level", "CurrentLevel")
                        .WithMany()
                        .HasForeignKey("CurrentLevelId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LmsGateway.Domain.Registrations.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LmsGateway.Domain.Registrations.Programme", "Programme")
                        .WithMany()
                        .HasForeignKey("ProgrammeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LmsGateway.Domain.Registrations.StudentLevel", b =>
                {
                    b.HasOne("LmsGateway.Domain.Registrations.Level", "Level")
                        .WithMany()
                        .HasForeignKey("LevelId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LmsGateway.Domain.Registrations.Student", "Student")
                        .WithMany("LevelHistory")
                        .HasForeignKey("StudentId");
                });
        }
    }
}
