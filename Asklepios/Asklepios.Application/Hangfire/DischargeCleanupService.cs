using Asklepios.Core.Repositories.Patients;

namespace Asklepios.Application.Hangfire;

public class DischargeCleanupService : IDischargeCleanupService
{
    private readonly IDischargeRepository _dischargeRepository;

    public DischargeCleanupService(IDischargeRepository dischargeRepository)
    {
        _dischargeRepository = dischargeRepository;
    }

    public async Task RemoveOldDischarges()
    {
        var dateThreshold = DateOnly.FromDateTime(DateTime.UtcNow.AddMonths(-6));
        
        var oldDischarges = await _dischargeRepository.GetDischargesOlderThanAsync(dateThreshold);
        
        foreach (var discharge in oldDischarges)
        {
            await _dischargeRepository.DeleteDischargeByIdAsync(discharge.DischargeId);
        }
    }
}
