
using Asklepios.Core.Entities.Patients;

namespace Asklepios.Core.Entities.Examinations;

public class ExamResult
{
    public Guid ExamResultId { get; set; }
    
    public Guid PatientId { get; set; }
    public Patient Patient { get; set; }
    
    public DateOnly Date { get; set; }
    public string Result { get; set; }
    public string Comment { get; set; }
    
    public int ExaminationId { get; set; }
    public Examination Examination { get; set; }
}