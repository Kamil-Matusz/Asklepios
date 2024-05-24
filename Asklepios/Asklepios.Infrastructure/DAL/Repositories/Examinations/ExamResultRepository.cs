using Asklepios.Core.Entities.Examinations;
using Asklepios.Core.Repositories.Examinations;
using Asklepios.Infrastructure.DAL.PostgreSQL;
using Microsoft.EntityFrameworkCore;

namespace Asklepios.Infrastructure.DAL.Repositories.Examinations;

public class ExamResultRepository : IExamResultRepository
{
    private readonly AsklepiosDbContext _dbContext;
    private readonly DbSet<ExamResult> _examResults;

    public ExamResultRepository(AsklepiosDbContext dbContext)
    {
        _dbContext = dbContext;
        _examResults = _dbContext.ExamResults;
    }
    
    public async Task<ExamResult> GetExamResultByIdAsync(Guid examResultId)
        => await _examResults
            .AsNoTracking()
            .Include(x => x.Examination)
            .SingleOrDefaultAsync(x => x.ExamResultId == examResultId);

    public async Task<IReadOnlyList<ExamResult>> GetAllExamResultsAsync(int pageIndex, int pageSize)
        => await _examResults
            .AsNoTracking()
            .Include(x => x.Examination)
            .OrderBy(x => x.Examination)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

    public async Task AddExamResultAsync(ExamResult examResult)
    {
        await _examResults.AddAsync(examResult);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateExamResultAsync(ExamResult examResult)
    {
        _examResults.Update(examResult);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteExamResultAsync(ExamResult examResult)
    {
        _examResults.Remove(examResult);
        await _dbContext.SaveChangesAsync();
    }
}