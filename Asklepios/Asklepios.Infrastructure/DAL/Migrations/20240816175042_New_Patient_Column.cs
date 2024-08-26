using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Asklepios.Infrastructure.DAL.Migrations
{
    /// <inheritdoc />
    public partial class New_Patient_Column : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MedicalStaffId",
                table: "Patients",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Patients_MedicalStaffId",
                table: "Patients",
                column: "MedicalStaffId");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_MedicalStaff_MedicalStaffId",
                table: "Patients",
                column: "MedicalStaffId",
                principalTable: "MedicalStaff",
                principalColumn: "DoctorId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_MedicalStaff_MedicalStaffId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_MedicalStaffId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "MedicalStaffId",
                table: "Patients");
        }
    }
}
