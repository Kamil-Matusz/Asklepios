using Asklepios.Core.DTO.Clinics;
using Asklepios.Core.Entities.Clinics;
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
}