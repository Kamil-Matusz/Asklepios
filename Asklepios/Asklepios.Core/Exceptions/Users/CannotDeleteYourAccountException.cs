namespace Asklepios.Core.Exceptions.Users;

public class CannotDeleteYourAccountException : CustomException
{
    public Guid Id { get; set; }
    
    public CannotDeleteYourAccountException(Guid id) : base($"User with ID: '{id}' cannot be deleted because this is your account.")
    {
        Id = id;
    }
}