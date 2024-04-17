using Asklepios.Core.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Asklepios.Infrastructure.DAL.PostgreSQL.Configurations;

public class NurseConfiguration : IEntityTypeConfiguration<Nurse>
{
    public void Configure(EntityTypeBuilder<Nurse> builder)
    {
        builder.HasKey(x => x.NurseId);
        builder.Property(x => x.NurseId).IsRequired();
        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Surname).IsRequired().HasMaxLength(100);
        
        builder.HasOne(n => n.User)
            .WithOne(u => u.Nurse)
            .HasForeignKey<Nurse>(n => n.UserId)
            .IsRequired();
        
        builder.HasOne(n => n.Department)
            .WithOne(d => d.Nurse)
            .HasForeignKey<Nurse>(n => n.DepartmentId)
            .IsRequired();
    }
}