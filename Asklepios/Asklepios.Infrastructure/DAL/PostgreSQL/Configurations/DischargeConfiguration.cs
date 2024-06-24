using Asklepios.Core.Entities.Patients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Asklepios.Infrastructure.DAL.PostgreSQL.Configurations;

internal class DischargeConfiguration : IEntityTypeConfiguration<Discharge>
{
    public void Configure(EntityTypeBuilder<Discharge> builder)
    {
        builder.HasKey(x => x.DischargeId);
        builder.Property(x => x.PatientName).IsRequired().HasMaxLength(200);
        builder.Property(x => x.PatientSurname).IsRequired().HasMaxLength(200);
        builder.HasIndex(x => x.PeselNumber).IsUnique();
        builder.Property(x => x.PeselNumber).IsRequired().HasMaxLength(11);
        builder.Property(x => x.Address).IsRequired().HasMaxLength(200);
        builder.Property(x => x.Date).IsRequired();
        builder.Property(x => x.DischargeReasson).IsRequired().HasMaxLength(2000);
        builder.Property(x => x.Summary).IsRequired().HasMaxLength(5000);
        builder.Property(x => x.MedicalStaffId).IsRequired();
    }
}