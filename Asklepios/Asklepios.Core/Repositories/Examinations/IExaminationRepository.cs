using Asklepios.Core.Entities.Examinations;

namespace Asklepios.Core.Repositories.Examinations;

public interface IExaminationRepository
{
    Task<Examination> GetExaminationByIdAsync(int examinationId);
    Task<IReadOnlyList<Examination>> GetAllExaminationsAsync(int pageIndex, int pageSize);
    Task<IReadOnlyList<Examination>> GetAllExaminationsByCategoryAsync(string category ,int pageIndex, int pageSize);
    Task AddExaminationAsync(Examination examination);
    Task UpdateExaminationAsync(Examination examination);
    Task DeleteExaminationAsync(Examination examination);
}