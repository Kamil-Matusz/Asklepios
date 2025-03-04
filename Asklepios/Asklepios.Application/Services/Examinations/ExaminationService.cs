using Asklepios.Core.DTO.Examinations;
using Asklepios.Core.Entities.Examinations;
using Asklepios.Core.Exceptions.Examinations;
using Asklepios.Core.Repositories.Examinations;

namespace Asklepios.Application.Services.Examinations;

public class ExaminationService : IExaminationService
{
    private readonly IExaminationRepository _examinationRepository;
    private readonly IExaminationCacheRepository _examinationCacheRepository;

    public ExaminationService(IExaminationRepository examinationRepository, IExaminationCacheRepository examinationCacheRepository)
    {
        _examinationRepository = examinationRepository;
        _examinationCacheRepository = examinationCacheRepository;
    }

    public async Task AddExaminationAsync(ExaminationDto dto)
    {
        await _examinationRepository.AddExaminationAsync(new Examination
        {
            ExaminationId = dto.ExamId,
            ExamName = dto.ExamName,
            ExamCategory = dto.ExamCategory
        });
    }

    public async Task<ExaminationDto> GetExaminationAsync(int id)
    {
        var examination = await _examinationRepository.GetExaminationByIdAsync(id);
        if (examination is null)
        {
            throw new ExaminationNotFoundException(id);
        }

        var dto = Map<ExaminationDto>(examination);

        return dto;
    }

    public async Task<IReadOnlyList<ExaminationDto>> GetAllExaminationsAsync(int pageIndex, int pageSize)
    {
        var cachedExaminations = await _examinationCacheRepository.GetExaminationsAsync(pageIndex, pageSize);
        if (cachedExaminations != null)
            return cachedExaminations;
        
        var examinations = await _examinationRepository.GetAllExaminationsAsync(pageIndex, pageSize);
        var examinationDtos = examinations.Select(Map<ExaminationDto>).ToList();
        
        await _examinationCacheRepository.SetExaminationsAsync(pageIndex, pageSize, examinationDtos);
        return examinationDtos;
    }

    public async Task<IReadOnlyList<ExaminationDto>> GetAllExaminationsByCategoryAsync(string category, int pageIndex, int pageSize)
    {
        var examinations = await _examinationRepository.GetAllExaminationsByCategoryAsync(category, pageIndex, pageSize);
        return examinations.Select(Map<ExaminationDto>).ToList();
    }

    public async Task UpdateExaminationAsync(ExaminationDto dto)
    {
        var examination = await _examinationRepository.GetExaminationByIdAsync(dto.ExamId);
        if (examination is null)
        {
            throw new ExaminationNotFoundException(dto.ExamId);
        }
        
        examination.ExamName = dto.ExamName;
        examination.ExamCategory = dto.ExamCategory;
        
        await _examinationRepository.UpdateExaminationAsync(examination);
    }

    public async Task DeleteExaminationAsync(int id)
    {
        var examination = await _examinationRepository.GetExaminationByIdAsync(id);
        if (examination is null)
        {
            throw new ExaminationNotFoundException(id);
        }
        
        await _examinationRepository.DeleteExaminationAsync(examination);
    }

    public async Task<List<ExaminationAutocompleteDto>> GetExaminationsList()
    {
        var examinations = await _examinationRepository.GetExaminationsList();
        return examinations.Select(MapExaminationsListAutocomplete<ExaminationAutocompleteDto>).ToList();
    }

    private static T Map<T>(Examination examination) where T : ExaminationDto, new() => new T()
    {
        ExamId = examination.ExaminationId,
        ExamName = examination.ExamName,
        ExamCategory = examination.ExamCategory
    };
    
    private static T MapExaminationsListAutocomplete<T>(Examination examination) where T : ExaminationAutocompleteDto, new() => new T()
    {
        ExamId = examination.ExaminationId,
        ExamName = examination.ExamName,
    };
}