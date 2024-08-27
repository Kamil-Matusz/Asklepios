using Asklepios.Core.Entities.Views;

namespace Asklepios.Core.Repositories.Statistics;

public interface IDischargeSummaryRepository
{
    Task<List<MonthlyDischargeSummary>> GetMonthlyDischargesAsync();
    Task<List<MonthlyDischargeSummary>> GetMonthlyDischargesForYearAsync(int year);
}