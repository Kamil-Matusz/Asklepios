namespace Asklepios.Core.DTO.Patients;

public class PatientVisitDto
{
    public DateOnly AdmissionDate { get; set; }
    public DateOnly DischargeDate { get; set; }
    public string OperationName { get; set; }
    public string Result { get; set; }
    public string Comlications { get; set; }
    public string AnesthesiaType { get; set; }
}