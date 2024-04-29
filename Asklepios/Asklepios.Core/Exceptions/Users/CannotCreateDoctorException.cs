namespace Asklepios.Core.Exceptions.Users;

public class CannotCreateDoctorException : CustomException
{
    public CannotCreateDoctorException() : base("The doctor cannot be added because the user has the wrong role")
    {
    }
}