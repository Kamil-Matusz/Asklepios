using Asklepios.Core.Repositories.Statistics;
using Asklepios.Core.Views;

namespace Asklepios.Application.Services.Statistics;

public class SummaryService : ISummaryService
{
    private readonly ISummaryRepository _summaryRepository;

    public SummaryService(ISummaryRepository summaryRepository)
    {
        _summaryRepository = summaryRepository;
    }

    public async Task<List<MonthlyDischargeSummary>> GetMonthlyDischargesAsync()
    {
        return await _summaryRepository.GetMonthlyDischargesAsync();
    }
    
    public async Task<List<MonthlyAdmissionSummary>> GetMonthlyAdmissionsAsync()
    {
        return await _summaryRepository.GetMonthlyAdmissionsAsync();
    }

    public async Task<List<MonthlyDischargeSummary>> GetMonthlyDischargesForYearAsync(int year)
    {
        return await _summaryRepository.GetMonthlyDischargesForYearAsync(year);
    }
}