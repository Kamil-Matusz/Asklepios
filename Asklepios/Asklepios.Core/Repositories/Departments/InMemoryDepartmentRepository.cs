using Asklepios.Core.Entities.Departments;

namespace Asklepios.Core.Repositories.Departments;

public class InMemoryDepartmentRepository : IDepartmentRepository
{
    private readonly List<Department> _departments = new();

    public Task<Department> GetDepartmentByIdAsync(Guid departmentId)
        => Task.FromResult(_departments.SingleOrDefault(x => x.DepartmentId == departmentId));

    public async Task<IReadOnlyList<Department>> GetAllDepartmentsAsync(int pageIndex, int pageSize)
    {
        await Task.CompletedTask;
        return _departments;
    }

    public Task AddDepartmentAsync(Department department)
    {
        _departments.Add(department);
        return Task.CompletedTask;
    }

    public Task UpdateDepartmentAsync(Department department)
    {
        return Task.CompletedTask;
    }

    public Task DeleteDepartmentAsync(Department department)
    {
        _departments.Remove(department);
        return Task.CompletedTask;
    }
}