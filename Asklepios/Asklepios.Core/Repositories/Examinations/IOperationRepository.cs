using Asklepios.Core.Entities.Examinations;

namespace Asklepios.Core.Repositories.Examinations;

public interface IOperationRepository
{
    Task<Operation> GetOperationByIdAsync(Guid operationId);
    Task<IReadOnlyList<Operation>> GetAllOperationsAsync(int pageIndex, int pageSize);
    Task<IReadOnlyList<Operation>> GetAllOperationsByPatientAsync(Guid patientId);
    Task<IReadOnlyList<Operation>> GetAllOperationsByDoctorAsync(Guid doctorId, int pageIndex, int pageSize);
    Task AddOperationAsync(Operation operation);
    Task UpdateOperationAsync(Operation operation);
    Task DeleteOperationAsync(Operation operation);
}