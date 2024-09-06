using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Asklepios.Infrastructure.DAL.Migrations
{
    /// <inheritdoc />
    public partial class CreateMonthlyDischargeView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"CREATE VIEW MonthlyDischarges AS
              SELECT 
                  DATE_TRUNC('month', CAST(""Date"" AS DATE)) AS DischargeMonth,
                  COUNT(*) AS PatientCount
              FROM 
                  ""Discharges""
              GROUP BY 
                  DATE_TRUNC('month', CAST(""Date"" AS DATE))
              ORDER BY 
                  DischargeMonth;"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW IF EXISTS MonthlyDischarges;");
        }
    }
}
