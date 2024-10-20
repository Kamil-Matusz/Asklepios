using Asklepios.Core.Entities.Users;

namespace Asklepios.Core.Repositories.Users;

public interface IMedicalStaffRepository
{
    Task<MedicalStaff> GetDoctorByIdAsync(Guid doctorId);
    Task<Guid> GetDoctorIdAsync(Guid userId);
    Task<Guid> GetUserIdByDoctor(Guid doctorId);
    Task<IReadOnlyList<MedicalStaff>> GetAllDoctorsAsync(int pageIndex, int pageSize);
    Task AddDoctorAsync(MedicalStaff medicalStaff);
    Task UpdateDoctorAsync(MedicalStaff medicalStaff);
    Task DeleteDoctorAsync(MedicalStaff medicalStaff);
    Task<List<MedicalStaff>> GetDoctorsList();
    Task<IReadOnlyList<MedicalStaff>> GetClinicDoctorsAsync();
}