namespace Asklepios.Core.Exceptions.Users;

public sealed class InvalidRoleException : CustomException
{
    public string Role { get; }

    public InvalidRoleException(string role) : base($"Role: '{role}' is invalid.")
    {
        Role = role;
    }
}