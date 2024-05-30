using Asklepios.Core.Entities.Examinations;
using Asklepios.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Asklepios.Infrastructure.DAL.PostgreSQL.Configurations;

internal class ExaminationConfiguration : IEntityTypeConfiguration<Examination>
{
    public void Configure(EntityTypeBuilder<Examination> builder)
    {
        builder.HasKey(x => x.ExaminationId);
        builder.Property(x => x.ExaminationId).IsRequired();
        builder.Property(x => x.ExamName).IsRequired().HasMaxLength(300);
        builder.Property(x => x.ExamCategory)
            .HasConversion(x => x.Value, x => new ExamCategory(x))
            .IsRequired()
            .HasMaxLength(300);
    }
}