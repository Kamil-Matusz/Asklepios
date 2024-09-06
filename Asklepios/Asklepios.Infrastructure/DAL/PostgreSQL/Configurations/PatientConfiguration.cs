using Asklepios.Core.Entities.Patients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Asklepios.Infrastructure.DAL.PostgreSQL.Configurations;

internal class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.HasKey(x => x.PatientId);
        builder.Property(x => x.PatientName).IsRequired().HasMaxLength(200);
        builder.Property(x => x.PatientSurname).IsRequired().HasMaxLength(200);
        builder.HasIndex(x => x.PeselNumber).IsUnique();
        builder.Property(x => x.PeselNumber).IsRequired().HasMaxLength(11);
        builder.Property(x => x.IsDischarged).IsRequired();
        builder.Property(x => x.DepartmentId).IsRequired();
        builder.Property(x => x.RoomId).IsRequired();
        builder.Property(x => x.MedicalStaffId).IsRequired();
        builder.Property(x => x.AdmissionDate).IsRequired();
        builder.Property(x => x.Address).IsRequired().HasMaxLength(200);
    }
}