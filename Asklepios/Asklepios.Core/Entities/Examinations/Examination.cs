using Asklepios.Core.ValueObjects;

namespace Asklepios.Core.Entities.Examinations;

public class Examination
{
    public int ExaminationId { get; set; }
    public string ExamName { get; set; }
    public ExamCategory ExamCategory { get; set; }
    
    public IEnumerable<ExamResult> ExamResults { get; set; }
}