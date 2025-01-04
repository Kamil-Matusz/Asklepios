using Asklepios.Core.Entities.Clinics;

namespace Asklepios.Core.Repositories.Clinics;

public interface IClinicAppointmentDetailsRepository
{
    Task<ClinicAppointmentDetails> GetAppointmentDetailsByAppointmentIdAsync(Guid appointmentId);
    Task AddAppointmentAsync(ClinicAppointmentDetails clinicAppointmentDetails);
    Task UpdateAppointmentAsync(ClinicAppointmentDetails clinicAppointmentDetails);
    Task DeleteAppointmentDetailsAsync(ClinicAppointmentDetails clinicAppointmentDetails);
}