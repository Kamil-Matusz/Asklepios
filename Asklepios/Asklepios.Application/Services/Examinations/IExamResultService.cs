using Asklepios.Core.DTO.Examinations;

namespace Asklepios.Application.Services.Examinations;

public interface IExamResultService
{
    Task AddExamResultAsync(ExamResultDto dto);
    Task<ExamResultDto> GetExamResultAsync(Guid id);
    Task<IReadOnlyList<ExamResultListDto>> GetAllExamResultsAsync(int pageIndex, int pageSize);
    Task UpdateExamResultAsync(ExamResultDto dto);
    Task DeleteExamResultAsync(Guid id);
}