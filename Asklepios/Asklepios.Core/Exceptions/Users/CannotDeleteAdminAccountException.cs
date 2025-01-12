namespace Asklepios.Core.Exceptions.Users;

public class CannotDeleteAdminAccountException : CustomException
{
    public Guid Id { get; set; }
    
    public CannotDeleteAdminAccountException(Guid id) : base($"User with ID: '{id}' cannot be deleted because this Admin account.")
    {
        Id = id;
    }
}