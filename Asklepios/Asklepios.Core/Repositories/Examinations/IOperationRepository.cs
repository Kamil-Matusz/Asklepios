using Asklepios.Core.Entities.Examinations;

namespace Asklepios.Core.Repositories.Examinations;

public interface IOperationRepository
{
    Task<Operation> GetOperationByIdAsync(Guid operationId);
    Task<IReadOnlyList<Operation>> GetAllOperationsAsync(int pageIndex, int pageSize);
    Task AddOperationAsync(Operation operation);
    Task UpdateOperationAsync(Operation operation);
    Task DeleteOperationAsync(Operation operation);
}