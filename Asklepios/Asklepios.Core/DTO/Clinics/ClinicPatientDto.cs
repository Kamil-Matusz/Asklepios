namespace Asklepios.Core.DTO.Clinics;

public class ClinicPatientDto
{
    public Guid ClinicPatientId { get; set; }
    public string PatientName { get; set; }
    public string PatientSurname { get; set; }
    public string PeselNumber { get; set; }
    public string ContactNumber { get; set; }
    public string Email { get; set; }
}