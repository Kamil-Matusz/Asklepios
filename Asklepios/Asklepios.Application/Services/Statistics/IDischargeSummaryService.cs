using Asklepios.Core.Entities.Views;

namespace Asklepios.Application.Services.Statistics;

public interface IDischargeSummaryService
{
    Task<List<MonthlyDischargeSummary>> GetMonthlyDischargesAsync();
    Task<List<MonthlyDischargeSummary>> GetMonthlyDischargesForYearAsync(int year);
}
