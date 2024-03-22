using Asklepios.Core.Entities.Departments;

namespace Asklepios.Core.Policies.Departments;

public interface IDepartmentDeletePolicy
{
    Task<bool> CannotDeleteDepartmentPolicy(Department department);
}