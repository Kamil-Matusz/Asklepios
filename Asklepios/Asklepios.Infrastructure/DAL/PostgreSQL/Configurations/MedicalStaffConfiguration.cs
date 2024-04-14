using Asklepios.Core.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Asklepios.Infrastructure.DAL.PostgreSQL.Configurations;

public class MedicalStaffConfiguration : IEntityTypeConfiguration<MedicalStaff>
{
    public void Configure(EntityTypeBuilder<MedicalStaff> builder)
    {
        builder.HasKey(x => x.DoctorId);
        builder.Property(x => x.DoctorId).IsRequired();
        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Surname).IsRequired().HasMaxLength(100);
        builder.Property(x => x.PrivatePhoneNumber).IsRequired().HasMaxLength(12);
        builder.Property(x => x.HospitalPhoneNumber).IsRequired().HasMaxLength(12);
        builder.Property(x => x.Specialization).IsRequired().HasMaxLength(200);
        builder.Property(x => x.MedicalLicenseNumber).IsRequired().HasMaxLength(12);
    }
}