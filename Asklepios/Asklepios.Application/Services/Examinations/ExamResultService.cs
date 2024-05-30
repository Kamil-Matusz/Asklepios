using Asklepios.Core.DTO.Examinations;
using Asklepios.Core.Entities.Examinations;
using Asklepios.Core.Exceptions.Examinations;
using Asklepios.Core.Repositories.Examinations;

namespace Asklepios.Application.Services.Examinations;

public class ExamResultService : IExamResultService
{
    private readonly IExamResultRepository _examResultRepository;

    public ExamResultService(IExamResultRepository examResultRepository)
    {
        _examResultRepository = examResultRepository;
    }

    public async Task AddExamResultAsync(ExamResultDto dto)
    {
        dto.ExamResultId = Guid.NewGuid();
        await _examResultRepository.AddExamResultAsync(new ExamResult
        {
            ExamResultId = dto.ExamResultId,
            PatientId = dto.PatientId,
            Date = dto.Date,
            Comment = dto.Comment,
            Result = dto.Result,
            ExaminationId = dto.ExamId
        });
    }

    public async Task<ExamResultDto> GetExamResultAsync(Guid id)
    {
        var examResult = await _examResultRepository.GetExamResultByIdAsync(id);
        if (examResult is null)
        {
            throw new ExamResultNotFoundExaction(id);
        }

        var dto = Map<ExamResultDto>(examResult);

        return dto;
    }

    public async Task<IReadOnlyList<ExamResultListDto>> GetAllExamResultsAsync(int pageIndex, int pageSize)
    {
        var examResults = await _examResultRepository.GetAllExamResultsAsync(pageIndex, pageSize);
        return examResults.Select(MapExamResultList<ExamResultListDto>).ToList();
    }

    public async Task UpdateExamResultAsync(ExamResultDto dto)
    {
        var examResult = await _examResultRepository.GetExamResultByIdAsync(dto.ExamResultId);
        if (examResult is null)
        {
            throw new ExamResultNotFoundExaction(dto.ExamResultId);
        }

        examResult.PatientId = dto.PatientId;
        examResult.Date = dto.Date;
        examResult.Comment = dto.Comment;
        examResult.Result = dto.Result;
        examResult.ExaminationId = dto.ExamId;

        await _examResultRepository.UpdateExamResultAsync(examResult);
    }

    public async Task DeleteExamResultAsync(Guid id)
    {
        var examResult = await _examResultRepository.GetExamResultByIdAsync(id);
        if (examResult is null)
        {
            throw new ExamResultNotFoundExaction(id);
        }

        await _examResultRepository.DeleteExamResultAsync(examResult);
    }
    
    private static T Map<T>(ExamResult examResult) where T : ExamResultDto, new() => new T()
    {
        ExamResultId = examResult.ExamResultId,
        PatientId = examResult.PatientId,
        Date = examResult.Date,
        Comment = examResult.Comment,
        Result = examResult.Result,
        ExamId = examResult.ExaminationId
    };
    
    private static T MapExamResultList<T>(ExamResult examResult) where T : ExamResultListDto, new() => new T()
    {
        ExamResultId = examResult.ExamResultId,
        PatientId = examResult.PatientId,
        Date = examResult.Date,
        Comment = examResult.Comment,
        Result = examResult.Result,
        ExamName = examResult.Examination.ExamName
    };
}