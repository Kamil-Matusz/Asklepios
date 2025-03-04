using System.Text.Json;
using Asklepios.Core.DTO.Statistics;
using Asklepios.Core.Repositories.Statistics;
using Microsoft.Extensions.Caching.Distributed;

namespace Asklepios.Infrastructure.Redis.Repositories.Statistics;

public class StatisticsCacheRepository : IStatisticsCacheRepository
{
    private readonly IDistributedCache _cache;
        private readonly TimeSpan _cacheDuration = TimeSpan.FromHours(1);

        public StatisticsCacheRepository(IDistributedCache cache)
        {
            _cache = cache;
        }

        private async Task<T?> GetFromCacheAsync<T>(string key)
        {
            var cachedData = await _cache.GetStringAsync(key);
            return cachedData != null ? JsonSerializer.Deserialize<T>(cachedData) : default;
        }

        private async Task SetToCacheAsync<T>(string key, T value)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = _cacheDuration
            };

            await _cache.SetStringAsync(key, JsonSerializer.Serialize(value), options);
        }

        private string GetDepartmentStatsKey(Guid departmentId) => $"Asklepios_DepartmentStats_{departmentId}";
        private string GetAllDepartmentStatsKey() => "Asklepios_AllDepartmentStats";
        private string GetTotalDepartmentsKey() => "Asklepios_TotalDepartments";
        private string GetTotalDoctorsKey() => "Asklepios_TotalDoctors";
        private string GetTotalNursesKey() => "Asklepios_TotalNurses";

        public async Task<DepartmentStatsDto?> GetDepartmentStatsAsync(Guid departmentId)
            => await GetFromCacheAsync<DepartmentStatsDto>(GetDepartmentStatsKey(departmentId));

        public async Task SetDepartmentStatsAsync(Guid departmentId, DepartmentStatsDto data)
            => await SetToCacheAsync(GetDepartmentStatsKey(departmentId), data);

        public async Task<DepartmentStatsDto?> GetAllDepartmentStatsAsync()
            => await GetFromCacheAsync<DepartmentStatsDto>(GetAllDepartmentStatsKey());

        public async Task SetAllDepartmentStatsAsync(DepartmentStatsDto data)
            => await SetToCacheAsync(GetAllDepartmentStatsKey(), data);

        public async Task<int?> GetTotalDepartmentsCountAsync()
            => await GetFromCacheAsync<int>(GetTotalDepartmentsKey());

        public async Task SetTotalDepartmentsCountAsync(int count)
            => await SetToCacheAsync(GetTotalDepartmentsKey(), count);

        public async Task<int?> GetTotalDoctorsCountAsync()
            => await GetFromCacheAsync<int>(GetTotalDoctorsKey());

        public async Task SetTotalDoctorsCountAsync(int count)
            => await SetToCacheAsync(GetTotalDoctorsKey(), count);

        public async Task<int?> GetTotalNursesCountAsync()
            => await GetFromCacheAsync<int>(GetTotalNursesKey());

        public async Task SetTotalNursesCountAsync(int count)
            => await SetToCacheAsync(GetTotalNursesKey(), count);
}