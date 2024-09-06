using Asklepios.Core.Entities.Patients;

namespace Asklepios.Core.Repositories.Patients;

public interface IPatientRepository
{
    Task<Patient> GetPatientByIdAsync(Guid patientId);
    Task<Patient> GetPatientDataByIdAsync(Guid patientId);
    Task<IReadOnlyList<Patient>> GetAllPatientsAsync(int pageIndex, int pageSize);
    Task<IReadOnlyList<Patient>> GetAllPatientsByDepartmentAsync(Guid departmentId, int pageIndex, int pageSize);
    Task<IReadOnlyList<Patient>> GetAllPatientsByDoctorAsync(Guid doctorId, int pageIndex, int pageSize);
    Task<IReadOnlyList<Patient>> GetAllPatientsByRoomAsync(Guid roomId, int pageIndex, int pageSize);
    Task AddPatientAsync(Patient patient);
    Task UpdatePatientAsync(Patient patient);
    Task DeletePatientAsync(Patient patient);
    Task<List<Patient>> GetPatientsList();
}