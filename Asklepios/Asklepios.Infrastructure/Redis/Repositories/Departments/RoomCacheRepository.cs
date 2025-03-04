using System.Text.Json;
using Asklepios.Core.DTO.Departments;
using Asklepios.Core.Repositories.Departments;
using Microsoft.Extensions.Caching.Distributed;

namespace Asklepios.Infrastructure.Redis.Repositories.Departments;

public class RoomCacheRepository : IRoomCacheRepository
{
    private readonly IDistributedCache _cache;
    private readonly TimeSpan _cacheDuration = TimeSpan.FromDays(1);

    public RoomCacheRepository(IDistributedCache cache)
    {
        _cache = cache;
    }

    private string GetCacheKey(int pageIndex, int pageSize) 
        => $"Asklepios_Rooms_{pageIndex}_{pageSize}";

    public async Task<IReadOnlyList<RoomListDto>?> GetRoomsAsync(int pageIndex, int pageSize)
    {
        var cacheKey = GetCacheKey(pageIndex, pageSize);
        var cachedData = await _cache.GetStringAsync(cacheKey);
        return cachedData != null ? JsonSerializer.Deserialize<IReadOnlyList<RoomListDto>>(cachedData) : null;
    }

    public async Task SetRoomsAsync(int pageIndex, int pageSize, IReadOnlyList<RoomListDto> rooms)
    {
        var cacheKey = GetCacheKey(pageIndex, pageSize);
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = _cacheDuration
        };

        await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(rooms), options);
    }
}