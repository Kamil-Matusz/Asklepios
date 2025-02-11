using Asklepios.Core.DTO.Statistics;
using Asklepios.Core.Exceptions.Statistics;
using Asklepios.Core.Repositories.Statistics;

namespace Asklepios.Application.Services.Statistics;

public class StatisticsService : IStatisticsService
{
    private readonly IStatisticsRepository _statisticsRepository;
    private readonly IStatisticsCacheRepository _statisticsCacheRepository;

    public StatisticsService(IStatisticsRepository statisticsRepository, IStatisticsCacheRepository statisticsCacheRepository)
    {
        _statisticsRepository = statisticsRepository;
        _statisticsCacheRepository = statisticsCacheRepository;
    }

    public async Task<DepartmentStatsDto> GetDepartmentStatsAsync(Guid departmentId)
    {
        var cachedData = await _statisticsCacheRepository.GetDepartmentStatsAsync(departmentId);
        if (cachedData != null)
            return cachedData;

        var departmentStats = await _statisticsRepository.GetDepartmentStatsAsync(departmentId);
        if (departmentStats == null || !departmentStats.Any())
        {
            throw new StatsException("Not found any departments.");
        }

        var result = departmentStats.First();
        await _statisticsCacheRepository.SetDepartmentStatsAsync(departmentId, result);
        return result;
    }

    public async Task<DepartmentStatsDto> GetAllDepartmentStatsAsync()
    {
        var cachedData = await _statisticsCacheRepository.GetAllDepartmentStatsAsync();
        if (cachedData != null)
            return cachedData;

        var allDepartmentStats = await _statisticsRepository.GetAllDepartmentStatsAsync();
        if (allDepartmentStats == null)
        {
            throw new StatsException("No statistics.");
        }

        await _statisticsCacheRepository.SetAllDepartmentStatsAsync(allDepartmentStats);
        return allDepartmentStats;
    }

    public async Task<int> GetTotalPatientsCountAsync()
        => await _statisticsRepository.GetTotalPatientsCountAsync();

    public async Task<int> GetTotalDepartmentsCountAsync()
    {
        var cachedData = await _statisticsCacheRepository.GetTotalDepartmentsCountAsync();
        if (cachedData.HasValue)
            return cachedData.Value;

        var count = await _statisticsRepository.GetTotalDepartmentsCountAsync();
        await _statisticsCacheRepository.SetTotalDepartmentsCountAsync(count);
        return count;
    }

    public async Task<int> GetTotalDoctorsCountAsync()
    {
        var cachedData = await _statisticsCacheRepository.GetTotalDoctorsCountAsync();
        if (cachedData.HasValue)
            return cachedData.Value;

        var count = await _statisticsRepository.GetTotalDoctorsCountAsync();
        await _statisticsCacheRepository.SetTotalDoctorsCountAsync(count);
        return count;
    }

    public async Task<int> GetTotalNursesCountAsync()
    {
        var cachedData = await _statisticsCacheRepository.GetTotalNursesCountAsync();
        if (cachedData.HasValue)
            return cachedData.Value;

        var count = await _statisticsRepository.GetTotalNursesCountAsync();
        await _statisticsCacheRepository.SetTotalNursesCountAsync(count);
        return count;
    }
}