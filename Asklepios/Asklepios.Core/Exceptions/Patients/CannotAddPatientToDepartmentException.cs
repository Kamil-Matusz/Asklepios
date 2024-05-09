namespace Asklepios.Core.Exceptions.Patients;

public class CannotAddPatientToDepartmentException : CustomException
{
    public Guid Id { get; set; }
    
    public CannotAddPatientToDepartmentException(Guid id) : base("The patient cannot be added to the unit. No seats available.")
    {
        Id = id;
    }
}