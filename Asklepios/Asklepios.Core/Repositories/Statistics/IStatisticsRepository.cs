using Asklepios.Core.DTO.Statistics;

namespace Asklepios.Core.Repositories.Statistics;

public interface IStatisticsRepository
{
    Task<List<DepartmentStatsDto>> GetDepartmentStatsAsync(Guid departmentId);
    Task<DepartmentStatsDto> GetAllDepartmentStatsAsync();
    Task<int> GetTotalPatientsCountAsync();
    Task<int> GetTotalDepartmentsCountAsync();
    Task<int> GetTotalDoctorsCountAsync();
    Task<int> GetTotalNursesCountAsync();
}