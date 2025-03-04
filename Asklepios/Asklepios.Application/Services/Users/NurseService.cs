using Asklepios.Core.DTO.Users;
using Asklepios.Core.Entities.Users;
using Asklepios.Core.Exceptions.Users;
using Asklepios.Core.Policies.Users;
using Asklepios.Core.Repositories.Users;

namespace Asklepios.Application.Services.Users;

public class NurseService : INurseService
{
    private readonly INurseRepository _nurseRepository;
    private readonly IRolePolicy _rolePolicy;
    private readonly INurseCacheRepository _nurseCacheRepository;

    public NurseService(INurseRepository nurseRepository, IRolePolicy rolePolicy, INurseCacheRepository nurseCacheRepository)
    {
        _nurseRepository = nurseRepository;
        _rolePolicy = rolePolicy;
        _nurseCacheRepository = nurseCacheRepository;
    }

    public async Task AddNurseAsync(NurseDto dto)
    {
        if (await _rolePolicy.CannotCreateNurse(dto.UserId) is false)
        {
            throw new CannotCreateNurseException();
        }
        
        dto.NurseId = Guid.NewGuid();
        await _nurseRepository.AddNurseAsync(new Nurse
        {
            NurseId = dto.NurseId,
            Name = dto.Name,
            Surname = dto.Surname,
            UserId = dto.UserId,
            DepartmentId = dto.DepartmentId
        });
    }

    public async Task<NurseDto> GetNurseAsync(Guid id)
    {
        var nurse = await _nurseRepository.GetNurseByIdAsync(id);
        if (nurse is null)
        {
            throw new NurseNotFoundException(id);
        }
        
        var dto = Map<NurseDto>(nurse);
        return dto;
    }

    public async Task<IReadOnlyList<NurseListDto>> GetAllNursesAsync(int pageIndex, int pageSize)
    {
        var cachedNurses = await _nurseCacheRepository.GetNursesAsync(pageIndex, pageSize);
        if (cachedNurses != null)
            return cachedNurses;
        
        var nurses = await _nurseRepository.GetAllNursesAsync(pageIndex, pageSize);
        var nursesDto = nurses.Select(MapNurseList<NurseListDto>).ToList();
        
        await _nurseCacheRepository.SetNursesAsync(pageIndex, pageSize, nursesDto);
        return nursesDto;
    }

    public async Task UpdateNurseAsync(NurseDto dto)
    {
        var nurse = await _nurseRepository.GetNurseByIdAsync(dto.NurseId);
        if (nurse is null)
        {
            throw new NurseNotFoundException(dto.NurseId);
        }

        nurse.Name = dto.Name;
        nurse.Surname = dto.Surname;
        nurse.DepartmentId = dto.DepartmentId;
        nurse.UserId = dto.UserId;

        await _nurseRepository.UpdateNurseAsync(nurse);
    }

    public async Task DeleteNurseAsync(Guid id)
    {
        var nurse = await _nurseRepository.GetNurseByIdAsync(id);
        if (nurse is null)
        {
            throw new NurseNotFoundException(id);
        }

        await _nurseRepository.DeleteNurseAsync(nurse);
    }
    
    private static T Map<T>(Nurse nurse) where T : NurseDto, new() => new T()
    {
        NurseId = nurse.NurseId,
        Name = nurse.Name,
        Surname = nurse.Surname,
        UserId = nurse.UserId,
        DepartmentId = nurse.DepartmentId
    };
    
    private static T MapNurseList<T>(Nurse nurse) where T : NurseListDto, new() => new T()
    {
        NurseId = nurse.NurseId,
        Name = nurse.Name,
        Surname = nurse.Surname,
        DepartmentName = nurse.Department.DepartmentName
    };
}