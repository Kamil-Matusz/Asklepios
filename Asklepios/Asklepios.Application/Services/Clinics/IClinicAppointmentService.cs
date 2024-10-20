using Asklepios.Core.DTO.Clinics;

namespace Asklepios.Application.Services.Clinics;

public interface IClinicAppointmentService
{
    Task RegisterPatientAndCreateAppointmentAsync(ClinicAppointmentRequestDto dto);
    Task DeleteClinicAppointmentAsync(Guid appointmentId);
    Task<ClinicAppointmentListDto> GetClinicAppointmentByIdAsync(Guid appointmentId);
    Task<IReadOnlyList<ClinicAppointmentListDto>> GetClinicAppointmentsByDateAsync(DateTime date);
    Task UpdateClinicAppointmentAsync(ClinicAppointmentStatusDto dto);
    Task RegisterPatientAndCreateAppointmentBuUserAsync(ClinicAppointmentRequestByUserDto dto);
}