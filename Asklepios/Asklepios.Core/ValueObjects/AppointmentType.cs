using Asklepios.Core.Exceptions.Clinics;

namespace Asklepios.Core.ValueObjects;

public sealed record AppointmentType
{
    public static IEnumerable<string> Appointment { get; } = new[] { "Consultation", "Examination","SpecialistConsultation", "Other"};
    
    public string Value { get; }

    public AppointmentType(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > 80)
        {
            throw new InvalidAppointmentTypeException(value);
        }

        if (!Appointment.Contains(value))
        {
            throw new InvalidAppointmentTypeException(value);
        }

        Value = value;
    }
    
    public static AppointmentType Consultation() => new("Consultation");
    public static AppointmentType Examination() => new("Examination");
    public static AppointmentType SpecialistConsultation() => new("SpecialistConsultation");
    public static AppointmentType Other() => new("Other");
    
    public static implicit operator AppointmentType(string value) => new AppointmentType(value);

    public static implicit operator string(AppointmentType value) => value?.Value;

    public override string ToString() => Value;
}