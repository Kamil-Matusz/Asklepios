using Asklepios.Core.DTO.Patients;
using Asklepios.Core.Entities.Patients;

namespace Asklepios.Application.Services.Patients;

public interface IPatientService
{
    Task AddPatientAsync(PatientDto dto);
    Task<PatientDto> GetPatientDataAsync(Guid id);
    Task<PatientDetailsDto> GetPatientAsync(Guid id);
    Task<IReadOnlyList<PatientListDto>> GetAllPatientsAsync(int pageIndex, int pageSize);
    Task UpdatePatientAsync(PatientDto dto);
    Task DeletePatientAsync(Guid id);
    Task<List<PatientAutocompleteDto>> GetPatientsList();
}