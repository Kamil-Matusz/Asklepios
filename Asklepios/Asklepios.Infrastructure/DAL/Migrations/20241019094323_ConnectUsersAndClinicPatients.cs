using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Asklepios.Infrastructure.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ConnectUsersAndClinicPatients : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "ClinicPatients",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClinicPatients_UserId",
                table: "ClinicPatients",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClinicPatients_Users_UserId",
                table: "ClinicPatients",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClinicPatients_Users_UserId",
                table: "ClinicPatients");

            migrationBuilder.DropIndex(
                name: "IX_ClinicPatients_UserId",
                table: "ClinicPatients");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ClinicPatients");
        }
    }
}
