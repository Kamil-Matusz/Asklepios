using Asklepios.Core.Entities.Views;
using Asklepios.Core.Repositories.Statistics;

namespace Asklepios.Application.Services.Statistics;

public class DischargeSummaryService : IDischargeSummaryService
{
    private readonly IDischargeSummaryRepository _dischargeSummaryRepository;

    public DischargeSummaryService(IDischargeSummaryRepository dischargeSummaryRepository)
    {
        _dischargeSummaryRepository = dischargeSummaryRepository;
    }

    public async Task<List<MonthlyDischargeSummary>> GetMonthlyDischargesAsync()
    {
        return await _dischargeSummaryRepository.GetMonthlyDischargesAsync();
    }

    public async Task<List<MonthlyDischargeSummary>> GetMonthlyDischargesForYearAsync(int year)
    {
        return await _dischargeSummaryRepository.GetMonthlyDischargesForYearAsync(year);
    }
}