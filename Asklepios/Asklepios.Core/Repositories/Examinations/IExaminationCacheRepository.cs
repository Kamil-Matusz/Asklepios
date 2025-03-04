using Asklepios.Core.DTO.Examinations;

namespace Asklepios.Core.Repositories.Examinations;

public interface IExaminationCacheRepository
{
    Task<IReadOnlyList<ExaminationDto>?> GetExaminationsAsync(int pageIndex, int pageSize);
    Task SetExaminationsAsync(int pageIndex, int pageSize, IReadOnlyList<ExaminationDto> examinations);
}
