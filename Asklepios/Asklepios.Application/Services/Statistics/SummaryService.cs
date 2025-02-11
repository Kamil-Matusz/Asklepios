using Asklepios.Core.Repositories.Statistics;
using Asklepios.Core.Views;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Asklepios.Application.Services.Statistics;

public class SummaryService : ISummaryService
{
    private readonly ISummaryRepository _summaryRepository;
    private readonly IRedisSummaryRepository _redisSummaryRepository;
    

    public SummaryService(ISummaryRepository summaryRepository, IRedisSummaryRepository redisSummaryRepository)
    {
        _summaryRepository = summaryRepository;
        _redisSummaryRepository = redisSummaryRepository;
    }

    public async Task<List<MonthlyDischargeSummary>> GetMonthlyDischargesAsync()
    {
        var cachedDischarges = await _redisSummaryRepository.GetMonthlyDischargesAsync();
        if (cachedDischarges != null)
            return cachedDischarges;

        var discharges = await _summaryRepository.GetMonthlyDischargesAsync();
        await _redisSummaryRepository.SetMonthlyDischargesAsync(discharges);
        return discharges;
    }
    
    public async Task<List<MonthlyAdmissionSummary>> GetMonthlyAdmissionsAsync()
    {
        var cachedAddmissions = await _redisSummaryRepository.GetMonthlyAdmissionsAsync();
        if (cachedAddmissions != null)
            return cachedAddmissions;

        var admissions = await _summaryRepository.GetMonthlyAdmissionsAsync();
        await _redisSummaryRepository.SetMonthlyAdmissionsAsync(admissions);
        return admissions;
    }

    public async Task<List<MonthlyDischargeSummary>> GetMonthlyDischargesForYearAsync(int year)
    {
        var cachedDischarges = await _redisSummaryRepository.GetMonthlyDischargesForYearAsync(year);
        if (cachedDischarges != null)
            return cachedDischarges;

        var discharges = await _summaryRepository.GetMonthlyDischargesForYearAsync(year);
        await _redisSummaryRepository.SetMonthlyDischargesForYearAsync(year, discharges);
        return discharges;
    }
}