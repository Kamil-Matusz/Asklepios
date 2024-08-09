using Asklepios.Core.DTO.Statistics;
using Asklepios.Core.Repositories.Statistics;

namespace Asklepios.Application.Services.Statistics;

public class DepartmentStatisticsService : IDepartmentStatisticsService
{
    private readonly IDepartmentStatisticsRepository _departmentStatisticsRepository;

    public DepartmentStatisticsService(IDepartmentStatisticsRepository departmentStatisticsRepository)
    {
        _departmentStatisticsRepository = departmentStatisticsRepository;
    }

    public async Task<DepartmentStatsDto> GetDepartmentStatsAsync(Guid departmentId)
    {
        var departmentStats = await _departmentStatisticsRepository.GetDepartmentStatsAsync(departmentId);
        if (departmentStats == null || !departmentStats.Any())
        {
            throw new Exception("Nie znaleziono oddziału.");
        }
        
        return departmentStats.First();
    }

    public async Task<DepartmentStatsDto> GetAllDepartmentStatsAsync()
    {
        var allDepartmentStats = await _departmentStatisticsRepository.GetAllDepartmentStatsAsync();
        if (allDepartmentStats == null)
        {
            throw new Exception("Brak statystyk.");
        }

        return allDepartmentStats;
    }

    public async Task<int> GetTotalPatientsCountAsync()
    {
        return await _departmentStatisticsRepository.GetTotalPatientsCountAsync();
    }
}