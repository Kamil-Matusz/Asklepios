using Asklepios.Core.DTO.Clinics;
using Asklepios.Core.Entities.Clinics;
using Asklepios.Core.Exceptions.Clinics;
using Asklepios.Core.Repositories.Clinics;

namespace Asklepios.Application.Services.Clinics;

public class ClinicPatientService : IClinicPatientService
{
    private readonly IClinicPatientRepository _clinicPatientRepository;

    public ClinicPatientService(IClinicPatientRepository clinicPatientRepository)
    {
        _clinicPatientRepository = clinicPatientRepository;
    }

    public async Task<ClinicPatientDto> GetClinicPatientByIdAsync(Guid id)
    {
        var clinicPatient = await _clinicPatientRepository.GetClinicPatientByIdAsync(id);
        if (clinicPatient is null)
        {
            throw new ClinicPatientNotFoundException(id);
        }

        return Map<ClinicPatientDto>(clinicPatient);
    }

    public async Task<IReadOnlyList<ClinicPatientDto>> GetAllClinicPatientsAsync(int pageIndex, int pageSize)
    {
        var clinicPatients = await _clinicPatientRepository.GetAllClinicPatientsAsync(pageIndex, pageSize);
        return clinicPatients.Select(Map<ClinicPatientDto>).ToList();
    }

    public async Task AddClinicPatientAsync(ClinicPatientDto dto)
    {
        var existingPatient = await _clinicPatientRepository.GetPatientByPeselAsync(dto.PeselNumber);
        if (existingPatient != null)
        {
            throw new ClinicPatientAlreadyExistsException(dto.PeselNumber);
        }

        dto.ClinicPatientId = Guid.NewGuid();
        var clinicPatient = new ClinicPatient
        {
            ClinicPatientId = dto.ClinicPatientId,
            PatientName = dto.PatientName,
            PatientSurname = dto.PatientSurname,
            PeselNumber = dto.PeselNumber,
            ContactNumber = dto.ContactNumber,
            Email = dto.Email
        };

        await _clinicPatientRepository.AddClinicPatientAsync(clinicPatient);
    }

    public async Task UpdateClinicPatientAsync(ClinicPatientDto dto)
    {
        var clinicPatient = await _clinicPatientRepository.GetClinicPatientByIdAsync(dto.ClinicPatientId);
        if (clinicPatient is null)
        {
            throw new ClinicPatientNotFoundException(dto.ClinicPatientId);
        }

        clinicPatient.PatientName = dto.PatientName;
        clinicPatient.PatientSurname = dto.PatientSurname;
        clinicPatient.PeselNumber = dto.PeselNumber;
        clinicPatient.ContactNumber = dto.ContactNumber;
        clinicPatient.Email = dto.Email;

        await _clinicPatientRepository.UpdateClinicPatientAsync(clinicPatient);
    }

    public async Task DeleteClinicPatientAsync(Guid id)
    {
        var clinicPatient = await _clinicPatientRepository.GetClinicPatientByIdAsync(id);
        if (clinicPatient is null)
        {
            throw new ClinicPatientNotFoundException(id);
        }

        await _clinicPatientRepository.DeleteClinicPatientAsync(clinicPatient);
    }

    public async Task<Guid> GetClinicPatientIdAsync(Guid id)
    {
        var patientId = await _clinicPatientRepository.GetPatientIdAsync(id);
        return patientId;
    }

    private static T Map<T>(ClinicPatient clinicPatient) where T : ClinicPatientDto, new() => new T()
    {
        ClinicPatientId = clinicPatient.ClinicPatientId,
        PatientName = clinicPatient.PatientName,
        PatientSurname = clinicPatient.PatientSurname,
        PeselNumber = clinicPatient.PeselNumber,
        ContactNumber = clinicPatient.ContactNumber,
        Email = clinicPatient.Email
    };
}