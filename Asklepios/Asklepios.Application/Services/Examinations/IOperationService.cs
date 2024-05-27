using Asklepios.Core.DTO.Examinations;
using Asklepios.Core.Entities.Examinations;

namespace Asklepios.Application.Services.Examinations;

public interface IOperationService
{
    Task AddOperationAsync(OperationDto dto);
    Task<IReadOnlyList<OperationItemDto>> GetAllOperationsAsync(int pageIndex, int pageSize);
    Task<IReadOnlyList<OperationItemDto>> GetAllOperationsByDoctorIdAsync(Guid doctorId, int pageIndex, int pageSize);
    Task<OperationItemDto> GetOperationByIdAsync(Guid id);
    Task UpdateOperationAsync(OperationDto dto);
    Task DeleteOperationAsync(Guid id);
}