using Asklepios.Core.Entities.Patients;

namespace Asklepios.Core.Repositories.Patients;

public interface IDischargeRepository
{
    Task<Discharge> GetDischargeByIdAsync(Guid dischargeId);
    Task AddDischargeAsync(Discharge discharge);
    Task UpdateDischargeAsync(Discharge discharge);
    Task DeleteDischargeAsync(Discharge discharge);
}