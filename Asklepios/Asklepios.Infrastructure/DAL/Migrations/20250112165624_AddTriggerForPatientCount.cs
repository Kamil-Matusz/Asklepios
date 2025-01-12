using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Asklepios.Infrastructure.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddTriggerForPatientCount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
        CREATE OR REPLACE FUNCTION update_actual_number_of_patients()
        RETURNS TRIGGER AS $$
        BEGIN
            UPDATE ""Departments""
            SET ""ActualNumberOfPatients"" = (
                SELECT COUNT(*) 
                FROM ""Patients"" 
                WHERE ""DepartmentId"" = NEW.""DepartmentId""
            )
            WHERE ""DepartmentId"" = NEW.""DepartmentId"";

            RETURN NEW;
        END;
        $$ LANGUAGE plpgsql;");
            
            migrationBuilder.Sql(@"
        CREATE TRIGGER trigger_update_actual_number_of_patients
        AFTER INSERT OR DELETE OR UPDATE OF ""DepartmentId""
        ON ""Patients""
        FOR EACH ROW
        EXECUTE FUNCTION update_actual_number_of_patients();");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
        DROP TRIGGER IF EXISTS trigger_update_actual_number_of_patients ON ""Patient"";");

            // Usuwanie funkcji
            migrationBuilder.Sql(@"
        DROP FUNCTION IF EXISTS update_actual_number_of_patients;");
        }
    }
}
