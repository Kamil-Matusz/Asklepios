using Asklepios.Core.Views;

namespace Asklepios.Core.Repositories.Statistics;

public interface ISummaryCacheRepository
{
    Task<List<MonthlyDischargeSummary>?> GetMonthlyDischargesAsync();
    Task<List<MonthlyAdmissionSummary>?> GetMonthlyAdmissionsAsync();
    Task<List<MonthlyDischargeSummary>?> GetMonthlyDischargesForYearAsync(int year);
    Task SetMonthlyDischargesAsync(List<MonthlyDischargeSummary> data);
    Task SetMonthlyAdmissionsAsync(List<MonthlyAdmissionSummary> data);
    Task SetMonthlyDischargesForYearAsync(int year, List<MonthlyDischargeSummary> data);
}