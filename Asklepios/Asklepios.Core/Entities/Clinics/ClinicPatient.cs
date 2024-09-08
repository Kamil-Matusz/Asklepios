namespace Asklepios.Core.Entities.Clinics;

public class ClinicPatient
{
    public Guid ClinicPatientId { get; set; }
    public string PatientName { get; set; }
    public string PatientSurname { get; set; }
    public string PeselNumber { get; set; }
    public string ContactNumber { get; set; }
    public string Email { get; set; }
    
    public IEnumerable<ClinicAppointment> Appointments { get; set; }
}