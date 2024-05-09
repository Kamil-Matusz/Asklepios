namespace Asklepios.Core.Exceptions.Patients;

public class CannotAddPatientException : CustomException
{
    public Guid Id { get; set; }
    
    public CannotAddPatientException(Guid id) : base("The patient cannot be added to the unit. No seats available.")
    {
        Id = id;
    }
}