﻿// <auto-generated />
using System;
using Asklepios.Infrastructure.DAL.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Asklepios.Infrastructure.DAL.Migrations
{
    [DbContext(typeof(AsklepiosDbContext))]
    [Migration("20240903154817_PatientsHistoryTable")]
    partial class PatientsHistoryTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Asklepios.Core.Entities.Departments.Department", b =>
                {
                    b.Property<Guid>("DepartmentId")
                        .HasColumnType("uuid");

                    b.Property<int>("ActualNumberOfPatients")
                        .HasColumnType("integer");

                    b.Property<string>("DepartmentName")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<int>("NumberOfBeds")
                        .HasColumnType("integer");

                    b.HasKey("DepartmentId");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("Asklepios.Core.Entities.Departments.Room", b =>
                {
                    b.Property<Guid>("RoomId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("DepartmentId")
                        .HasColumnType("uuid");

                    b.Property<int>("NumberOfBeds")
                        .HasColumnType("integer");

                    b.Property<int>("RoomNumber")
                        .HasColumnType("integer");

                    b.Property<string>("RoomType")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.HasKey("RoomId");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("Asklepios.Core.Entities.Examinations.ExamResult", b =>
                {
                    b.Property<Guid>("ExamResultId")
                        .HasColumnType("uuid");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<int>("ExaminationId")
                        .HasColumnType("integer");

                    b.Property<Guid>("PatientId")
                        .HasColumnType("uuid");

                    b.Property<string>("Result")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.HasKey("ExamResultId");

                    b.HasIndex("ExaminationId");

                    b.HasIndex("PatientId");

                    b.ToTable("ExamResults");
                });

            modelBuilder.Entity("Asklepios.Core.Entities.Examinations.Examination", b =>
                {
                    b.Property<int>("ExaminationId")
                        .HasColumnType("integer");

                    b.Property<string>("ExamCategory")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<string>("ExamName")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.HasKey("ExaminationId");

                    b.ToTable("Examinations");
                });

            modelBuilder.Entity("Asklepios.Core.Entities.Examinations.Operation", b =>
                {
                    b.Property<Guid>("OperationId")
                        .HasColumnType("uuid");

                    b.Property<string>("AnesthesiaType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Comlications")
                        .IsRequired()
                        .HasMaxLength(10000)
                        .HasColumnType("character varying(10000)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("MedicalStaffId")
                        .HasColumnType("uuid");

                    b.Property<string>("OperationName")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)");

                    b.Property<Guid>("PatientId")
                        .HasColumnType("uuid");

                    b.Property<string>("Result")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("OperationId");

                    b.HasIndex("MedicalStaffId");

                    b.HasIndex("PatientId");

                    b.ToTable("Operations");
                });

            modelBuilder.Entity("Asklepios.Core.Entities.Patients.Discharge", b =>
                {
                    b.Property<Guid>("DischargeId")
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<string>("DischargeReasson")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)");

                    b.Property<Guid>("MedicalStaffId")
                        .HasColumnType("uuid");

                    b.Property<string>("PatientName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("PatientSurname")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("PeselNumber")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("character varying(11)");

                    b.Property<string>("Summary")
                        .IsRequired()
                        .HasMaxLength(5000)
                        .HasColumnType("character varying(5000)");

                    b.HasKey("DischargeId");

                    b.HasIndex("MedicalStaffId");

                    b.HasIndex("PeselNumber")
                        .IsUnique();

                    b.ToTable("Discharges");
                });

            modelBuilder.Entity("Asklepios.Core.Entities.Patients.Patient", b =>
                {
                    b.Property<Guid>("PatientId")
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<DateOnly>("AdmissionDate")
                        .HasColumnType("date");

                    b.Property<Guid>("DepartmentId")
                        .HasColumnType("uuid");

                    b.Property<string>("InitialDiagnosis")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDischarged")
                        .HasColumnType("boolean");

                    b.Property<Guid>("MedicalStaffId")
                        .HasColumnType("uuid");

                    b.Property<string>("PatientName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("PatientSurname")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("PeselNumber")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("character varying(11)");

                    b.Property<Guid>("RoomId")
                        .HasColumnType("uuid");

                    b.Property<string>("Treatment")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("PatientId");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("MedicalStaffId");

                    b.HasIndex("PeselNumber")
                        .IsUnique();

                    b.HasIndex("RoomId");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("Asklepios.Core.Entities.Patients.PatientHistory", b =>
                {
                    b.Property<Guid>("HistoryId")
                        .HasColumnType("uuid");

                    b.Property<string>("History")
                        .IsRequired()
                        .HasColumnType("jsonb");

                    b.Property<string>("PatientName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("PatientSurname")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("PeselNumber")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("character varying(11)");

                    b.HasKey("HistoryId");

                    b.HasIndex("PeselNumber")
                        .IsUnique();

                    b.ToTable("PatientHistories");
                });

            modelBuilder.Entity("Asklepios.Core.Entities.Users.MedicalStaff", b =>
                {
                    b.Property<Guid>("DoctorId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("DepartmentId")
                        .HasColumnType("uuid");

                    b.Property<string>("HospitalPhoneNumber")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("character varying(12)");

                    b.Property<string>("MedicalLicenseNumber")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("character varying(12)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("PrivatePhoneNumber")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("character varying(12)");

                    b.Property<string>("Specialization")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("DoctorId");

                    b.HasIndex("DepartmentId")
                        .IsUnique();

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("MedicalStaff");
                });

            modelBuilder.Entity("Asklepios.Core.Entities.Users.Nurse", b =>
                {
                    b.Property<Guid>("NurseId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("DepartmentId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("NurseId");

                    b.HasIndex("DepartmentId")
                        .IsUnique();

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Nurses");
                });

            modelBuilder.Entity("Asklepios.Core.Entities.Users.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.HasKey("UserId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Asklepios.Core.Views.MonthlyAdmissionSummary", b =>
                {
                    b.Property<DateTime>("AdmissionMonth")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("admissionmonth");

                    b.Property<int>("PatientCount")
                        .HasColumnType("integer")
                        .HasColumnName("patientcount");

                    b.ToTable((string)null);

                    b.ToView("monthlyadmissions", (string)null);
                });

            modelBuilder.Entity("Asklepios.Core.Views.MonthlyDischargeSummary", b =>
                {
                    b.Property<DateTime>("DischargeMonth")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("dischargemonth");

                    b.Property<int>("PatientCount")
                        .HasColumnType("integer")
                        .HasColumnName("patientcount");

                    b.ToTable((string)null);

                    b.ToView("monthlydischarges", (string)null);
                });

            modelBuilder.Entity("Asklepios.Core.Entities.Departments.Room", b =>
                {
                    b.HasOne("Asklepios.Core.Entities.Departments.Department", "Department")
                        .WithMany("Rooms")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("Asklepios.Core.Entities.Examinations.ExamResult", b =>
                {
                    b.HasOne("Asklepios.Core.Entities.Examinations.Examination", "Examination")
                        .WithMany("ExamResults")
                        .HasForeignKey("ExaminationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Asklepios.Core.Entities.Patients.Patient", "Patient")
                        .WithMany("ExamResults")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Examination");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("Asklepios.Core.Entities.Examinations.Operation", b =>
                {
                    b.HasOne("Asklepios.Core.Entities.Users.MedicalStaff", "MedicalStaff")
                        .WithMany("Operations")
                        .HasForeignKey("MedicalStaffId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Asklepios.Core.Entities.Patients.Patient", "Patient")
                        .WithMany("Operations")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MedicalStaff");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("Asklepios.Core.Entities.Patients.Discharge", b =>
                {
                    b.HasOne("Asklepios.Core.Entities.Users.MedicalStaff", "MedicalStaff")
                        .WithMany("Discharges")
                        .HasForeignKey("MedicalStaffId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MedicalStaff");
                });

            modelBuilder.Entity("Asklepios.Core.Entities.Patients.Patient", b =>
                {
                    b.HasOne("Asklepios.Core.Entities.Departments.Department", "Department")
                        .WithMany("Patients")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Asklepios.Core.Entities.Users.MedicalStaff", "MedicalStaff")
                        .WithMany("Patients")
                        .HasForeignKey("MedicalStaffId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Asklepios.Core.Entities.Departments.Room", "Room")
                        .WithMany("Patients")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("MedicalStaff");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("Asklepios.Core.Entities.Users.MedicalStaff", b =>
                {
                    b.HasOne("Asklepios.Core.Entities.Departments.Department", "Department")
                        .WithOne("MedicalStaff")
                        .HasForeignKey("Asklepios.Core.Entities.Users.MedicalStaff", "DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Asklepios.Core.Entities.Users.User", "User")
                        .WithOne("MedicalStaff")
                        .HasForeignKey("Asklepios.Core.Entities.Users.MedicalStaff", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Asklepios.Core.Entities.Users.Nurse", b =>
                {
                    b.HasOne("Asklepios.Core.Entities.Departments.Department", "Department")
                        .WithOne("Nurse")
                        .HasForeignKey("Asklepios.Core.Entities.Users.Nurse", "DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Asklepios.Core.Entities.Users.User", "User")
                        .WithOne("Nurse")
                        .HasForeignKey("Asklepios.Core.Entities.Users.Nurse", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Asklepios.Core.Entities.Departments.Department", b =>
                {
                    b.Navigation("MedicalStaff")
                        .IsRequired();

                    b.Navigation("Nurse")
                        .IsRequired();

                    b.Navigation("Patients");

                    b.Navigation("Rooms");
                });

            modelBuilder.Entity("Asklepios.Core.Entities.Departments.Room", b =>
                {
                    b.Navigation("Patients");
                });

            modelBuilder.Entity("Asklepios.Core.Entities.Examinations.Examination", b =>
                {
                    b.Navigation("ExamResults");
                });

            modelBuilder.Entity("Asklepios.Core.Entities.Patients.Patient", b =>
                {
                    b.Navigation("ExamResults");

                    b.Navigation("Operations");
                });

            modelBuilder.Entity("Asklepios.Core.Entities.Users.MedicalStaff", b =>
                {
                    b.Navigation("Discharges");

                    b.Navigation("Operations");

                    b.Navigation("Patients");
                });

            modelBuilder.Entity("Asklepios.Core.Entities.Users.User", b =>
                {
                    b.Navigation("MedicalStaff")
                        .IsRequired();

                    b.Navigation("Nurse")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
