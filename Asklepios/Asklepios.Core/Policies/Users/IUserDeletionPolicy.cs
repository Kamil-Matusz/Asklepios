using Asklepios.Core.Entities.Users;

namespace Asklepios.Core.Policies.Users;

public interface IUserDeletionPolicy
{
    Task<bool> CannotDeleteUserPolicy(Guid userId);
}