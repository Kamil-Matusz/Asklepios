using Asklepios.Core.DTO.Clinics;

namespace Asklepios.Application.Services.Clinics;

public interface IClinicAppointmentService
{
    Task RegisterPatientAndCreateAppointmentAsync(ClinicAppointmentRequestDto dto);
}