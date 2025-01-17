namespace Asklepios.Core.Policies.Users;

public interface IUserDeletionPolicy
{
    Task<bool> CannotDeleteUserPolicy(Guid userId);
    Task<bool> CannotDeleteAdminAccount(Guid userId);
    Task<bool> CannotDeleteYourAccount(Guid currentUserId, Guid userId);
}