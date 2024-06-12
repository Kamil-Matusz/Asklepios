using Asklepios.Core.DTO.Users;

namespace Asklepios.Application.Services.Users;

public interface IMedicalStaffService
{
    Task AddDoctorAsync(MedicalStaffDto dto);
    Task<MedicalStaffDto> GetDoctorAsync(Guid id);
    Task<Guid> GetDoctorIdAsync(Guid id);
    Task<IReadOnlyList<MedicalStaffListDto>> GetAllDoctorsAsync(int pageIndex, int pageSize);
    Task UpdateDoctorAsync(MedicalStaffDto dto);
    Task DeleteDoctorAsync(Guid id);
}