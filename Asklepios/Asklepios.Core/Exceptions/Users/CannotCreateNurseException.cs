namespace Asklepios.Core.Exceptions.Users;

public class CannotCreateNurseException : CustomException
{
    public CannotCreateNurseException() : base("The nurse cannot be added because the user has the wrong role.")
    {
    }
}