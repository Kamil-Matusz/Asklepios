using Asklepios.Core.Entities.Departments;

namespace Asklepios.Core.Repositories.Departments;

public interface IDepartmentRepository
{
    Task<Department> GetDepartmentByIdAsync(Guid departmentId);
    Task<IReadOnlyList<Department>> GetAllDepartmentsAsync(int pageIndex, int pageSize);
    Task AddDepartmentAsync(Department department);
    Task UpdateDepartmentAsync(Department department);
    Task DeleteDepartmentAsync(Department department);
    Task<int> CountPatientsInDepartmentAsync(Guid departmentId);
    Task<int> GetNumberOfBedsAsync(Guid departmentId);
}