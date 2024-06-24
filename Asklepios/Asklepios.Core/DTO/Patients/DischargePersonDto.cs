namespace Asklepios.Core.DTO.Patients;

public class DischargePersonDto
{
    public Guid PatientId { get; set; }
    public DateOnly DischargeDate { get; set; }
    public string DischargeReason { get; set; }
    public string Summary { get; set; }
    public Guid MedicalStaffId { get; set; }
}