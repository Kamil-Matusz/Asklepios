using Asklepios.Core.Entities.Examinations;

namespace Asklepios.Core.Repositories.Examinations;

public interface IExamResultRepository
{
    Task<ExamResult> GetExamResultByIdAsync(Guid examResultId);
    Task<IReadOnlyList<ExamResult>> GetAllExamResultsAsync(int pageIndex, int pageSize);
    Task AddExamResultAsync(ExamResult examResult);
    Task UpdateExamResultAsync(ExamResult examResult);
    Task DeleteExamResultAsync(ExamResult examResult);
}