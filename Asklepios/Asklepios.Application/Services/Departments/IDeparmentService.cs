using Asklepios.Core.DTO.Departments;

namespace Asklepios.Application.Services.Departments;

public interface IDeparmentService
{
    Task AddDepartmentAsync(DepartmentDto dto);
    Task<DepartmentDetailsDto> GetDepartmentAsync(Guid id);
    Task<IReadOnlyList<DepartmentListDto>> GetAllDepartmentsAsync(int pageIndex, int pageSize);
    Task UpdateDepartmentAsync(DepartmentDetailsDto dto);
    Task DeleteDepartmentAsync(Guid id);
    Task<List<DepartmentDto>> GetAutocompleteAsync(string search, int limit = 10);
}