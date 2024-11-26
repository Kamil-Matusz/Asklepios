using Asklepios.Core.Entities.Users;
using Asklepios.Core.ValueObjects;

namespace Asklepios.Core.Entities.Clinics;

public class ClinicAppointment
{
    public Guid AppointmentId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public AppointmentType AppointmentType { get; set; }
    
    public Guid ClinicPatientId { get; set; }
    public ClinicPatient ClinicPatient { get; set; }

    public Guid MedicalStaffId { get; set; }
    public MedicalStaff MedicalStaff { get; set; }

    public string Status { get; set; }

    public ClinicAppointment(Guid appointmentId, DateTime appointmentDate, AppointmentType appointmentType, Guid clinicPatientId, Guid medicalStaffId, string status)
    {
        AppointmentId = appointmentId;
        AppointmentDate = appointmentDate;
        AppointmentType = appointmentType;
        ClinicPatientId = clinicPatientId;
        MedicalStaffId = medicalStaffId;
        Status = status;
    }

    public ClinicAppointment()
    {
    }
}