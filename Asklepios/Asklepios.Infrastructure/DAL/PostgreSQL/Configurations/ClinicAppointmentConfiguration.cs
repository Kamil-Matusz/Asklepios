using Asklepios.Core.Entities.Clinics;
using Asklepios.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Asklepios.Infrastructure.DAL.PostgreSQL.Configurations;

internal class ClinicAppointmentConfiguration : IEntityTypeConfiguration<ClinicAppointment>
{
    public void Configure(EntityTypeBuilder<ClinicAppointment> builder)
    {
        builder.HasKey(x => x.AppointmentId);
        builder.Property(x => x.AppointmentDate).IsRequired();
        builder.Property(x => x.AppointmentType).HasConversion(x => x.Value, x => new AppointmentType(x)).IsRequired();
        builder.Property(x => x.ClinicPatientId).IsRequired();
        builder.Property(x => x.MedicalStaffId).IsRequired();
        builder.Property(x => x.Status).IsRequired().HasMaxLength(300);
    }
}