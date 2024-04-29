using Asklepios.Core.Entities.Departments;
using Asklepios.Core.Entities.Users;
using Asklepios.Core.Repositories.Users;
using Asklepios.Infrastructure.DAL.PostgreSQL;
using Microsoft.EntityFrameworkCore;

namespace Asklepios.Infrastructure.DAL.Repositories;

public class NurseRepository : INurseRepository
{
    private readonly AsklepiosDbContext _dbContext;
    private readonly DbSet<Nurse> _nurses;

    public NurseRepository(AsklepiosDbContext dbContext)
    {
        _dbContext = dbContext;
        _nurses = _dbContext.Nurses;
    }
    
    public async Task<Nurse> GetNurseByIdAsync(Guid nurseId)
        => await _nurses
            .Include(x => x.Department)
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.NurseId == nurseId);

    public async Task<IReadOnlyList<Nurse>> GetAllNursesAsync(int pageIndex, int pageSize)
        => await _nurses
            .AsNoTracking()
            .Include(x => x.Department)
            .OrderBy(x => x.Name)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

    public async Task AddNurseAsync(Nurse nurse)
    {
        await _nurses.AddAsync(nurse);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateNurseAsync(Nurse nurse)
    {
        _nurses.Update(nurse);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteNurseAsync(Nurse nurse)
    {
        _nurses.Remove(nurse);
        await _dbContext.SaveChangesAsync();
    }
}