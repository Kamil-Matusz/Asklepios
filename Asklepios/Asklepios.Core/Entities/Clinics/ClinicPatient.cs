namespace Asklepios.Core.Entities.Clinics;

public class ClinicPatient
{
    public Guid ClinicPatientId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string PeselNumber { get; set; }
    public string ContactNumber { get; set; }
    public string Email { get; set; }
}