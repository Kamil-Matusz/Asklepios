using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Asklepios.Infrastructure.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Operations_Tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Operations",
                columns: table => new
                {
                    OperationId = table.Column<Guid>(type: "uuid", nullable: false),
                    OperationName = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    AnesthesiaType = table.Column<string>(type: "text", nullable: false),
                    Result = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    Comlications = table.Column<string>(type: "character varying(10000)", maxLength: 10000, nullable: false),
                    PatientId = table.Column<Guid>(type: "uuid", nullable: false),
                    MedicalStaffId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operations", x => x.OperationId);
                    table.ForeignKey(
                        name: "FK_Operations_MedicalStaff_MedicalStaffId",
                        column: x => x.MedicalStaffId,
                        principalTable: "MedicalStaff",
                        principalColumn: "DoctorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Operations_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Operations_MedicalStaffId",
                table: "Operations",
                column: "MedicalStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Operations_PatientId",
                table: "Operations",
                column: "PatientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Operations");
        }
    }
}
