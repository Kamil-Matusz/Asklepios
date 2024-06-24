using Asklepios.Core.Entities.Examinations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Asklepios.Infrastructure.DAL.PostgreSQL.Configurations;

internal class ExamResultConfiguration : IEntityTypeConfiguration<ExamResult>
{
    public void Configure(EntityTypeBuilder<ExamResult> builder)
    {
        builder.HasKey(x => x.ExamResultId);
        builder.Property(x => x.ExamResultId).IsRequired();
        builder.Property(x => x.PatientId).IsRequired();
        builder.Property(x => x.Date).IsRequired();
        builder.Property(x => x.Result).IsRequired().HasMaxLength(300);
        builder.Property(x => x.Comment).IsRequired().HasMaxLength(300);
        builder.Property(x => x.ExaminationId).IsRequired();
    }
}