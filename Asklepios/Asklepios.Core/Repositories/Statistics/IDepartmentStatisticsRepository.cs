using Asklepios.Core.DTO.Statistics;

namespace Asklepios.Core.Repositories.Statistics;

public interface IDepartmentStatisticsRepository
{
    Task<List<DepartmentStatsDto>> GetDepartmentStatsAsync(Guid departmentId);
    Task<DepartmentStatsDto> GetAllDepartmentStatsAsync();
    Task<int> GetTotalPatientsCountAsync();
    Task<int> GetTotalDepartmentsCountAsync();
}