using Asklepios.Core.Entities.Clinics;

namespace Asklepios.Core.Repositories.Clinics;

public interface IClinicPatientRepository
{
    Task<ClinicPatient> GetClinicPatientByIdAsync(Guid clinicPatientId);
    Task<IReadOnlyList<ClinicPatient>> GetAllClinicPatientsAsync(int pageIndex, int pageSize);
    Task AddClinicPatientAsync(ClinicPatient clinicPatient);
    Task UpdateClinicPatientAsync(ClinicPatient clinicPatient);
    Task DeleteClinicPatientAsync(ClinicPatient clinicPatient);
    Task<ClinicPatient> GetPatientByPeselAsync(string peselNumber);
}