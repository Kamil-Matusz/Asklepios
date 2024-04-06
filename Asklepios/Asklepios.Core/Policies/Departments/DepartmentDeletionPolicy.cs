using Asklepios.Core.Entities.Departments;

namespace Asklepios.Core.Policies.Departments;

public class DepartmentDeletionPolicy : IDepartmentDeletePolicy
{
    public async Task<bool> CannotDeleteDepartmentPolicy(Department department)
    {
        if (department.Rooms is null || !department.Rooms.Any())
        {
            return true;
        }

        return true;
    }
}