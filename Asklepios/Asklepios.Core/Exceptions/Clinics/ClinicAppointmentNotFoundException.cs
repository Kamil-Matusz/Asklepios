namespace Asklepios.Core.Exceptions.Clinics;

public class ClinicAppointmentNotFoundException : CustomException
{
    public Guid Id { get; set; }
    
    public ClinicAppointmentNotFoundException(Guid id) : base($"Appointment with ID: '{id}' was not found.")
    {
        Id = id;
    }
}