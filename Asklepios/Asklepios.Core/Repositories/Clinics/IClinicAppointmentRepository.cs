using Asklepios.Core.Entities.Clinics;

namespace Asklepios.Core.Repositories.Clinics;

public interface IClinicAppointmentRepository
{
    Task<ClinicAppointment> GetAppointmentByIdAsync(Guid appointmentId);
    Task<IReadOnlyList<ClinicAppointment>> GetAppointmentsByPatientIdAsync(Guid clinicPatientId, int pageIndex, int pageSize);
    Task<IReadOnlyList<ClinicAppointment>> GetAppointmentsByMedicalStaffIdAsync(Guid medicalStaffId, int pageIndex, int pageSize);
    Task<IReadOnlyList<ClinicAppointment>> GetAppointmentsByDateAsync(DateTime date);
    Task AddAppointmentAsync(ClinicAppointment appointment);
    Task UpdateAppointmentAsync(ClinicAppointment appointment);
    Task DeleteAppointmentAsync(ClinicAppointment appointment);
    Task<IReadOnlyList<ClinicAppointment>> GetUserPastAppointmentsAsync(Guid clinicPatientId);
    Task<IReadOnlyList<ClinicAppointment>> GetUserFutureAppointmentsAsync(Guid clinicPatientId);
}