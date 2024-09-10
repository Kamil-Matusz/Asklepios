using Asklepios.Core.DTO.Clinics;

namespace Asklepios.Application.Services.Clinics;

public interface IClinicPatientService
{
    Task<ClinicPatientDto> GetClinicPatientByIdAsync(Guid clinicPatientId);
    Task<IReadOnlyList<ClinicPatientDto>> GetAllClinicPatientsAsync(int pageIndex, int pageSize);
    Task AddClinicPatientAsync(ClinicPatientDto dto);
    Task UpdateClinicPatientAsync(ClinicPatientDto dto);
    Task DeleteClinicPatientAsync(Guid clinicPatientId);
}