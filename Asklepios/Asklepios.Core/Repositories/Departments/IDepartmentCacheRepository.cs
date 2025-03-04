using Asklepios.Core.DTO.Departments;

namespace Asklepios.Core.Repositories.Departments;

public interface IDepartmentCacheRepository
{
    Task<IReadOnlyList<DepartmentListDto>?> GetDepartmentsAsync(int pageIndex, int pageSize);
    Task SetDepartmentsAsync(int pageIndex, int pageSize, IReadOnlyList<DepartmentListDto> departments);
}