using Asklepios.Core.Entities.Patients;

namespace Asklepios.Core.Repositories.Patients;

public interface IPatientHistoryRepository
{
    Task<PatientHistory> GetPatientHistoryByIdAsync(Guid historyId);
    Task<PatientHistory> GetFullPatientHistoryByPeselAsync(string peselNumber);
    Task AddPatientHistoryAsync(PatientHistory patientHistory);
    Task UpdatePatientHistoryAsync(PatientHistory patientHistory);
    Task DeletePatientHistoryAsync(PatientHistory patientHistory);
    Task<bool> PatientExistAsync(string peselNumber);
}