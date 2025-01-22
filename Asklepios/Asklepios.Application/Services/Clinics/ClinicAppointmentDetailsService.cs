using Asklepios.Core.DTO.Clinics;
using Asklepios.Core.Entities.Clinics;
using Asklepios.Core.Exceptions.Clinics;
using Asklepios.Core.Repositories.Clinics;

namespace Asklepios.Application.Services.Clinics;

public class ClinicAppointmentDetailsService : IClinicAppointmentDetailsService
{
    private readonly IClinicAppointmentDetailsRepository _clinicAppointmentDetailsRepository;

    public ClinicAppointmentDetailsService(IClinicAppointmentDetailsRepository clinicAppointmentDetailsRepository)
    {
        _clinicAppointmentDetailsRepository = clinicAppointmentDetailsRepository;
    }

    public async Task DeleteClinicAppointmentDetailsByAppointmentIdAsync(Guid appointmentId)
    {
        var appointmentDetails = await _clinicAppointmentDetailsRepository.GetAppointmentDetailsByAppointmentIdAsync(appointmentId);
        if (appointmentDetails is null)
        {
            throw new ClinicAppointmentNotFoundException(appointmentId);
        }

        await _clinicAppointmentDetailsRepository.DeleteAppointmentDetailsAsync(appointmentDetails);
    }

    public async Task AddClinicAppointmentDetailsAsync(ClinicAppointmentDetailsDto dto)
    {
        dto.AppointmentDetailsId = Guid.NewGuid();
        var clinicAppointmentDetails = new ClinicAppointmentDetails
        {
            AppointmentDetailsId = dto.AppointmentDetailsId,
            Description = dto.Description,
            Recommendations = dto.Recommendations,
            Prescriptions = dto.Prescriptions,
            DoctorComments = dto.DoctorComments,
            AppointmentId = dto.AppointmentId
        };

        await _clinicAppointmentDetailsRepository.AddAppointmentAsync(clinicAppointmentDetails);
    }

    public async Task UpdateClinicPatientAsync(ClinicAppointmentDetailsDto dto)
    {
        var appointmentDetails = await _clinicAppointmentDetailsRepository.GetAppointmentDetailsByAppointmentIdAsync(dto.AppointmentId);
        if (appointmentDetails is null)
        {
            throw new ClinicAppointmentNotFoundException(dto.AppointmentId);
        }

        appointmentDetails.Description = dto.Description;
        appointmentDetails.Recommendations = dto.Recommendations;
        appointmentDetails.Prescriptions = dto.Prescriptions;
        appointmentDetails.DoctorComments = dto.DoctorComments;

        await _clinicAppointmentDetailsRepository.UpdateAppointmentAsync(appointmentDetails);
    }

    public async Task<ClinicAppointmentDetailsDto> GetClinicAppointmentDetailsByAppointmentIdAsync(Guid appointmentId)
    {
        var appointmentDetails = await _clinicAppointmentDetailsRepository.GetAppointmentDetailsByAppointmentIdAsync(appointmentId);
        if (appointmentDetails is null)
        {
            throw new ClinicAppointmentNotFoundException(appointmentId);
        }
        
        return Map<ClinicAppointmentDetailsDto>(appointmentDetails);
    }
    
    private static T Map<T>(ClinicAppointmentDetails appointmentDetails) where T : ClinicAppointmentDetailsDto, new() => new T()
    {
        AppointmentDetailsId = appointmentDetails.AppointmentDetailsId,
        Description = appointmentDetails.Description,
        Recommendations = appointmentDetails.Recommendations,
        Prescriptions = appointmentDetails.Prescriptions,
        DoctorComments = appointmentDetails.DoctorComments,
        AppointmentId = appointmentDetails.AppointmentId
    };
}