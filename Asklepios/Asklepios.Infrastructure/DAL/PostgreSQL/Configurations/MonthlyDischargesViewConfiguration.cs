using Asklepios.Core.Entities.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Asklepios.Infrastructure.DAL.PostgreSQL.Configurations;

internal class MonthlyDischargeSummaryConfiguration : IEntityTypeConfiguration<MonthlyDischargeSummary>
{
    public void Configure(EntityTypeBuilder<MonthlyDischargeSummary> builder)
    {
        builder.ToView("monthlyDischarges")
            .HasNoKey();
        
        builder.Property(e => e.DischargeMonth)
            .HasColumnName("dischargemonth");

        builder.Property(e => e.PatientCount)
            .HasColumnName("patientcount");
    }
}