using Asklepios.Core.Repositories.Statistics;
using Asklepios.Core.Views;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Asklepios.Application.Services.Statistics;

public class SummaryService : ISummaryService
{
    private readonly ISummaryRepository _summaryRepository;
    private readonly ISummaryCacheRepository _summaryCacheRepository;
    

    public SummaryService(ISummaryRepository summaryRepository, ISummaryCacheRepository summaryCacheRepository)
    {
        _summaryRepository = summaryRepository;
        _summaryCacheRepository = summaryCacheRepository;
    }

    public async Task<List<MonthlyDischargeSummary>> GetMonthlyDischargesAsync()
    {
        var cachedDischarges = await _summaryCacheRepository.GetMonthlyDischargesAsync();
        if (cachedDischarges != null)
            return cachedDischarges;

        var discharges = await _summaryRepository.GetMonthlyDischargesAsync();
        await _summaryCacheRepository.SetMonthlyDischargesAsync(discharges);
        return discharges;
    }
    
    public async Task<List<MonthlyAdmissionSummary>> GetMonthlyAdmissionsAsync()
    {
        var cachedAddmissions = await _summaryCacheRepository.GetMonthlyAdmissionsAsync();
        if (cachedAddmissions != null)
            return cachedAddmissions;

        var admissions = await _summaryRepository.GetMonthlyAdmissionsAsync();
        await _summaryCacheRepository.SetMonthlyAdmissionsAsync(admissions);
        return admissions;
    }

    public async Task<List<MonthlyDischargeSummary>> GetMonthlyDischargesForYearAsync(int year)
    {
        var cachedDischarges = await _summaryCacheRepository.GetMonthlyDischargesForYearAsync(year);
        if (cachedDischarges != null)
            return cachedDischarges;

        var discharges = await _summaryRepository.GetMonthlyDischargesForYearAsync(year);
        await _summaryCacheRepository.SetMonthlyDischargesForYearAsync(year, discharges);
        return discharges;
    }
}