using Asklepios.Core.ValueObjects;

namespace Asklepios.Core.DTO.Clinics;

public class ClinicAppointmentRequestDto
{
    public string PatientName { get; set; }
    public string PatientSurname { get; set; }
    public string PeselNumber { get; set; }
    public string ContactNumber { get; set; }
    public string Email { get; set; }
    
    public DateTime AppointmentDate { get; set; }
    public AppointmentType AppointmentType { get; set; }
    public Guid MedicalStaffId { get; set; }
    
    public string Status { get; set; } = "Scheduled"; 
}