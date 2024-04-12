namespace Asklepios.Core.Exceptions.Users;

public class CannotDeleteUserException : CustomException
{
    public Guid Id { get; set; }
    
    public CannotDeleteUserException(Guid id) : base($"User with ID: '{id}' cannot be deleted because his accout is active.")
    {
        Id = id;
    }
}