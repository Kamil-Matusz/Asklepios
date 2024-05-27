using Asklepios.Core.DTO.Examinations;
using Asklepios.Core.Entities.Examinations;
using Asklepios.Core.Exceptions.Examinations;
using Asklepios.Core.Repositories.Examinations;

namespace Asklepios.Application.Services.Examinations;

public class OperationService : IOperationService
{
    private readonly IOperationRepository _operationRepository;

    public OperationService(IOperationRepository operationRepository)
    {
        _operationRepository = operationRepository;
    }

    public async Task AddOperationAsync(OperationDto dto)
    {
        dto.OperationId = Guid.NewGuid();
        await _operationRepository.AddOperationAsync(new Operation
        {
            OperationId = dto.OperationId,
            OperationName = dto.OperationName,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            AnesthesiaType = dto.AnesthesiaType,
            Result = dto.Result,
            Comlications = dto.Comlications,
            PatientId = dto.PatientId,
            MedicalStaffId = dto.MedicalStaffId
        });
    }

    public async Task<IReadOnlyList<OperationItemDto>> GetAllOperationsAsync(int pageIndex, int pageSize)
    {
        var operations = await _operationRepository.GetAllOperationsAsync(pageIndex, pageSize);
        return operations.Select(MapOperationItem<OperationItemDto>).ToList();
    }

    public async Task<IReadOnlyList<OperationItemDto>> GetAllOperationsByDoctorIdAsync(Guid doctorId, int pageIndex, int pageSize)
    {
        var operations = await _operationRepository.GetAllOperationsByDoctorAsync(doctorId, pageIndex, pageSize);
        return operations.Select(MapOperationItem<OperationItemDto>).ToList();
    }

    public async Task<OperationItemDto> GetOperationByIdAsync(Guid id)
    {
        var operation = await _operationRepository.GetOperationByIdAsync(id);
        if (operation is null)
        {
            throw new OperationNotFoundException(id);
        }
        
        var dto = MapOperationItem<OperationItemDto>(operation);

        return dto;
    }

    public async Task UpdateOperationAsync(OperationDto dto)
    {
        var operation = await _operationRepository.GetOperationByIdAsync(dto.OperationId);
        if (operation is null)
        {
            throw new OperationNotFoundException(dto.OperationId);
        }

        await _operationRepository.UpdateOperationAsync(operation);
    }

    public async Task DeleteOperationAsync(Guid id)
    {
        var operation = await _operationRepository.GetOperationByIdAsync(id);
        if (operation is null)
        {
            throw new OperationNotFoundException(id);
        }
        
        await _operationRepository.DeleteOperationAsync(operation);
    }
    
    private static T Map<T>(Operation operation) where T : OperationDto, new() => new T()
    {
        OperationId = operation.OperationId,
        OperationName = operation.OperationName,
        StartDate = operation.StartDate,
        EndDate = operation.EndDate,
        AnesthesiaType = operation.AnesthesiaType,
        Result = operation.Result,
        Comlications = operation.Comlications,
        PatientId = operation.PatientId,
        MedicalStaffId = operation.MedicalStaffId
    };
    
    private static T MapOperationItem<T>(Operation operation) where T : OperationItemDto, new() => new T()
    {
        OperationId = operation.OperationId,
        OperationName = operation.OperationName,
        StartDate = operation.StartDate,
        EndDate = operation.EndDate,
        AnesthesiaType = operation.AnesthesiaType,
        Result = operation.Result,
        Comlications = operation.Comlications,
        DoctorName = operation.MedicalStaff.Name,
        DoctornSurname = operation.MedicalStaff.Surname,
        PatientName = operation.Patient.PatientName,
        PatientSurname = operation.Patient.PatientSurname
    };
}