namespace Asklepios.Core.DTO.Patients;

public class PatientListDto
{
    public Guid PatientId { get; set; }
    public string PatientName { get; set; }
    public string PatientSurname { get; set; }
    public string PeselNumber { get; set; }
    public string InitialDiagnosis { get; set; }
    public bool IsDischarged { get; set; }
    public string Treatment { get; set; }
    public string DepartmentName { get; set; }
    public int RoomNumber { get; set; }
    public DateOnly AdmissionDate { get; set; }
    public string Address { get; set; }
}