using Asklepios.Core.Entities.Departments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Asklepios.Infrastructure.DAL.PostgreSQL.Configurations;

internal class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.HasKey(x => x.DepartmentId);
        builder.Property(x => x.DepartmentId).IsRequired();
        builder.Property(x => x.DepartmentId).IsRequired().HasMaxLength(300);
        builder.Property(x => x.NumberOfBeds).IsRequired();
        builder.Property(x => x.ActualNumberOfPatients).IsRequired();
    }
}