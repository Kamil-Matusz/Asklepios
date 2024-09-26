using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Asklepios.Infrastructure.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ClinicsModule_Tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClinicPatients",
                columns: table => new
                {
                    ClinicPatientId = table.Column<Guid>(type: "uuid", nullable: false),
                    PatientName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    PatientSurname = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    PeselNumber = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    ContactNumber = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicPatients", x => x.ClinicPatientId);
                });

            migrationBuilder.CreateTable(
                name: "ClinicAppointments",
                columns: table => new
                {
                    AppointmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    AppointmentDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    AppointmentType = table.Column<string>(type: "text", nullable: false),
                    ClinicPatientId = table.Column<Guid>(type: "uuid", nullable: false),
                    MedicalStaffId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicAppointments", x => x.AppointmentId);
                    table.ForeignKey(
                        name: "FK_ClinicAppointments_ClinicPatients_ClinicPatientId",
                        column: x => x.ClinicPatientId,
                        principalTable: "ClinicPatients",
                        principalColumn: "ClinicPatientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClinicAppointments_MedicalStaff_MedicalStaffId",
                        column: x => x.MedicalStaffId,
                        principalTable: "MedicalStaff",
                        principalColumn: "DoctorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClinicAppointments_ClinicPatientId",
                table: "ClinicAppointments",
                column: "ClinicPatientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClinicAppointments_MedicalStaffId",
                table: "ClinicAppointments",
                column: "MedicalStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_ClinicPatients_PeselNumber",
                table: "ClinicPatients",
                column: "PeselNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClinicAppointments");

            migrationBuilder.DropTable(
                name: "ClinicPatients");
        }
    }
}
