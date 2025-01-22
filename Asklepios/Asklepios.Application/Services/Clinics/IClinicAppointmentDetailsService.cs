using Asklepios.Core.DTO.Clinics;

namespace Asklepios.Application.Services.Clinics;

public interface IClinicAppointmentDetailsService
{
    Task DeleteClinicAppointmentDetailsByAppointmentIdAsync(Guid appointmentId);
    Task AddClinicAppointmentDetailsAsync(ClinicAppointmentDetailsDto dto);
    Task UpdateClinicPatientAsync(ClinicAppointmentDetailsDto dto);
    Task<ClinicAppointmentDetailsDto> GetClinicAppointmentDetailsByAppointmentIdAsync(Guid appointmentId);
}