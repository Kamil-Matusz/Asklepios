using Asklepios.Core.ValueObjects;

namespace Asklepios.Core.DTO.Clinics;

public class ClinicAppointmentListDto
{
    public Guid AppointmentId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public AppointmentType AppointmentType { get; set; }
    public string PatientName { get; set; }
    public string PatientSurname { get; set; }
    public string PeselNumber { get; set; }
    public string DoctorName { get; set; }
    public string DoctorSurname { get; set; }
    public string Status { get; set; }
}