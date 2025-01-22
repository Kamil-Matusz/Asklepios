namespace Asklepios.Core.Entities.Clinics;

public class ClinicAppointmentDetails
{
    public Guid AppointmentDetailsId { get; set; }
    public string Description { get; set; }
    public string Recommendations { get; set; }
    public string Prescriptions { get; set; }
    public string DoctorComments { get; set; }
    public Guid AppointmentId { get; set; } 
    public ClinicAppointment ClinicAppointment { get; set; }
}