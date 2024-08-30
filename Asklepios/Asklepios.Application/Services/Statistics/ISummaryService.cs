using Asklepios.Core.Entities.Views;

namespace Asklepios.Application.Services.Statistics;

public interface ISummaryService
{
    Task<List<MonthlyDischargeSummary>> GetMonthlyDischargesAsync();
    Task<List<MonthlyAdmissionSummary>> GetMonthlyAdmissionsAsync();
    Task<List<MonthlyDischargeSummary>> GetMonthlyDischargesForYearAsync(int year);
}
