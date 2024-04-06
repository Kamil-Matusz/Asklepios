using Asklepios.Core.Entities.Departments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Asklepios.Infrastructure.DAL.PostgreSQL.Configurations;

internal class RoomConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.HasKey(x => x.RoomId);
        builder.Property(x => x.RoomId).IsRequired();
        builder.Property(x => x.RoomNumber).IsRequired();
        builder.Property(x => x.RoomType).IsRequired().HasMaxLength(300);
        builder.Property(x => x.NumberOfBeds).IsRequired();
        builder.Property(x => x.DepartmentId).IsRequired();
    }
}