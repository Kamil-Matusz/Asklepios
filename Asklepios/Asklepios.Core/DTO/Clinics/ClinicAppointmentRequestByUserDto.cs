namespace Asklepios.Core.DTO.Clinics;

public class ClinicAppointmentRequestByUserDto
{
    public string PatientName { get; set; }
    public string PatientSurname { get; set; }
    public string PeselNumber { get; set; }
    public string ContactNumber { get; set; }
    public string Email { get; set; }
    
    public DateTime AppointmentDate { get; set; }
    public string AppointmentType { get; set; }
    public Guid MedicalStaffId { get; set; }
    
    public string Status { get; set; } = "Scheduled";

    public Guid UserId { get; set; }
}