using Asklepios.Core.DTO.Users;

namespace Asklepios.Core.Repositories.Users;

public interface IAccountCacheRepository
{
    Task<AccountDto?> GetAccountAsync(Guid userId);
    Task SetAccountAsync(Guid userId, AccountDto account);
}