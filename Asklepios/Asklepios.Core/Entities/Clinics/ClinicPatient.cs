using Asklepios.Core.Entities.Users;

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
    
    public Guid? UserId { get; set; }
    public User User { get; set; }
    
    public ClinicPatient(Guid clinicPatientId, string patientName, string patientSurname, string peselNumber, string contactNumber, string email, Guid? userId = null)
    {
        ClinicPatientId = clinicPatientId;
        PatientName = patientName;
        PatientSurname = patientSurname;
        PeselNumber = peselNumber;
        ContactNumber = contactNumber;
        Email = email;
        UserId = userId;
        Appointments = new List<ClinicAppointment>();
    }

    public ClinicPatient()
    {
    }
}