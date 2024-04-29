namespace Asklepios.Core.Policies.Users;

public interface IRolePolicy
{
    Task<bool> CannotCreateNurse(Guid userId);
    Task<bool> CannotCreateDoctor(Guid userId);
}