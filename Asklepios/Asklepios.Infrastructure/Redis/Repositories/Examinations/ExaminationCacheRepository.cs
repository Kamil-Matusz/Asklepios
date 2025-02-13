using System.Text.Json;
using Asklepios.Core.DTO.Examinations;
using Asklepios.Core.Repositories.Examinations;
using Microsoft.Extensions.Caching.Distributed;

namespace Asklepios.Infrastructure.Redis.Repositories.Examinations;

public class ExaminationCacheRepository : IExaminationCacheRepository
{
    private readonly IDistributedCache _cache;
    private readonly TimeSpan _cacheDuration = TimeSpan.FromDays(5);

    public ExaminationCacheRepository(IDistributedCache cache)
    {
        _cache = cache;
    }
    
    private string GetCacheKey(int pageIndex, int pageSize) 
        => $"Asklepios_Examinations_{pageIndex}_{pageSize}";
    
    public async Task<IReadOnlyList<ExaminationDto>?> GetExaminationsAsync(int pageIndex, int pageSize)
    {
        var cacheKey = GetCacheKey(pageIndex, pageSize);
        var cachedData = await _cache.GetStringAsync(cacheKey);
        return cachedData != null ? JsonSerializer.Deserialize<IReadOnlyList<ExaminationDto>>(cachedData) : null;
    }

    public async Task SetExaminationsAsync(int pageIndex, int pageSize, IReadOnlyList<ExaminationDto> examinations)
    {
        var cacheKey = GetCacheKey(pageIndex, pageSize);
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = _cacheDuration
        };
        
        await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(examinations), options);
    }
}