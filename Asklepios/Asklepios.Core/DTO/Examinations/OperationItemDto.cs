namespace Asklepios.Core.DTO.Examinations;

public class OperationItemDto
{
    public Guid OperationId { get; set; }
    public string OperationName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string AnesthesiaType { get; set; }
    public string Result { get; set; }
    public string Complications { get; set; }
    public string DoctorName { get; set; }
    public string DoctornSurname { get; set; }
    public string PatientName { get; set; }
    public string PatientSurname { get; set; }
}