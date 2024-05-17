namespace Asklepios.Core.DTO.Patients;

public class PatientDto
{
    public Guid PatientId { get; set; }
    public string PatientName { get; set; }
    public string PatientSurname { get; set; }
    public string PeselNumber { get; set; }
    public string InitialDiagnosis { get; set; }
    public bool IsDischarged { get; set; }
    public string Treatment { get; set; }
    public Guid DepartmentId { get; set; }
    public Guid RoomId { get; set; }
}