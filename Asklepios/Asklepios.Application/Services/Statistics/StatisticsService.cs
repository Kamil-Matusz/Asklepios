using Asklepios.Core.DTO.Statistics;
using Asklepios.Core.Repositories.Statistics;

namespace Asklepios.Application.Services.Statistics;

public class StatisticsService : IStatisticsService
{
    private readonly IStatisticsRepository _statisticsRepository;

    public StatisticsService(IStatisticsRepository statisticsRepository)
    {
        _statisticsRepository = statisticsRepository;
    }

    public async Task<DepartmentStatsDto> GetDepartmentStatsAsync(Guid departmentId)
    {
        var departmentStats = await _statisticsRepository.GetDepartmentStatsAsync(departmentId);
        if (departmentStats == null || !departmentStats.Any())
        {
            throw new Exception("Nie znaleziono oddziału.");
        }
        
        return departmentStats.First();
    }

    public async Task<DepartmentStatsDto> GetAllDepartmentStatsAsync()
    {
        var allDepartmentStats = await _statisticsRepository.GetAllDepartmentStatsAsync();
        if (allDepartmentStats == null)
        {
            throw new Exception("Brak statystyk.");
        }

        return allDepartmentStats;
    }

    public async Task<int> GetTotalPatientsCountAsync()
    {
        return await _statisticsRepository.GetTotalPatientsCountAsync();
    }

    public async Task<int> GetTotalDepartmentsCountAsync()
    {
        return await _statisticsRepository.GetTotalDepartmentsCountAsync();
    }

    public async Task<int> GetTotalDoctorsCountAsync()
    {
        return await _statisticsRepository.GetTotalDoctorsCountAsync();
    }

    public async Task<int> GetTotalNursesCountAsync()
    {
        return await _statisticsRepository.GetTotalNursesCountAsync();
    }
}