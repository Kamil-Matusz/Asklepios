using Asklepios.Core.DTO.Clinics;
using Asklepios.Core.DTO.Users;

namespace Asklepios.Core.Repositories.Users;

public interface IMedicalStaffCacheRepository
{
    Task<IReadOnlyList<ClinicDoctorListDto>?> GetClinicDoctorsAsync();
    Task SetClinicDoctorsAsync(IReadOnlyList<ClinicDoctorListDto> clinicDoctors);
    Task<IReadOnlyList<MedicalStaffListDto>?> GetMedicalStaffAsync(int pageIndex, int pageSize);
    Task SetMedicalStaffAsync(int pageIndex, int pageSize, IReadOnlyList<MedicalStaffListDto> medicalStaffList);
}