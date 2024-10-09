namespace Asklepios.Core.Exceptions.Clinics;

public class ClinicPatientNotFoundException : CustomException
{
    public Guid Id { get; set; }
    
    public ClinicPatientNotFoundException(Guid id) : base($"Patient with ID: '{id}' was not found.")
    {
        Id = id;
    }
}