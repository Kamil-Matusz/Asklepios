using Asklepios.Core.DTO.Departments;
using Asklepios.Core.Entities.Departments;
using Asklepios.Core.Exceptions.Departments;
using Asklepios.Core.Policies.Departments;
using Asklepios.Infrastructure.Repositories.Departments;

namespace Asklepios.Application.Services.Departments;

public class DepartmentService : IDeparmentService
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IDepartmentDeletePolicy _departmentDeletePolicy;

    public DepartmentService(IDepartmentRepository departmentRepository, IDepartmentDeletePolicy departmentDeletePolicy)
    {
        _departmentRepository = departmentRepository;
        _departmentDeletePolicy = departmentDeletePolicy;
    }

    public async Task AddDepartmentAsync(DepartmentDto dto)
    {
        dto.DepartmentId = Guid.NewGuid();
        await _departmentRepository.AddDepartmentAsync(new Department
        {
            DepartmentId = dto.DepartmentId,
            DepartmentName = dto.DepartmentName,
            NumberOfBeds = dto.NumberOfBeds,
            ActualNumberOfPatients = dto.ActualNumberOfPatients
        });
    }

    public async Task<DepartmentDetailsDto> GetDepartmentAsync(Guid id)
    {
        var department = await _departmentRepository.GetDepartmentByIdAsync(id);
        if (department is null)
        {
            return null;
        }

        var dto = Map<DepartmentDetailsDto>(department);
        dto.Rooms = department.Rooms?.Select(x => new RoomDto
        {
            RoomId = x.RoomId,
            DepartmentId = x.DepartmentId,
            RoomNumber = x.RoomNumber,
            RoomType = x.RoomType
        }).ToList();

        return dto;
    }

    public async Task<IReadOnlyList<DepartmentListDto>> GetAllDepartmentsAsync()
    {
        var departments = await _departmentRepository.GetAllDepartmentsAsync();
        return departments.Select(MapDepartmentList<DepartmentListDto>).ToList();
    }

    public async Task UpdateDepartmentAsync(DepartmentDetailsDto dto)
    {
        var department = await _departmentRepository.GetDepartmentByIdAsync(dto.DepartmentId);
        if (department is null)
        {
            throw new DepartmentNotFoundException(dto.DepartmentId);
        }

        department.DepartmentName = dto.DepartmentName;
        department.NumberOfBeds = dto.NumberOfBeds;

        await _departmentRepository.UpdateDepartmentAsync(department);
    }

    public async Task DeleteDepartmentAsync(Guid id)
    {
        var department = await _departmentRepository.GetDepartmentByIdAsync(id);
        if (department is null)
        {
            throw new DepartmentNotFoundException(id);
        }

        if (await _departmentDeletePolicy.CannotDeleteDepartmentPolicy(department) is false)
        {
            throw new CannotDeleteDepartmentException(id);
        }

        await _departmentRepository.DeleteDepartmentAsync(department);
    }

    private static T Map<T>(Department department) where T : DepartmentDto, new() => new T()
    {
        DepartmentId = department.DepartmentId,
        DepartmentName = department.DepartmentName,
        NumberOfBeds = department.NumberOfBeds,
        ActualNumberOfPatients = department.ActualNumberOfPatients
    };
    
    private static T MapDepartmentList<T>(Department department) where T : DepartmentListDto, new() => new T()
    {
        DepartmentId = department.DepartmentId,
        DepartmentName = department.DepartmentName,
        NumberOfBeds = department.NumberOfBeds,
        ActualNumberOfPatients = department.ActualNumberOfPatients
    };
}