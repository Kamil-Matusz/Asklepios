using Asklepios.Core.Entities.Users;

namespace Asklepios.Core.Repositories.Users;

public interface IMedicalStaffRepository
{
    Task<MedicalStaff> GetDoctorByIdAsync(Guid doctorId);
    Task<Guid> GetDoctorIdAsync(Guid userId);
    Task<IReadOnlyList<MedicalStaff>> GetAllNDoctorsAsync(int pageIndex, int pageSize);
    Task AddDoctorAsync(MedicalStaff medicalStaff);
    Task UpdateDoctorAsync(MedicalStaff medicalStaff);
    Task DeleteDoctorAsync(MedicalStaff medicalStaff);
    Task<List<MedicalStaff>> GetDoctorsList();
}