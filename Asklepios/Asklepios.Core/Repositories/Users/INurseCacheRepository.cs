using Asklepios.Core.DTO.Users;

namespace Asklepios.Core.Repositories.Users;

public interface INurseCacheRepository
{
    Task<IReadOnlyList<NurseListDto>?> GetNursesAsync(int pageIndex, int pageSize);
    Task SetNursesAsync(int pageIndex, int pageSize, IReadOnlyList<NurseListDto> nurseList);
}