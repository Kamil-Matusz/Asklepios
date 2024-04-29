using Asklepios.Core.DTO.Users;

namespace Asklepios.Application.Services.Users;

public interface INurseService
{
    Task AddNurseAsync(NurseDto dto);
    Task<NurseDto> GetNurseAsync(Guid id);
    Task<IReadOnlyList<NurseListDto>> GetAllNursesAsync(int pageIndex, int pageSize);
    Task UpdateNurseAsync(NurseDto dto);
    Task DeleteNurseAsync(Guid id);
}