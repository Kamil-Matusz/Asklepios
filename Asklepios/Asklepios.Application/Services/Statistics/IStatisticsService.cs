using Asklepios.Core.DTO.Statistics;

namespace Asklepios.Application.Services.Statistics;

public interface IStatisticsService
{
    Task<DepartmentStatsDto> GetDepartmentStatsAsync(Guid departmentId);
    Task<DepartmentStatsDto> GetAllDepartmentStatsAsync();
    Task<int> GetTotalPatientsCountAsync();
    Task<int> GetTotalDepartmentsCountAsync();
    Task<int> GetTotalDoctorsCountAsync();
    Task<int> GetTotalNursesCountAsync();
}