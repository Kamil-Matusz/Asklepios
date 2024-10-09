namespace Asklepios.Core.Exceptions.Clinics;

public class InvalidAppointmentTypeException : CustomException
{
    public string AppointmentType { get; }

    public InvalidAppointmentTypeException(string appointmentType) : base($" '{appointmentType}' is invalid.")
    {
        AppointmentType = appointmentType;
    }
}