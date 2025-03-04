using Asklepios.Core.DTO.Statistics;

namespace Asklepios.Core.Repositories.Statistics;

public interface IStatisticsCacheRepository
{
    Task<DepartmentStatsDto?> GetDepartmentStatsAsync(Guid departmentId);
    Task SetDepartmentStatsAsync(Guid departmentId, DepartmentStatsDto data);

    Task<DepartmentStatsDto?> GetAllDepartmentStatsAsync();
    Task SetAllDepartmentStatsAsync(DepartmentStatsDto data);
    
    Task<int?> GetTotalDepartmentsCountAsync();
    Task SetTotalDepartmentsCountAsync(int count);

    Task<int?> GetTotalDoctorsCountAsync();
    Task SetTotalDoctorsCountAsync(int count);

    Task<int?> GetTotalNursesCountAsync();
    Task SetTotalNursesCountAsync(int count);
}