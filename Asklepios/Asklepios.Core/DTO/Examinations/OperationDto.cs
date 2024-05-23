namespace Asklepios.Core.DTO.Examinations;

public class OperationDto
{
    public Guid OperationId { get; set; }
    public string OperationName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string AnesthesiaType { get; set; }
    public string Result { get; set; }
    public string Comlications { get; set; }
    public Guid PatientId { get; set; }
    public Guid MedicalStaffId { get; set; }
}