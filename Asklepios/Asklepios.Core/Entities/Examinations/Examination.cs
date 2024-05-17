using Asklepios.Core.ValueObjects;

namespace Asklepios.Core.Entities.Examinations;

public class Examination
{
    public int ExamId { get; set; }
    public string ExamName { get; set; }
    public ExamCategory ExamCategory { get; set; }
}