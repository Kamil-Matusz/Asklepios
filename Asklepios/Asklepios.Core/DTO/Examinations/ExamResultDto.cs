namespace Asklepios.Core.DTO.Examinations;

public class ExamResultDto
{
    public Guid ExamResultId { get; set; }
    public Guid PatientId { get; set; }
    public DateOnly Date { get; set; }
    public string Result { get; set; }
    public string Comment { get; set; }
    public int ExamId { get; set; }
}