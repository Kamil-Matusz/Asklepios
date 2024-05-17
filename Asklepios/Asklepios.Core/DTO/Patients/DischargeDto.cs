namespace Asklepios.Core.DTO.Patients;

public class DischargeDto
{
    public Guid DischargeId { get; set; }
    public string PatientName { get; set; }
    public string PatientSurname { get; set; }
    public string PeselNumber { get; set; }
    public string Address { get; set; }
    public DateOnly Date { get; set; }
    public string DischargeReasson { get; set; }
    public string Summary { get; set; }
    public Guid MedicalStaffId { get; set; }
}