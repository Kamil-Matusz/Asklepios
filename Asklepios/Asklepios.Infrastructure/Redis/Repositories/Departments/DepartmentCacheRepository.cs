using System.Text.Json;
using Asklepios.Core.DTO.Departments;
using Asklepios.Core.Repositories.Departments;
using Microsoft.Extensions.Caching.Distributed;

namespace Asklepios.Infrastructure.Redis.Repositories.Departments;

public class DepartmentCacheRepository : IDepartmentCacheRepository
{
    private readonly IDistributedCache _cache;
    private readonly TimeSpan _cacheDuration = TimeSpan.FromDays(1);

    public DepartmentCacheRepository(IDistributedCache cache)
    {
        _cache = cache;
    }

    private string GetCacheKey(int pageIndex, int pageSize) 
        => $"Asklepios_Departments_{pageIndex}_{pageSize}";

    public async Task<IReadOnlyList<DepartmentListDto>?> GetDepartmentsAsync(int pageIndex, int pageSize)
    {
        var cacheKey = GetCacheKey(pageIndex, pageSize);
        var cachedData = await _cache.GetStringAsync(cacheKey);
        return cachedData != null ? JsonSerializer.Deserialize<IReadOnlyList<DepartmentListDto>>(cachedData) : null;
    }

    public async Task SetDepartmentsAsync(int pageIndex, int pageSize, IReadOnlyList<DepartmentListDto> departments)
    {
        var cacheKey = GetCacheKey(pageIndex, pageSize);
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = _cacheDuration
        };

        await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(departments), options);
    }
}