using System.Text.Json;
using Asklepios.Core.DTO.Users;
using Asklepios.Core.Repositories.Users;
using Microsoft.Extensions.Caching.Distributed;

namespace Asklepios.Infrastructure.Redis.Repositories;

public class AccountCacheRepository : IAccountCacheRepository
{
    private readonly IDistributedCache _cache;
    private readonly TimeSpan _cacheDuration = TimeSpan.FromHours(12);

    public AccountCacheRepository(IDistributedCache cache)
    {
        _cache = cache;
    }

    private string GetAccountKey(Guid userId) => $"Asklepios_Account_{userId}";

    public async Task<AccountDto?> GetAccountAsync(Guid userId)
    {
        var cacheKey = GetAccountKey(userId);
        var cachedData = await _cache.GetStringAsync(cacheKey);
        return cachedData != null ? JsonSerializer.Deserialize<AccountDto>(cachedData) : null;
    }

    public async Task SetAccountAsync(Guid userId, AccountDto account)
    {
        var cacheKey = GetAccountKey(userId);
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = _cacheDuration
        };

        await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(account), options);
    }
}