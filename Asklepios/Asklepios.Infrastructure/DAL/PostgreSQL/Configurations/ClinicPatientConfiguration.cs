using Asklepios.Core.Entities.Clinics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Asklepios.Infrastructure.DAL.PostgreSQL.Configurations;

internal class ClinicPatientConfiguration : IEntityTypeConfiguration<ClinicPatient>
{
    public void Configure(EntityTypeBuilder<ClinicPatient> builder)
    {
        builder.HasKey(x => x.ClinicPatientId);
        builder.Property(x => x.PatientName).IsRequired().HasMaxLength(200);
        builder.Property(x => x.PatientSurname).IsRequired().HasMaxLength(200);
        builder.HasIndex(x => x.PeselNumber).IsUnique();
        builder.Property(x => x.PeselNumber).IsRequired().HasMaxLength(11);
        
        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .IsRequired(false);
    }
}