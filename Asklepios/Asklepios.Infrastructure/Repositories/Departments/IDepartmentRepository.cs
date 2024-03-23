using Asklepios.Core.Entities.Departments;

namespace Asklepios.Infrastructure.Repositories.Departments;

public interface IDepartmentRepository
{
    Task<Department> GetDepartmentByIdAsync(Guid departmentId);
    Task<IReadOnlyList<Department>> GetAllDepartmentsAsync();
    Task AddDepartmentAsync(Department department);
    Task UpdateDepartmentAsync(Department department);
    Task DeleteDepartmentAsync(Department department);
}