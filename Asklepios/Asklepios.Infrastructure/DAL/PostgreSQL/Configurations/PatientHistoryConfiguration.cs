using Asklepios.Core.Entities.Patients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Asklepios.Infrastructure.DAL.PostgreSQL.Configurations;

internal class PatientHistoryConfiguration : IEntityTypeConfiguration<PatientHistory>
{
    public void Configure(EntityTypeBuilder<PatientHistory> builder)
    {
        builder.HasKey(x => x.HistoryId);
        builder.Property(x => x.PatientName).IsRequired().HasMaxLength(200);
        builder.Property(x => x.PatientSurname).IsRequired().HasMaxLength(200);
        builder.HasIndex(x => x.PeselNumber).IsUnique();
        builder.Property(x => x.PeselNumber).IsRequired().HasMaxLength(11);
        builder.Property(ph => ph.History).HasColumnType("jsonb");
    }
}