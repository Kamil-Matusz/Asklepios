using Asklepios.Core.Entities.Examinations;
using Asklepios.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Asklepios.Infrastructure.DAL.PostgreSQL.Configurations;

public class OperationConfiguration : IEntityTypeConfiguration<Operation>
{
    public void Configure(EntityTypeBuilder<Operation> builder)
    {
        builder.HasKey(x => x.OperationId);
        builder.Property(x => x.OperationId).IsRequired();
        builder.Property(x => x.OperationName).IsRequired().HasMaxLength(2000);
        builder.Property(x => x.StartDate).IsRequired();
        builder.Property(x => x.EndDate).IsRequired();
        builder.Property(x => x.AnesthesiaType).HasConversion(x => x.Value, x => new AnesthesiaType(x)).IsRequired();
        builder.Property(x => x.Comlications).HasMaxLength(10000);
        builder.Property(x => x.Result).IsRequired().HasMaxLength(2000);
        builder.Property(x => x.PatientId).IsRequired();
        builder.Property(x => x.MedicalStaffId).IsRequired();
    }
}