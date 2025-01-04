using Asklepios.Core.Entities.Clinics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Asklepios.Infrastructure.DAL.PostgreSQL.Configurations;

internal class ClinicAppointmentDetailsConfiguration : IEntityTypeConfiguration<ClinicAppointmentDetails>
{
    public void Configure(EntityTypeBuilder<ClinicAppointmentDetails> builder)
    {
        builder.HasKey(x => x.AppointmentDetailsId);
        builder.Property(x => x.Description).IsRequired();
        builder.Property(x => x.Recommendations);
        builder.Property(x => x.Prescriptions);
        builder.Property(x => x.DoctorComments);
        builder.Property(x => x.AppointmentId).IsRequired();
        
        builder.HasOne(n => n.ClinicAppointment)
            .WithOne(u => u.ClinicAppointmentDetails)
            .HasForeignKey<ClinicAppointmentDetails>(n => n.AppointmentId)
            .IsRequired();
    }
}