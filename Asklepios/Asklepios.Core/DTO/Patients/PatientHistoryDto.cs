namespace Asklepios.Core.DTO.Patients;

public class PatientHistoryDto
{
    public Guid HistoryId { get; set; }
    public string PatientName { get; set; }
    public string PatientSurname { get; set; }
    public string PeselNumber { get; set; }
    public List<PatientVisitDto> History { get; set; } = new();
}