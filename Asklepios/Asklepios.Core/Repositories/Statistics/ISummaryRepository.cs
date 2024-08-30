using Asklepios.Core.Entities.Views;

namespace Asklepios.Core.Repositories.Statistics;

public interface ISummaryRepository
{
    Task<List<MonthlyDischargeSummary>> GetMonthlyDischargesAsync();
    Task<List<MonthlyAdmissionSummary>> GetMonthlyAdmissionsAsync();
    Task<List<MonthlyDischargeSummary>> GetMonthlyDischargesForYearAsync(int year);
}