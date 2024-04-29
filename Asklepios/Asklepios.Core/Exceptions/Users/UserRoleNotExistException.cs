namespace Asklepios.Core.Exceptions.Users;

public class UserRoleNotExistException : CustomException
{
    public UserRoleNotExistException() : base("This role don't exist")
    {
    }
}