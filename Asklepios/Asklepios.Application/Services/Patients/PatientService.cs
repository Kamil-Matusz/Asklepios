using Asklepios.Core.DTO.Examinations;
using Asklepios.Core.DTO.Patients;
using Asklepios.Core.Entities.Patients;
using Asklepios.Core.Exceptions.Patients;
using Asklepios.Core.Policies.Patients;
using Asklepios.Core.Repositories.Patients;

namespace Asklepios.Application.Services.Patients;

public class PatientService : IPatientService
{
    private readonly IPatientRepository _patientRepository;
    private readonly IAddPatientPolicy _addPatientPolicy;

    public PatientService(IPatientRepository patientRepository, IAddPatientPolicy addPatientPolicy)
    {
        _patientRepository = patientRepository;
        _addPatientPolicy = addPatientPolicy;
    }

    public async Task AddPatientAsync(PatientDto dto)
    {
        if (await _addPatientPolicy.CannotAddPatientToTheDepartment(dto.DepartmentId) is false)
        {
            throw new CannotAddPatientToDepartmentException(dto.DepartmentId);
        }

        if (await _addPatientPolicy.CannotAddPatientToTheRoom(dto.RoomId) is false)
        {
            throw new CannotAddPatientToRoomException(dto.RoomId);
        }
        
        dto.PatientId = Guid.NewGuid();
        await _patientRepository.AddPatientAsync(new Patient
        {
            PatientId = dto.PatientId,
            PatientName = dto.PatientName,
            PatientSurname = dto.PatientSurname,
            PeselNumber = dto.PeselNumber,
            InitialDiagnosis = dto.InitialDiagnosis,
            IsDischarged = dto.IsDischarged,
            Treatment = dto.Treatment,
            DepartmentId = dto.DepartmentId,
            RoomId = dto.RoomId
        });
    }

    public async Task<PatientDto> GetPatientDataAsync(Guid id)
    {
        var patient = await _patientRepository.GetPatientByIdAsync(id);
        if (patient is null)
        {
            throw new PatientNotFoundException(id);
        }

        var dto = Map<PatientDto>(patient);
        return dto;
    }

    public async Task<PatientDetailsDto> GetPatientAsync(Guid id)
    {
        var patient = await _patientRepository.GetPatientByIdAsync(id);
        if (patient is null)
        {
            throw new PatientNotFoundException(id);
        }
        
        var dto = Map<PatientDetailsDto>(patient);
        dto.Operations = patient.Operations?.Select(x => new OperationDto
        {
            OperationId = x.OperationId,
            OperationName = x.OperationName,
            AnesthesiaType = x.AnesthesiaType,
            Result = x.Result,
            Comlications = x.Comlications,
            StartDate = x.StartDate,
            EndDate = x.EndDate,
        }).ToList();

        dto.ExamResults = patient.ExamResults?.Select(x => new ExamResultDto
        {
            ExamResultId = x.ExamResultId,
            PatientId = x.PatientId,
            Date = x.Date,
            Result = x.Result,
            Comment = x.Comment,
            ExamId = x.ExaminationId
        }).ToList();

        return dto;
    }

    
    public async Task<IReadOnlyList<PatientListDto>> GetAllPatientsAsync(int pageIndex, int pageSize)
    {
        var patients = await _patientRepository.GetAllPatientsAsync(pageIndex, pageSize);
        return patients.Select(MapPatientList<PatientListDto>).ToList();
    }

    public async Task UpdatePatientAsync(PatientDto dto)
    {
        var patient = await _patientRepository.GetPatientByIdAsync(dto.PatientId);
        if (patient is null)
        {
            throw new PatientNotFoundException(dto.PatientId);
        }

        patient.PatientName = dto.PatientName;
        patient.PatientSurname = dto.PatientSurname;
        patient.PeselNumber = dto.PeselNumber;
        patient.InitialDiagnosis = dto.InitialDiagnosis;
        patient.IsDischarged = dto.IsDischarged;
        patient.Treatment = dto.Treatment;
        patient.DepartmentId = dto.DepartmentId;
        patient.RoomId = dto.RoomId;

        await _patientRepository.UpdatePatientAsync(patient);
    }

    public async Task DeletePatientAsync(Guid id)
    {
        var patient = await _patientRepository.GetPatientByIdAsync(id);
        if (patient is null)
        {
            throw new PatientNotFoundException(id);
        }
        
        await _patientRepository.DeletePatientAsync(patient);
    }

    public async Task<List<PatientAutocompleteDto>> GetPatientsList()
    {
        var patients = await _patientRepository.GetPatientsList();
        return patients.Select(MapPatientsListAutocomplete<PatientAutocompleteDto>).ToList();
    }

    private static T Map<T>(Patient patient) where T : PatientDto, new() => new T()
    {
       PatientId = patient.PatientId,
       PatientName = patient.PatientName,
       PatientSurname = patient.PatientSurname,
       PeselNumber = patient.PeselNumber,
       InitialDiagnosis = patient.InitialDiagnosis,
       IsDischarged = patient.IsDischarged,
       Treatment = patient.Treatment,
       DepartmentId = patient.DepartmentId,
       RoomId = patient.RoomId
    };
    
    private static T MapPatientList<T>(Patient patient) where T : PatientListDto, new() => new T()
    {
        PatientId = patient.PatientId,
        PatientName = patient.PatientName,
        PatientSurname = patient.PatientSurname,
        PeselNumber = patient.PeselNumber,
        InitialDiagnosis = patient.InitialDiagnosis,
        IsDischarged = patient.IsDischarged,
        Treatment = patient.Treatment,
        DepartmentName = patient.Department.DepartmentName,
        RoomNumber = patient.Room.RoomNumber
    };
    
    private static T MapPatientsListAutocomplete<T>(Patient patient) where T : PatientAutocompleteDto, new() => new T()
    {
        PatientId = patient.PatientId,
        PatientName = patient.PatientName,
        PatientSurname = patient.PatientSurname,
        PeselNumber = patient.PeselNumber
    };
}