using Asklepios.Core.DTO.Clinics;

namespace Asklepios.Core.Repositories.Users;

public interface IMedicalStaffCacheRepository
{
    Task<IReadOnlyList<ClinicDoctorListDto>?> GetClinicDoctorsAsync();
    Task SetClinicDoctorsAsync(IReadOnlyList<ClinicDoctorListDto> clinicDoctors);
}