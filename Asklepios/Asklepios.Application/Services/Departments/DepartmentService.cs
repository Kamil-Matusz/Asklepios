using Asklepios.Core.DTO.Departments;
using Asklepios.Core.DTO.Patients;
using Asklepios.Core.Entities.Departments;
using Asklepios.Core.Exceptions.Departments;
using Asklepios.Core.Policies.Departments;
using Asklepios.Core.Repositories.Departments;

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
            throw new DepartmentNotFoundException(id);
        }

        var dto = Map<DepartmentDetailsDto>(department);
        
        dto.Rooms = department.Rooms?.Select(x => new RoomDto
        {
            RoomId = x.RoomId,
            RoomNumber = x.RoomNumber,
            RoomType = x.RoomType
        }).ToList();
        
        dto.Patients = department.Patients?.Select(x => new PatientDto
        {
            PatientId = x.PatientId,
            PatientName = x.PatientName,
            PatientSurname = x.PatientSurname,
            InitialDiagnosis = x.InitialDiagnosis,
            IsDischarged = x.IsDischarged,
            Treatment = x.Treatment,
        }).ToList();

        return dto;
    }

    public async Task<IReadOnlyList<DepartmentListDto>> GetAllDepartmentsAsync(int pageIndex, int pageSize)
    {
        var departments = await _departmentRepository.GetAllDepartmentsAsync(pageIndex, pageSize);
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
    
    public async Task<List<DepartmentDto>> GetAutocompleteAsync(string search, int limit = 10)
    {
        var departments = await _departmentRepository.GetAutocompleteDepartments(search, limit);
        return departments.Select(Map<DepartmentDto>).ToList();
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