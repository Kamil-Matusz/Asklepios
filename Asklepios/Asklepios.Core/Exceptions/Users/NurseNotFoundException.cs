namespace Asklepios.Core.Exceptions.Users;

public class NurseNotFoundException : CustomException
{
    public Guid Id { get; set; }
    
    public NurseNotFoundException(Guid id) : base($"Nurse with ID: '{id}' was not found.")
    {
        Id = id;
    }
}