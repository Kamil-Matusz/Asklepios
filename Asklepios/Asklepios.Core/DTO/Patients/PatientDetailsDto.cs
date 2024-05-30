using Asklepios.Core.DTO.Examinations;

namespace Asklepios.Core.DTO.Patients;

public class PatientDetailsDto : PatientDto
{
    public List<OperationDto> Operations { get; set; }
    public List<ExamResultDto> ExamResults { get; set; }
}