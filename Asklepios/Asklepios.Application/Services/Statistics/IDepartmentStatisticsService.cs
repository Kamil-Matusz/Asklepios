using Asklepios.Core.DTO.Statistics;

namespace Asklepios.Application.Services.Statistics;

public interface IDepartmentStatisticsService
{
    Task<DepartmentStatsDto> GetDepartmentStatsAsync(Guid departmentId);
    Task<DepartmentStatsDto> GetAllDepartmentStatsAsync();
    Task<int> GetTotalPatientsCountAsync();
    Task<int> GetTotalDepartmentsCountAsync();
}