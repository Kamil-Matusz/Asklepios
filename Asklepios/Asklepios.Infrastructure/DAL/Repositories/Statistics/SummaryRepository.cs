using Asklepios.Core.Entities.Views;
using Asklepios.Core.Repositories.Statistics;
using Asklepios.Infrastructure.DAL.PostgreSQL;
using Microsoft.EntityFrameworkCore;

namespace Asklepios.Infrastructure.DAL.Repositories.Statistics;

public class SummaryRepository : ISummaryRepository
{
    private readonly AsklepiosDbContext _context;

    public SummaryRepository(AsklepiosDbContext context)
    {
        _context = context;
    }

    public async Task<List<MonthlyDischargeSummary>> GetMonthlyDischargesAsync()
    {
        return await _context.Set<MonthlyDischargeSummary>()
            .ToListAsync();
    }
    
    public async Task<List<MonthlyAdmissionSummary>> GetMonthlyAdmissionsAsync()
    {
        return await _context.Set<MonthlyAdmissionSummary>()
            .ToListAsync();
    }

    public async Task<List<MonthlyDischargeSummary>> GetMonthlyDischargesForYearAsync(int year)
    {
        return await _context.Set<MonthlyDischargeSummary>()
            .Where(summary => summary.DischargeMonth.Year == year)
            .ToListAsync();
    }
}