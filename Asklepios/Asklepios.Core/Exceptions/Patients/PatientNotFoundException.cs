namespace Asklepios.Core.Exceptions.Patients;

public class PatientNotFoundException : CustomException
{
    public Guid Id { get; set; }
    
    public PatientNotFoundException(Guid id) : base($"Patient with ID: '{id}' was not found.")
    {
        Id = id;
    }
}