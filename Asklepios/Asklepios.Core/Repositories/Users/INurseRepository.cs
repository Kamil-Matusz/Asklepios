using Asklepios.Core.Entities.Users;

namespace Asklepios.Core.Repositories.Users;

public interface INurseRepository
{
    Task<Nurse> GetNurseByIdAsync(Guid nurseId);
    Task<IReadOnlyList<Nurse>> GetAllNursesAsync(int pageIndex, int pageSize);
    Task AddNurseAsync(Nurse nurse);
    Task UpdateNurseAsync(Nurse nurse);
    Task DeleteNurseAsync(Nurse nurse);
}