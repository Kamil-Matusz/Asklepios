using Asklepios.Core.Entities.Examinations;
using Asklepios.Core.Repositories.Examinations;
using Asklepios.Infrastructure.DAL.PostgreSQL;
using Microsoft.EntityFrameworkCore;

namespace Asklepios.Infrastructure.DAL.Repositories.Examinations;

public class ExaminationRepository : IExaminationRepository
{
    private readonly AsklepiosDbContext _dbContext;
    private readonly DbSet<Examination> _examinations;

    public ExaminationRepository(AsklepiosDbContext dbContext)
    {
        _dbContext = dbContext;
        _examinations = _dbContext.Examinations;
    }
    
    public async Task<Examination> GetExaminationByIdAsync(int examinationId)
        => await _examinations
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.ExaminationId == examinationId);

    public async Task<IReadOnlyList<Examination>> GetAllExaminationsAsync(int pageIndex, int pageSize)
        => await _examinations
            .AsNoTracking()
            .OrderBy(x => x.ExamName)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

    public async Task<IReadOnlyList<Examination>> GetAllExaminationsByCategoryAsync(string category, int pageIndex, int pageSize)
        => await _examinations
            .AsNoTracking()
            .OrderBy(x => x.ExamCategory)
            .Where(x => x.ExamCategory == category)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

    public async Task AddExaminationAsync(Examination examination)
    {
        await _examinations.AddAsync(examination);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateExaminationAsync(Examination examination)
    {
        _examinations.Update(examination);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteExaminationAsync(Examination examination)
    {
        _examinations.Remove(examination);
        await _dbContext.SaveChangesAsync();
    }
}