namespace Asklepios.Core.Exceptions.Users;

public class DoctorNotFoundException : CustomException
{
    public Guid Id { get; set; }
    
    public DoctorNotFoundException(Guid id) : base($"Doctor with ID: '{id}' was not found.")
    {
        Id = id;
    }
}