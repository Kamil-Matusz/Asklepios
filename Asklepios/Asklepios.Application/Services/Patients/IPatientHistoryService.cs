using Asklepios.Core.DTO.Patients;
using Asklepios.Core.Entities.Patients;

namespace Asklepios.Application.Services.Patients;

public interface IPatientHistoryService
{
    Task AddOrUpdatePatientHistoryAsync(PatientHistoryDto patientHistoryDto);
    Task<PatientHistoryDto> GetPatientHistoryByIdAsync(Guid historyId);
    Task<PatientHistoryDto> GetFullPatientHistoryByPeselAsync(string peselNumber);
    Task DeletePatientHistoryAsync(Guid historyId);
    Task<bool> PatientExistAsync(string peselNumber);
}