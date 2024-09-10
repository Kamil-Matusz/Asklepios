using Asklepios.Core.DTO.Clinics;
using Asklepios.Core.Entities.Clinics;
using Asklepios.Core.Exceptions.Clinics;
using Asklepios.Core.Repositories.Clinics;

namespace Asklepios.Application.Services.Clinics;

public class ClinicAppointmentService : IClinicAppointmentService
{
    private readonly IClinicPatientRepository _clinicPatientRepository;
    private readonly IClinicAppointmentRepository _clinicAppointmentRepository;

    public ClinicAppointmentService(IClinicPatientRepository clinicPatientRepository, IClinicAppointmentRepository clinicAppointmentRepository)
    {
        _clinicPatientRepository = clinicPatientRepository;
        _clinicAppointmentRepository = clinicAppointmentRepository;
    }

    public async Task RegisterPatientAndCreateAppointmentAsync(ClinicAppointmentRequestDto dto)
    {
        var existingPatient = await _clinicPatientRepository.GetPatientByPeselAsync(dto.PeselNumber);
    
        if (existingPatient is null)
        {
            var newPatient = new ClinicPatient
            {
                ClinicPatientId = Guid.NewGuid(),
                PatientName = dto.PatientName,
                PatientSurname = dto.PatientSurname,
                PeselNumber = dto.PeselNumber,
                ContactNumber = dto.ContactNumber,
                Email = dto.Email
            };

            await _clinicPatientRepository.AddClinicPatientAsync(newPatient);
            existingPatient = newPatient;
        }

        dto.Status = "Scheduled";
        var newAppointment = new ClinicAppointment
        {
            AppointmentId = Guid.NewGuid(),
            AppointmentDate = dto.AppointmentDate,
            AppointmentType = dto.AppointmentType,
            ClinicPatientId = existingPatient.ClinicPatientId,
            MedicalStaffId = dto.MedicalStaffId,
            Status = dto.Status
        };

        await _clinicAppointmentRepository.AddAppointmentAsync(newAppointment);
    }

    public async Task DeleteClinicAppointmentAsync(Guid appointmentId)
    {
        var clinicAppointment = await _clinicAppointmentRepository.GetAppointmentByIdAsync(appointmentId);
        if (clinicAppointment is null)
        {
            throw new ClinicAppointmentNotFoundException(appointmentId);
        }

        await _clinicAppointmentRepository.DeleteAppointmentAsync(clinicAppointment);
    }

    public async Task<ClinicAppointmentListDto> GetClinicAppointmentByIdAsync(Guid appointmentId)
    {
        var clinicAppointment = await _clinicAppointmentRepository.GetAppointmentByIdAsync(appointmentId);
        if (clinicAppointment is null)
        {
            throw new ClinicAppointmentNotFoundException(appointmentId);
        }
        
        return MapList<ClinicAppointmentListDto>(clinicAppointment);
    }

    public async Task UpdateClinicAppointmentAsync(ClinicAppointmentStatusDto dto)
    {
        var clinicAppointment = await _clinicAppointmentRepository.GetAppointmentByIdAsync(dto.AppointmentId);
        if (clinicAppointment is null)
        {
            throw new ClinicAppointmentNotFoundException(dto.AppointmentId);
        }

        clinicAppointment.Status = dto.Status;
        await _clinicAppointmentRepository.UpdateAppointmentAsync(clinicAppointment);
    }
    
    private static T Map<T>(ClinicAppointment clinicAppointment) where T : ClinicAppointmentDto, new() => new T()
    {
        AppointmentId = clinicAppointment.AppointmentId,
        AppointmentDate = clinicAppointment.AppointmentDate,
        AppointmentType = clinicAppointment.AppointmentType,
        ClinicPatientId = clinicAppointment.ClinicPatientId,
        MedicalStaffId = clinicAppointment.MedicalStaffId,
        Status = clinicAppointment.Status
    };
    
    private static T MapList<T>(ClinicAppointment clinicAppointment) where T : ClinicAppointmentListDto, new() => new T()
    {
        AppointmentId = clinicAppointment.AppointmentId,
        AppointmentDate = clinicAppointment.AppointmentDate,
        AppointmentType = clinicAppointment.AppointmentType,
        PatientName = clinicAppointment.ClinicPatient.PatientName,
        PatientSurname = clinicAppointment.ClinicPatient.PatientSurname,
        PeselNumber = clinicAppointment.ClinicPatient.PeselNumber,
        DoctorName = clinicAppointment.MedicalStaff.Name,
        DoctorSurname = clinicAppointment.MedicalStaff.Surname,
        Status = clinicAppointment.Status
    };
}