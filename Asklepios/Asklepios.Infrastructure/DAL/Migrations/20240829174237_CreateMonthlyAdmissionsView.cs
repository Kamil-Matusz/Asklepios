using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Asklepios.Infrastructure.DAL.Migrations
{
    /// <inheritdoc />
    public partial class CreateMonthlyAdmissionsView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"CREATE VIEW MonthlyAdmissions AS
                  SELECT 
                      DATE_TRUNC('month', CAST(""AdmissionDate"" AS DATE)) AS AdmissionMonth,
                      COUNT(*) AS PatientCount
                  FROM 
                      ""Patients""
                  GROUP BY 
                      DATE_TRUNC('month', CAST(""AdmissionDate"" AS DATE))
                  ORDER BY 
                      AdmissionMonth;"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW IF EXISTS MonthlyAdmissions;");
        }
    }
}
