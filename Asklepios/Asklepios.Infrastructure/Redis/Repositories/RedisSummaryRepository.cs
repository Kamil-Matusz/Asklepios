using System.Text.Json;
using Asklepios.Core.Repositories.Statistics;
using Asklepios.Core.Views;
using Microsoft.Extensions.Caching.Distributed;

namespace Asklepios.Infrastructure.Redis.Repositories;

public class RedisSummaryRepository : IRedisSummaryRepository
{
    private readonly IDistributedCache _cache;
    private readonly TimeSpan _cacheDuration = TimeSpan.FromHours(5);

    public RedisSummaryRepository(IDistributedCache cache)
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

    private string GetMonthlyDischargesKey() => "Asklepios_MonthlyDischarges";
    private string GetMonthlyAdmissionsKey() => "Asklepios_MonthlyAdmissions";
    private string GetMonthlyDischargesForYearKey(int year) => $"Asklepios_MonthlyDischarges_{year}";

    public async Task<List<MonthlyDischargeSummary>?> GetMonthlyDischargesAsync()
        => await GetFromCacheAsync<List<MonthlyDischargeSummary>>(GetMonthlyDischargesKey());

    public async Task<List<MonthlyAdmissionSummary>?> GetMonthlyAdmissionsAsync()
        => await GetFromCacheAsync<List<MonthlyAdmissionSummary>>(GetMonthlyAdmissionsKey());

    public async Task<List<MonthlyDischargeSummary>?> GetMonthlyDischargesForYearAsync(int year)
        => await GetFromCacheAsync<List<MonthlyDischargeSummary>>(GetMonthlyDischargesForYearKey(year));

    public async Task SetMonthlyDischargesAsync(List<MonthlyDischargeSummary> data)
        => await SetToCacheAsync(GetMonthlyDischargesKey(), data);

    public async Task SetMonthlyAdmissionsAsync(List<MonthlyAdmissionSummary> data)
        => await SetToCacheAsync(GetMonthlyAdmissionsKey(), data);

    public async Task SetMonthlyDischargesForYearAsync(int year, List<MonthlyDischargeSummary> data)
        => await SetToCacheAsync(GetMonthlyDischargesForYearKey(year), data);
}