using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Asklepios.Infrastructure.DAL.Migrations
{
    /// <inheritdoc />
    public partial class FixColumnName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Comlications",
                table: "Operations",
                newName: "Complications");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Complications",
                table: "Operations",
                newName: "Comlications");
        }
    }
}
