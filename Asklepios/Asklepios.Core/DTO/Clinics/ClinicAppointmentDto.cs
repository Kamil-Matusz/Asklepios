using Asklepios.Core.ValueObjects;

namespace Asklepios.Core.DTO.Clinics;

public class ClinicAppointmentDto
{
    public Guid AppointmentId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public AppointmentType AppointmentType { get; set; }
    public Guid ClinicPatientId { get; set; }
    public Guid MedicalStaffId { get; set; }
    public string Status { get; set; }
}