using Asklepios.Core.Entities.Examinations;
using Asklepios.Core.Repositories.Examinations;
using Asklepios.Infrastructure.DAL.PostgreSQL;
using Microsoft.EntityFrameworkCore;

namespace Asklepios.Infrastructure.DAL.Repositories.Examinations;

public class OperationRepository : IOperationRepository
{
    private readonly AsklepiosDbContext _dbContext;
    private readonly DbSet<Operation> _operations;

    public OperationRepository(AsklepiosDbContext dbContext)
    {
        _dbContext = dbContext;
        _operations = _dbContext.Operations;
    }
    
    public async Task<Operation> GetOperationByIdAsync(Guid operationId)
        => await _operations
            .Include(x => x.Patient)
            .Include(x => x.MedicalStaff)
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.OperationId == operationId);

    public async Task<IReadOnlyList<Operation>> GetAllOperationsAsync(int pageIndex, int pageSize)
        => await _operations
            .AsNoTracking()
            .Include(x => x.Patient)
            .Include(x => x.MedicalStaff)
            .OrderBy(x => x.OperationName)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

    public async Task AddOperationAsync(Operation operation)
    {
        await _operations.AddAsync(operation);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateOperationAsync(Operation operation)
    {
        _operations.Update(operation);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteOperationAsync(Operation operation)
    {
        _operations.Remove(operation);
        await _dbContext.SaveChangesAsync();
    }
}