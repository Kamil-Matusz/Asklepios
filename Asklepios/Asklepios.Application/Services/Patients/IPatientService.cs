using Asklepios.Core.DTO.Patients;

namespace Asklepios.Application.Services.Patients;

public interface IPatientService
{
    Task AddPatientAsync(PatientDto dto);
    Task<PatientDetailsDto> GetPatientAsync(Guid id);
    Task<IReadOnlyList<PatientListDto>> GetAllPatientsAsync(int pageIndex, int pageSize);
    Task UpdatePatientAsync(PatientDto dto);
    Task DeletePatientAsync(Guid id);
}