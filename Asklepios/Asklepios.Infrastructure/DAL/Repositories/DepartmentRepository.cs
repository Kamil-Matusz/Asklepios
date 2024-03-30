using Asklepios.Core.Entities.Departments;
using Asklepios.Infrastructure.DAL.PostgreSQL;
using Asklepios.Infrastructure.Repositories.Departments;
using Microsoft.EntityFrameworkCore;

namespace Asklepios.Infrastructure.DAL.Repositories;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly AsklepiosDbContext _dbContext;
    private readonly DbSet<Department> _departments;

    public DepartmentRepository(AsklepiosDbContext dbContext)
    {
        _dbContext = dbContext;
        _departments = _dbContext.Departments;
    }

    public async Task<Department> GetDepartmentByIdAsync(Guid departmentId)
        => await _departments
            .Include(x => x.Rooms)
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.DepartmentId == departmentId);

    public async Task<IReadOnlyList<Department>> GetAllDepartmentsAsync()
        => await _departments
            .AsNoTracking()
            .ToListAsync();

    public async Task AddDepartmentAsync(Department department)
    {
        await _departments.AddAsync(department);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateDepartmentAsync(Department department)
    {
        _departments.Update(department);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteDepartmentAsync(Department department)
    {
        _departments.Remove(department);
        await _dbContext.SaveChangesAsync();
    }
}