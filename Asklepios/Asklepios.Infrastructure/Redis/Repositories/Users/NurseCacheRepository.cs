using System.Text.Json;
using Asklepios.Core.DTO.Users;
using Asklepios.Core.Repositories.Users;
using Microsoft.Extensions.Caching.Distributed;

namespace Asklepios.Infrastructure.Redis.Repositories.Users;

public class NurseCacheRepository : INurseCacheRepository
{
    private readonly IDistributedCache _cache;
    private readonly TimeSpan _cacheDuration = TimeSpan.FromDays(1);

    public NurseCacheRepository(IDistributedCache cache)
    {
        _cache = cache;
    }
    
    private string GetNurseCacheKey(int pageIndex, int pageSize) 
        => $"Asklepios_Nurse_{pageIndex}_{pageSize}";

    public async Task<IReadOnlyList<NurseListDto>?> GetNursesAsync(int pageIndex, int pageSize)
    {
        var cacheKey = GetNurseCacheKey(pageIndex, pageSize);
        var cachedData = await _cache.GetStringAsync(cacheKey);
        return cachedData != null ? JsonSerializer.Deserialize<IReadOnlyList<NurseListDto>>(cachedData) : null;
    }

    public async Task SetNursesAsync(int pageIndex, int pageSize, IReadOnlyList<NurseListDto> nurseList)
    {
        var cacheKey = GetNurseCacheKey(pageIndex, pageSize);
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = _cacheDuration
        };

        await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(nurseList), options);
    }
}