using Asklepios.Core.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Asklepios.Infrastructure.DAL.PostgreSQL.Configurations;

public class MonthlyAdmissionsViewConfiguration : IEntityTypeConfiguration<MonthlyAdmissionSummary>
{
    public void Configure(EntityTypeBuilder<MonthlyAdmissionSummary> builder)
    {
        builder.ToView("monthlyDischarges")
            .HasNoKey();
        
        builder.Property(e => e.AdmissionMonth)
            .HasColumnName("admissionmonth");

        builder.Property(e => e.PatientCount)
            .HasColumnName("patientcount");
    }
}