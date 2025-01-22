using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Asklepios.Infrastructure.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ClinicAppointmetDetails_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClinicAppointmentDetails",
                columns: table => new
                {
                    AppointmentDetailsId = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Recommendations = table.Column<string>(type: "text", nullable: false),
                    Prescriptions = table.Column<string>(type: "text", nullable: false),
                    DoctorComments = table.Column<string>(type: "text", nullable: false),
                    AppointmentId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicAppointmentDetails", x => x.AppointmentDetailsId);
                    table.ForeignKey(
                        name: "FK_ClinicAppointmentDetails_ClinicAppointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "ClinicAppointments",
                        principalColumn: "AppointmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClinicAppointmentDetails_AppointmentId",
                table: "ClinicAppointmentDetails",
                column: "AppointmentId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClinicAppointmentDetails");
        }
    }
}
