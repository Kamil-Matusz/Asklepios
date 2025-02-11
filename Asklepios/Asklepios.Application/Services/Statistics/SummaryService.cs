using Asklepios.Core.Repositories.Statistics;
using Asklepios.Core.Views;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Asklepios.Application.Services.Statistics;

public class SummaryService : ISummaryService
{
    private readonly ISummaryRepository _summaryRepository;
    private readonly IDistributedCache _cache;
    private readonly DistributedCacheEntryOptions _cacheOptions;

    public SummaryService(
        ISummaryRepository summaryRepository,
        IDistributedCache cache)
    {
        _summaryRepository = summaryRepository;
        _cache = cache;
        _cacheOptions = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
        };
    }

    public async Task<List<MonthlyDischargeSummary>> GetMonthlyDischargesAsync()
    {
        var cacheKey = "Asklepios_MonthlyDischarges";
        var cachedData = await _cache.GetStringAsync(cacheKey);

        if (cachedData != null)
        {
            return JsonSerializer.Deserialize<List<MonthlyDischargeSummary>>(cachedData);
        }

        var data = await _summaryRepository.GetMonthlyDischargesAsync();
        await _cache.SetStringAsync(
            cacheKey,
            JsonSerializer.Serialize(data),
            _cacheOptions);

        return data;
    }
    
    public async Task<List<MonthlyAdmissionSummary>> GetMonthlyAdmissionsAsync()
    {
        var cacheKey = "Asklepios_MonthlyAdmissions";
        var cachedData = await _cache.GetStringAsync(cacheKey);

        if (cachedData != null)
        {
            return JsonSerializer.Deserialize<List<MonthlyAdmissionSummary>>(cachedData);
        }

        var data = await _summaryRepository.GetMonthlyAdmissionsAsync();
        await _cache.SetStringAsync(
            cacheKey,
            JsonSerializer.Serialize(data),
            _cacheOptions);

        return data;
    }

    public async Task<List<MonthlyDischargeSummary>> GetMonthlyDischargesForYearAsync(int year)
    {
        var cacheKey = $"Asklepios_MonthlyDischarges_{year}";
        var cachedData = await _cache.GetStringAsync(cacheKey);

        if (cachedData != null)
        {
            return JsonSerializer.Deserialize<List<MonthlyDischargeSummary>>(cachedData);
        }

        var data = await _summaryRepository.GetMonthlyDischargesForYearAsync(year);
        await _cache.SetStringAsync(
            cacheKey,
            JsonSerializer.Serialize(data),
            _cacheOptions);

        return data;
    }
}