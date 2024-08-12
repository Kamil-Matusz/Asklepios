namespace Asklepios.Core.DTO.Patients;

public class DischargePersonDto
{
    public Guid PatientId { get; set; }
    public string DischargeReasson { get; set; }
    public string Summary { get; set; }
}